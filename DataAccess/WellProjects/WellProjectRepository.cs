using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Wells;
using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.DataAccess.WellProjects
{
    public class WellProjectRepository : IWellProjectRepositoryAndMaker
    {
        private readonly Geolog.Contracts.IWORK _workService;
        private readonly List<EntityHeader> _wellProjects = new();

        public WellProjectRepository(Geolog.Contracts.IWORK workService)
        {
            _workService = workService;
        }

        public async Task MakeRepositoryAsync()
        {
            DataSet allCodes = await _workService.LoadCodesAsync();

            if (allCodes is null || allCodes.Tables["DrillProjects"] is null)
            {
                return;
            }

            _wellProjects.Clear();

            foreach (DataRow wellProjectRow in allCodes.Tables["DrillProjects"].Rows)
            {
                int projectId = (int)Convert.ToInt64(wellProjectRow["ID"]);
                string projectDescription = wellProjectRow["uname"].ToString().Trim() + " от " + wellProjectRow["Date_Confurm"].ToString().Trim();
                _wellProjects.Add(
                    new EntityHeader(
                        projectId,
                        projectDescription)
                    );

            }
        }

        public IEnumerable<EntityHeader> GetWellProjects()
        {          
            return _wellProjects;
        }

        
    }
}
