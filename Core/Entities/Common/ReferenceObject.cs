
namespace Web_Prom.Core.Blazor.Core.Entities.Common
{
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
    public record ReferenceObject(string Id, string Name, string? ParentId = null);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
}
