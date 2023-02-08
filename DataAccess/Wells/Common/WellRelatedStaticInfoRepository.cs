using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Common;
using Web_Prom.Core.Blazor.Core.Entities.Wells;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Common
{
    public class WellRelatedStaticInfoRepository : IWellRelatedStaticInfoRepository, IWellRelatedStaticInfoRepositoryMaker
    {
        private readonly List<ReferenceObject> _plants = new();
        private readonly List<ReferenceObject> _wellGroups = new();
        private readonly List<EntityHeader> _horizons = new();
        private readonly List<EntityHeader> _developmentObjects = new();
        private readonly List<EntityHeader> _reservoirs = new();
        private readonly List<EntityHeader> _perforationObjects = new();

        public void MakeRepository(string fieldId, DataSet wellRelatedInfo)
        {
            if (wellRelatedInfo.Tables["UKPG"] is not null)
            {
                _plants.Clear();

                foreach (DataRow plantRow in wellRelatedInfo.Tables["UKPG"].Rows)
                {
                    _plants.Add(new ReferenceObject(
                            plantRow["КодГП"].ToString().Trim(),
                            plantRow["Name"].ToString().Trim(),
                            fieldId
                    ));
                }
            }
            //---------------------------------------------------
            if (wellRelatedInfo.Tables["KUST"] is not null)
            {
                _wellGroups.Clear();

                foreach (DataRow wellGroupRow in wellRelatedInfo.Tables["KUST"].Rows)
                {
                    // TODO: ненадёжно!
                    if (Convert.ToInt64(wellGroupRow["Узел"]) == 0)
                    {
                        _wellGroups.Add(new ReferenceObject(
                            wellGroupRow["IDКуста"].ToString().Trim(),
                            wellGroupRow["ИмяКуста"].ToString().Trim(),
                            wellGroupRow["КодГП"].ToString().Trim()
                        ));
                    }
                }
            }
            //------------------------------------------------------
            if (wellRelatedInfo.Tables["Objects"] is not null)
            {
                _horizons.Clear();
                _developmentObjects.Clear();
                _reservoirs.Clear();
                _perforationObjects.Clear();

                foreach (DataRow objectRow in wellRelatedInfo.Tables["Objects"].Rows)
                {
                    int objectCode = (int)Convert.ToInt64(objectRow["Объект"]);
                    var objectHeader = new EntityHeader(
                              objectCode,
                              objectRow["Имя"].ToString().Trim());

                    if(objectCode < 1000 && !_perforationObjects.Any(t => t.Id == objectCode))
                    {
                        _perforationObjects.Add(objectHeader);
                    }
                    if (objectCode > 10 && objectCode < 100 && !_horizons.Any(t => t.Id == objectCode))
                    {
                        _horizons.Add(objectHeader);
                    }
                    else if (objectCode > 100 && objectCode < 1000 && !_developmentObjects.Any(t => t.Id == objectCode))
                    {
                        _developmentObjects.Add(objectHeader);
                    }
                    else if(((objectRow["Пласт"] is long reservoirCode && objectCode == reservoirCode) ||(objectCode >= 10000 && objectCode <= 99999)) && !_reservoirs.Any(t => t.Id == objectCode))
                    {
                        _reservoirs.Add(objectHeader);
                    }
                }
            }

        }

        public IEnumerable<EntityHeader> GetDevelopmentObjects()
        {
            return _developmentObjects;
        }

        public IEnumerable<EntityHeader> GetHorizons()
        {
            return _horizons;
        }
        public IEnumerable<EntityHeader> GetReservoirs()
        {
            return _reservoirs;
        }
        public IEnumerable<ReferenceObject> GetPlants()
        {
            return _plants;
        }

        public IEnumerable<ReferenceObject> GetWellGroups()
        {
            return _wellGroups;
        }

        public IEnumerable<EntityHeader> GetPerforationObjects()
        {
            return _perforationObjects;
        }
    }
}
