using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Common;
using Web_Prom.Core.Blazor.Core.Entities.Horizons;

namespace Web_Prom.Core.Blazor.DataAccess.Horizons
{
    public class HorizonRepository : IHorizonRepository, IHorizonRepositoryMaker
    {
        private readonly List<EntityHeader> _horizons = new();

        public void MakeRepository(DataTable horizonsTable)
        {
            if (horizonsTable.TableName != "Objects")
            {
                throw new ArgumentException($"Wrong development objects table name: {horizonsTable.TableName}");
            }

            _horizons.Clear();

            foreach (DataRow horizonRow in horizonsTable.Rows)
            {
                int objectCode = (int)Convert.ToInt64(horizonRow["Объект"]);
                if (objectCode > 10 && objectCode < 100 && !_horizons.Any(t => t.Id == objectCode))
                {
                    _horizons.Add(
                      new EntityHeader(
                          objectCode,
                          horizonRow["Имя"].ToString().Trim())
                     );
                }
            }
        }

        public IEnumerable<EntityHeader> GetHorizons()
        {
            return _horizons;
        }
      
    }
}
