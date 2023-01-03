using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using MountAnything;
using MountConsul.Catalog;
using MountConsul.Kv;

namespace MountConsul;

public class ConsulClient : IDisposable
{
    private readonly Datacenter _datacenter;
    private readonly HttpClient _client;

    public ConsulClient(ConsulConfig config, IPathHandlerContext context, Datacenter datacenter)
    {
        _datacenter = datacenter;
        _client = new HttpClient(new DebugLoggingHandler(context, new HttpClientHandler()))
        {
            BaseAddress = config.ConsulEndpoint
        };
        if (config.AclToken != null)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.AclToken);
        }
    }

    public string[] ListDatacenters()
    {
        return _client.GetJson<string[]>("v1/catalog/datacenters");
    }

    public ServiceNode[] GetServiceNodes(string serviceName)
    {
        return _client.GetJson<ServiceNode[]>($"v1/catalog/service/{serviceName}?dc={_datacenter}");
    }

    public ServiceNode? GetServiceNode(string serviceName, string nodeName)
    {
        if (!nodeName.Contains(":"))
        {
            return null;
        }
        
        var nodeParts = nodeName.Split(":");
        var address = nodeParts[0];
        var port = nodeParts[1];
        
        return _client.GetJson<ServiceNode[]>(
            $"v1/catalog/service/{serviceName}?dc={_datacenter}&filter={WebUtility.UrlEncode($"ServiceAddress == \"{address}\" and ServicePort == \"{port}\"")}")
            .FirstOrDefault();
    }

    public Service[] GetServices()
    {
        var response = _client.GetJson<Dictionary<string,string[]>>($"v1/catalog/services?dc={_datacenter}");

        return response.Select(p => new Service
        {
            Name = p.Key,
            Tags = p.Value
        }).ToArray();
    }

    public Service? GetService(string serviceName)
    {
        var response = _client.GetJson<Dictionary<string,string[]>>($"v1/catalog/services?dc={_datacenter}&filter={WebUtility.UrlEncode($"ServiceName == \"{serviceName}\"")}");
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

    public IEnumerable<KeyMetadata> GetKeysRecursive(ItemPath? pathPrefix = null)
    {
        var requestUri = "v1/kv";
        if (pathPrefix != null && !pathPrefix.IsRoot)
        {
            requestUri += $"/{pathPrefix}";
        }

        requestUri += $"?dc={_datacenter}&recurse";

        return _client.GetJson<KeyMetadata[]>(requestUri)
            .Where(k => pathPrefix == null || pathPrefix.IsRoot || k.Key == pathPrefix.FullName || k.Key.StartsWith(pathPrefix.FullName + "/"));
    }

    public void Dispose()
    {
        _client.Dispose();
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