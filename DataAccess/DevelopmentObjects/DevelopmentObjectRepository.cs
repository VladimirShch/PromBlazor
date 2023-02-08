using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Common;
using Web_Prom.Core.Blazor.Core.Entities.DevelopmentObjects;

namespace Web_Prom.Core.Blazor.DataAccess.DevelopmentObjects
{
    public class DevelopmentObjectRepository : IDevelopmentObjectRepository, IDevelopmentObjectRepositoryMaker
    {
        private readonly List<EntityHeader> _developmentObjects = new();

        public void MakeRepository(DataTable developmentObjectsTable)
        {
            if (developmentObjectsTable.TableName != "Objects")
            {
                throw new ArgumentException($"Wrong development objects table name: {developmentObjectsTable.TableName}");
            }

            _developmentObjects.Clear();

            foreach (DataRow developmentObjectRow in developmentObjectsTable.Rows)
            {
                int objectCode = (int)Convert.ToInt64(developmentObjectRow["Объект"]);
                if (objectCode > 100 && objectCode < 1000 && !_developmentObjects.Any(t => t.Id == objectCode))
                {
                    _developmentObjects.Add(
                      new EntityHeader(
                          objectCode,
                          developmentObjectRow["Имя"].ToString().Trim())
                     );
                }
            }
        }

        public IEnumerable<EntityHeader> GetObjects()
        {
            return _developmentObjects;
        }        
    }
}
