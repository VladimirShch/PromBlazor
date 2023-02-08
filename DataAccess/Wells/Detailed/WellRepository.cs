using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.ApplicationLayer.Credentials;
using Web_Prom.Core.Blazor.Core.Entities.ReferenceInformation;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;
using Web_Prom.Core.Blazor.Core.Entities.Wells.Detailed;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Detailed
{
    public class WellRepository : IWellRepository
    {
        private readonly Geolog.Contracts.IByArc _byArcService;
        private readonly UserCredentials _userCredentials;
        private readonly IWellAdapter _wellAdapter;
        private readonly IDynamicReferenceInformationRepository _referenceInformationRepository; // !!!TODO: костыль! Избавиться!

        public WellRepository(UserCredentials userCredentials, 
                              Geolog.Contracts.IByArc byArcService,
                              IWellAdapter wellAdapter,
                              IDynamicReferenceInformationRepository referenceInformationRepository)
        {
           // string argumentExceptionMessage = "Argument can not be null";

            _userCredentials = userCredentials ?? throw new ArgumentNullException(nameof(userCredentials));
            _byArcService = byArcService ?? throw new ArgumentNullException(nameof(byArcService));          
            _wellAdapter = wellAdapter ?? throw new ArgumentNullException(nameof(wellAdapter));
            _referenceInformationRepository = referenceInformationRepository ?? throw new ArgumentNullException(nameof(referenceInformationRepository));
        }

        // протестировать поведение, если fiedId, id = null или пустые или нечисловые строки
        // если сredentials валидные/невалидные
        public async Task<Well> Get(string fieldId, string id)
        {
            var fieldIdInt = Convert.ToInt32(fieldId);
            byte[] wellInfoRaw = await _byArcService.LoadWellBigInformationAsync(_userCredentials.Username, _userCredentials.Password, fieldIdInt, id);
            WellClass.Well wellDto = WellClass.ArhAndSer.ObjectFromArhiv<WellClass.Well>(wellInfoRaw);
            
            Well resultWell = _wellAdapter.Convert(wellDto);

            return resultWell;
        }

        public async Task Set(string fieldId, Well well)
        {
            IEnumerable<EquipmentType> equipmentTypes = _referenceInformationRepository.GetEquipmentTypes();

            WellClass.Well wellDto = _wellAdapter.ConvertBack(fieldId, well, equipmentTypes);
            byte[] wellBytes = WellClass.ArhAndSer.ObjectToArchive(wellDto);
            //--
            // TODO: int.Parse - небезопасно
            _ = await _byArcService.SaveWellAndGetUwiAsync(_userCredentials.Username, _userCredentials.Password, int.Parse(fieldId), wellBytes);
            //--
            //return Task.CompletedTask;
        }
        // По сути костыль, то же самое, что и Set просто с возвратом Uwi
        public async Task<string> CreateWell(string fieldId, Well well)
        {
            IEnumerable<EquipmentType> equipmentTypes = _referenceInformationRepository.GetEquipmentTypes();

            WellClass.Well wellDto = _wellAdapter.ConvertBack(fieldId, well, equipmentTypes);
            byte[] wellBytes = WellClass.ArhAndSer.ObjectToArchive(wellDto);
            //--
            // TODO: int.Parse - небезопасно
            string uwi = await _byArcService.SaveWellAndGetUwiAsync(_userCredentials.Username, _userCredentials.Password, int.Parse(fieldId), wellBytes);
            //--
            return uwi;
        }
    }
}
