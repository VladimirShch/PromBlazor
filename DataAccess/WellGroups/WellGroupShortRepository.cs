using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.Core.Entities.Common;
using Web_Prom.Core.Blazor.Core.Entities.WellGroups;

namespace Web_Prom.Core.Blazor.DataAccess.WellGroups
{
    public class WellGroupShortRepository : IWellGroupShortRepository, IWellGroupShortRepositoryMaker, IAllWellGroupsShortRepository
    {
        private readonly List<ReferenceObject> _wellGroups = new();

        public IEnumerable<ReferenceObject> GetWellGroups()
        {
            return _wellGroups;
        }

        public void MakeRepository(DataTable wellGroupsTable)
        {
            if (wellGroupsTable.TableName != "KUST")
            {
                throw new ArgumentException($"Wrong plantsTable name: {wellGroupsTable.TableName}");
            }

            _wellGroups.Clear();

            foreach (DataRow wellGroupRow in wellGroupsTable.Rows)
            {
                // TODO: ненадёжно!
                if(Convert.ToInt64(wellGroupRow["Узел"]) == 0)
                {
                    _wellGroups.Add(ConvertWellGroup(wellGroupRow));
                }             
            }
        }

        public IEnumerable<ReferenceObject> GetWellGroups(string plantId)
        {
            return _wellGroups.Where(t => t.ParentId == plantId).ToList();
        }

        public Task<IEnumerable<ReferenceObject>> GetWellGroupsAsync(string plantId)
        {
            return Task.FromResult(GetWellGroups(plantId));
        }
      
        // TODO: Не отсюда
        private ReferenceObject ConvertWellGroup(DataRow wellGroupRow)
        {
            return new ReferenceObject(
                wellGroupRow["IDКуста"].ToString().Trim(),
                wellGroupRow["ИмяКуста"].ToString().Trim(),
                wellGroupRow["КодГП"].ToString().Trim()
                );
        }
    
    }
}
