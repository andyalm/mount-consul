using System.Runtime.Versioning;
using MountAnything.Routing;

namespace MountConsul;

[RequiresPreviewFeatures]
public static class RouterExtensions
{
    public static void MapLiteral<THandler>(this IRoutable routable, Action<Route>? createChildRoutes = null) where THandler : ILiteralPathHandler
    {
        routable.MapLiteral<THandler>(THandler.LiteralItemName, createChildRoutes);
    } 
}