using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MountAnything;
using MountConsul.Catalog;

namespace MountConsul;

public class ConsulClient : IDisposable
{
    private readonly HttpClient _client;

    public ConsulClient(ConsulConfig config, IPathHandlerContext context)
    {
        _client = new HttpClient(new DebugLoggingHandler(context, new HttpClientHandler()))
        {
            BaseAddress = config.ConsulEndpoint
        };
        if (config.AclToken != null)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.AclToken);
        }
    }

    public ServiceNode[] GetServiceNodes(string serviceName)
    {
        return _client.GetJson<ServiceNode[]>($"v1/catalog/service/{serviceName}");
    }

    public ServiceNode? GetServiceNode(string serviceName, string nodeName)
    {
        return _client.GetJson<ServiceNode[]>(
            $"v1/catalog/service/{serviceName}?filter={WebUtility.UrlEncode($"Node == \"{nodeName}\"")}")
            .FirstOrDefault();
    }

    public Service[] GetServices()
    {
        var response = _client.GetJson<Dictionary<string,string[]>>("v1/catalog/services");

        return response.Select(p => new Service
        {
            Name = p.Key,
            Tags = p.Value
        }).ToArray();
    }

    public Service? GetService(string serviceName)
    {
        var response = _client.GetJson<Dictionary<string,string[]>>($"v1/catalog/services?filter={WebUtility.UrlEncode($"ServiceName == \"{serviceName}\"")}");
        if (response.TryGetValue(serviceName, out var tags))
        {
            return new Service
            {
                Name = serviceName,
                Tags = tags
            };
        }

        return null;
    }

    public void Dispose()
    {
        _client?.Dispose();
    }
    
    private class DebugLoggingHandler : DelegatingHandler
    {
        private readonly IPathHandlerContext _context;

        public DebugLoggingHandler(IPathHandlerContext context, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _context = context;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _context.WriteDebug(ToMessage(request));
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var response = base.Send(request, cancellationToken);
            _context.WriteDebug($"{response.StatusCode:D} ({response.StatusCode}) in {timer.ElapsedMilliseconds}ms");

            return response;
        }

        private string ToMessage(HttpRequestMessage request)
        {
            return $"{request.Method.Method} {request.RequestUri}";
        }
    }
}