using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.DataAccess.Common;
using Web_Prom.Core.Blazor.Core.Entities.Wells;
using Web_Prom.Core.Blazor.DataAccess.Wells.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Short
{
    public class WellShortRepositoryClient : IWellShortRepository
    {
        private readonly Communication.Client _spravService;
        private readonly UserCredentials _userCredentials;
        private readonly IWellShortAdapter _wellShortAdapter;
        private readonly IWellRelatedStaticInfoRepositoryMaker _wellRelatedStaticInfoRepositoryMaker; // Костыль(- избавиться) для заполнения репозитория ГП при однократном обращении к серверу, так как ручка на сервере для списка ГП и списка скважин - одна и та же

        public WellShortRepositoryClient(UserCredentials userCredentials, Communication.Client spravService, IWellShortAdapter commonWellAdapter,
            IWellRelatedStaticInfoRepositoryMaker wellRelatedStaticInfoRepositoryMaker)
        {
            _userCredentials = userCredentials;
            _spravService = spravService;
            _wellShortAdapter = commonWellAdapter;

            _wellRelatedStaticInfoRepositoryMaker = wellRelatedStaticInfoRepositoryMaker;
        }

        public async Task<IEnumerable<WellShort>> GetAll(string fieldId)
        {
            IEnumerable<WellShort> wells;
            try
            {
                var fieldIdInt = Convert.ToInt32(fieldId);
                byte[] wellsContent = await _spravService.LoadWellListWithParentAsync(fieldIdInt, true);
                DataSet dataSet = WellClass.ArhAndSer.ObjectFromArhiv<DataSet>(wellsContent);
                dataSet.AcceptChanges();

                wells = _wellShortAdapter.Convert(dataSet);

                // Костыли, нарушение - помимо собственно получения списка скважин, ещё и репозиторий ГП заполняется
                _wellRelatedStaticInfoRepositoryMaker.MakeRepository(fieldId, dataSet); //создание репозитория ГП

                return wells;
            }
            catch (Exception ex)
            {
                // Залогировать
                throw new RepositoryException("Ошибка получения данных", ex);
            }

        }




    }
}
