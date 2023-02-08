using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.WellConstructions
{
    public interface IWellConstructionRepository
    {
        Task DeleteConstructionItemAsync(string fieldId, int itemId);
    }
}
