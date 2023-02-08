using System;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.Entities.ReservoirPressures;

namespace Web_Prom.Core.Blazor.DataAccess.ReservoirPressures
{
    public class ReservoirPressureInformationRepository : IReservoirPressureInformationRepository
    {
        private readonly UserCredentials _userCredentials;
        private readonly Geolog.Contracts.IByArc _byArcService;
        private readonly Geolog.Contracts.IGrafic _graficService;
        private readonly IReservoirPressureInformationAdapter _adapter; // Это необязательно должен быть класс реализующий именно интерфейс IAdapter<t1,t2>.
        //Он может не подойти для твоих целей, тогда разработай свой интерфейс. Не забудь зарегистрировать в ModuleInitializer
        
        // Также потребуется адаптер, переводящий полученный с сервера WellClass.Well в тот класс(или классы), которые ты 
        // разработаешь специально для работы с приведением пластовых давлений в этой клиентской программе.
        // Так что, если WellClass поменяется, в этой программе нужно будет поправить только лишь один класс адаптера, ну или разработать новый.
        public ReservoirPressureInformationRepository(UserCredentials userCredentials, Geolog.Contracts.IByArc byArcService, Geolog.Contracts.IGrafic graficService, IReservoirPressureInformationAdapter adapter)
        {
            _userCredentials = userCredentials ?? throw new ArgumentNullException(nameof(userCredentials));
            _byArcService = byArcService ?? throw new ArgumentNullException(nameof(byArcService));
            _graficService = graficService ?? throw new ArgumentNullException(nameof(graficService));
            _adapter = adapter;
        }

        public async Task<ReservoirPressureInformation> Get(string fieldId, string wellId, string modelId)
        {
            int numericFieldId = Convert.ToInt32(fieldId);
            int numericModelId = Convert.ToInt32(modelId);

            byte[] resultBytes = await _byArcService.LoadIsslsAsync(_userCredentials.Username, _userCredentials.Password, numericFieldId, wellId, numericModelId);
            // Нарушение, но ладно
            WellClass.Well wellDto = WellClass.ArhAndSer.ObjectFromArhiv<WellClass.Well>(resultBytes);

            ReservoirPressureInformation result = _adapter.Convert(wellDto);
            return result;
        }

        public async Task Set(ReservoirPressureInformation pressureInformation)
        {
            Geolog.Contracts.IGrafic.SaveIsslsRequest pressureInformationDto = _adapter.ConvertBack(pressureInformation);
            pressureInformationDto.UserName = _userCredentials.Username;
            _ = await _graficService.SaveIsslsAsync(pressureInformationDto);
        }

        // Заменить на нормальный метод, который будет нужен тебе, и будет описан в IReservoirPressureInformationRepository
        // Я предпочитаю использовать в программе идентификаторы строковые, так, мне кажется, выше гибкость.
        // Например, когда мы писали Ярус на PPDM, там везде использовались строковые Guid вида XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
        // Поэтому, например, если вдруг придётся быстро перейти на PPDM, в программе ничего не придётся менять, кроме вот этих репозиториев и адаптеров
        //public async Task GetSomething(string fieldId, string wellId, string modelId)
        //{
        //    // Да, может быть Exception, по-хорошему, так не надо
        //    int numericFieldId = Convert.ToInt32(fieldId);
        //    int numericModelId = Convert.ToInt32(modelId);

        //    byte[] resultBytes = await _byArcService.LoadIsslsAsync(_userCredentials.Username, _userCredentials.Password, numericFieldId, wellId, numericModelId);
        //    // Нарушение, но ладно
        //    WellClass.Well wellDto = WellClass.ArhAndSer.ObjectFromArhiv<WellClass.Well>(resultBytes);
        //    // Дальше с помощью адаптера (ну или прямо здесь, что, конечно, ошибка, но ладно) преобразуешь wellDto в свой класс из Core и возвращаешь его 
        //}
    }
}
