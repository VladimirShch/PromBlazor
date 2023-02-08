
using Web_Prom.Core.Blazor.Core.Entities.ReservoirPressures;

namespace Web_Prom.Core.Blazor.DataAccess.ReservoirPressures
{
    public interface IReservoirPressureInformationAdapter
    {
        ReservoirPressureInformation Convert(WellClass.Well wellDto);
        Geolog.Contracts.IGrafic.SaveIsslsRequest ConvertBack(ReservoirPressureInformation reservoirPressureInformation);
    }
}
