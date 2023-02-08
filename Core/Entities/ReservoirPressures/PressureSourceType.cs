
namespace Web_Prom.Core.Blazor.Core.Entities.ReservoirPressures
{
    public enum PressureSourceType
    {
        Undefined,
        MiddlePerforationPoint, //SIP,       
        DatumPlane, //Plosk, // ? плоскость приведения? - какой правильный перевод?
        Bottomhole, //Zaboy,
        Tubing, //NKT,
        Wellhead, //Bufer,
        DeepSurvey //Glub  
    }
}
