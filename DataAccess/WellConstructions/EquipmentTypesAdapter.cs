using System;
using System.Collections.Generic;
using System.Data;
using Web_Prom.Core.Blazor.Core.Entities.WellConstructions;
using Web_Prom.Core.Blazor.DataAccess.Common;
using SystemConvert = System.Convert;

namespace Web_Prom.Core.Blazor.DataAccess.WellConstructions
{
    public class EquipmentTypesAdapter : IAdapter<DataTable?, ICollection<EquipmentType>>
    {
        public ICollection<EquipmentType> Convert(DataTable? codesDto)
        {
            var equipmentTypes = new List<EquipmentType>();
            if (codesDto is null)
            {
                return equipmentTypes;
            }

            foreach (DataRow codeDto in codesDto.Rows)
            {
                EquipmentType equipmentType = Convert(codeDto);
                // Костыль с null
                if (equipmentType is not null)
                {
                    equipmentTypes.Add(equipmentType);
                }               
            }

            return equipmentTypes;

        }

        public DataTable? ConvertBack(ICollection<EquipmentType> itemFrom)
        {
            throw new NotImplementedException();
        }

        // Костыль с nullable и try-catch - переделать!!! иначе мы во первых возможно теряем часть оборудования, во вторых не отличаем то, которое не хотим добавлять от того, например, которе уже добавили в базу, но не добавили в enum
        private EquipmentType? Convert(DataRow codeDto)
        {
            if(codeDto is null)
            {
                throw new ArgumentNullException(nameof(codeDto));
            }
            var equipmentKind = Convert(SystemConvert.ToInt32(codeDto["GRP"]));
            if(equipmentKind == WellEquipmentKind.Pipe)
            {
                try
                {
                    equipmentKind = Convert(SystemConvert.ToInt32(codeDto["ID"]));
                }
                catch(Exception e)
                {
                    return null;
                }
                
            }
            var equipmentType = new EquipmentType
            (
                id : SystemConvert.ToInt32(codeDto["ID"]),
                name : SystemConvert.ToString(codeDto["Name"]),
                kind : equipmentKind
            );

            return equipmentType;
        }

        // Вынести в отдельный класс
        private WellEquipmentKind Convert(int equipmentDtoGrp)
        {
            return equipmentDtoGrp switch
            {
                500001 => WellEquipmentKind.WellheadEquipment,
                500100 => WellEquipmentKind.CasingHead,
                500200 => WellEquipmentKind.Packer,
                500300 => WellEquipmentKind.SafetyValve,
                3730 => WellEquipmentKind.Pipe,
                1118101 => WellEquipmentKind.Conductor,
                1118121 => WellEquipmentKind.SurfaceCasing,
                1118141 => WellEquipmentKind.IntermediateCasing,
                1118161 => WellEquipmentKind.Casing,
                1180961 => WellEquipmentKind.CasingAdditional,
                1118181 => WellEquipmentKind.Liner,
                1118201 => WellEquipmentKind.Tubing,
                1118301 => WellEquipmentKind.ConcentricTubing,
                1118281 => WellEquipmentKind.Filter,
                500110 => WellEquipmentKind.CementPlug,
                500120 => WellEquipmentKind.Other,
                _ => throw new ArgumentException("Unknown dto code", nameof(equipmentDtoGrp))
            };
        }
    }
}
