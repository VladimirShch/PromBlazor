using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Wells;
using Web_Prom.Core.Blazor.DataAccess.Wells.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Short
{
    public class WellShortAdapter : IWellShortAdapter
    {
        private readonly WellPropertiesConverter _propertiesConverter;

        // TODO: передаю как класс, не интерфейс, использую экземплярные методы, хотя методы у него должны быть статики
        public WellShortAdapter(WellPropertiesConverter propertiesConverter)
        {
            _propertiesConverter = propertiesConverter ?? throw new ArgumentNullException(nameof(propertiesConverter), "Argument can not be null");
        }

        public IEnumerable<WellShort> Convert(DataSet dataSet)
        {
            var wells = new List<WellShort>();

            DataRowCollection wellRows = dataSet.Tables["Wells"].Rows;
            foreach (DataRow row in wellRows)
            {
                if (string.IsNullOrEmpty(row["ParentUwi"].ToString()))
                {
                    wells.Add(Convert(row, dataSet));
                }
            }

            foreach (DataRow row in wellRows)
            {
                if (!string.IsNullOrEmpty(row["ParentUwi"].ToString()))
                {
                    var wellbore = Convert(row, dataSet);

                    WellShort? parentWell = wells.FirstOrDefault(t => t.Id == row["ParentUwi"].ToString());
                    parentWell?.ChildWellbores?.Add(wellbore);
                }
            }

            return wells;
        }

        private WellShort Convert(DataRow wellRow) => new()
        {
            Id = wellRow["UWI"].ToString(),
            Name = wellRow["Names"].ToString(),
            PlantId = wellRow["ГП"].ToString(),
            State = _propertiesConverter.ConvertState(System.Convert.ToInt32(wellRow["Состояние"])),
            OnBalance = _propertiesConverter.ConvertBalance((DateTime)wellRow["Balanse"]),
            Type = _propertiesConverter.ConvertType(System.Convert.ToInt32(wellRow["TYPE"])),
            Shape = _propertiesConverter.ConvertShape(System.Convert.ToInt32(wellRow["StvolType"]))
        };

        private WellShort Convert(DataRow wellRow, DataSet dataSet)
        {
            WellShort resultWell = Convert(wellRow);
            try
            {
                DataRow[] wellsReservoirs = dataSet.Tables["Plast"].Select($"UWI='{resultWell.Id}'");
                // А если нет элементов в массиве?
                if (wellsReservoirs[0]["Пласт"] is DBNull)
                {
                    resultWell.PerforationObjects.Add(0);
                }
                else if (System.Convert.ToInt32(wellsReservoirs[0]["Пласт"]) == -999)
                {
                    resultWell.PerforationObjects.Add(-1);
                }
                else
                {
                    foreach(DataRow wellReservoir in wellsReservoirs)
                    {
                        DataRow[] reservoirObjects = dataSet.Tables["Objects"].Select($"Пласт={wellReservoir["Пласт"]} AND Объект < 1000");
                        foreach(DataRow reservoirObject in reservoirObjects)
                        {
                            int reservoirObjectId = System.Convert.ToInt32(reservoirObject["Объект"]);
                            if(!resultWell.PerforationObjects.Any(t => t == reservoirObjectId))
                            {
                                resultWell.PerforationObjects.Add(reservoirObjectId);
                            }
                        }
                    }
                    
                }
            }
            catch(Exception e)
            {
                throw;
            }
           
            return resultWell;
        }
       
    }
}
