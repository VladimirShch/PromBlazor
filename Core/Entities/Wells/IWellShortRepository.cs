using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells
{
    // Должен ли он находиться в уровне Core? Или интерфейс репозитория это уровень Application? Либо конкретно для WellShort это уровень приложения?
    public interface IWellShortRepository
    {
        Task<IEnumerable<WellShort>> GetAll(string fieldId);
    }
}
