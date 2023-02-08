using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.ReservoirPressures
{
    // Возвращает сущности предметной области, связанные с приведением пластового давления, определенные в этом же пространстве имён, в этой же папке
    public interface IReservoirPressureInformationRepository
    {
        Task<ReservoirPressureInformation> Get(string fieldId, string wellId, string modelId);
        Task Set(ReservoirPressureInformation pressureInformation);
    }
}
