using Web_Prom.Core.Blazor.Core.Entities.Common;

namespace Web_Prom.Core.Blazor.Core.Entities.WellConstructions
{
    public record EquipmentType : EntityHeader
    {
        public EquipmentType(int id, string name, WellEquipmentKind kind) : base(id, name)
        {
            Kind = kind;
        }

        public WellEquipmentKind Kind { get; init; }
    }
}
