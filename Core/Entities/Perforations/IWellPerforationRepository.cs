using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.Perforations
{
    public interface IWellPerforationRepository
    {
        Task DeletePerforationItemAsync(string fieldId, int itemId);
    }
}
