using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.Core.Entities.Common;
using Web_Prom.Core.Blazor.Core.Entities.Plants;

namespace Web_Prom.Core.Blazor.DataAccess.Plants
{
    // Не совсем понимаю, что я хотел сделать, и какой был план
    // Вообще это класс-заглушка, какое-то временное решение, заполняется списком ГП только для одного месторождения при вызове MakeRepository
    // В Ioc-контейнер добавляется как constant или singleton, с этим списком ГП потом и вызывается всю программу,
    // пока снова не вызовут MakeRepository
    public class PlantShortRepository : IPlantShortRepository, IPlantShortRepositoryMaker, IAllPlantsShortRepository
    {
        private readonly List<ReferenceObject> _plants = new();

        public void MakeRepository(string fieldId, DataTable plantsTable)
        {
            if(plantsTable.TableName != "UKPG")
            {
                throw new ArgumentException($"Wrong plantsTable name: {plantsTable.TableName}");
            }

            _plants.Clear();

            foreach (DataRow plantRow in plantsTable.Rows)
            {
                _plants.Add(ConvertPlant(plantRow, fieldId));
            }
           
        }

        // TODO: ужас-ужас, избавиться!
        public IEnumerable<ReferenceObject> GetPlants()
        {
            return _plants;
        }

        public IEnumerable<ReferenceObject> GetPlants(string fieldId)
        {
            return _plants.Where(t => t.ParentId == fieldId).ToList();
        }

        public Task<IEnumerable<ReferenceObject>> GetPlantsAsync(string fieldId)
        {
            return Task.FromResult(GetPlants(fieldId));
        }

        // Это вообще не отсюда
        private ReferenceObject ConvertPlant(DataRow plantRow, string fieldId)
        {
            return new ReferenceObject
            (
               plantRow["КодГП"].ToString().Trim(),
               plantRow["Name"].ToString().Trim(),
               fieldId
            );
        }      
    }
}
