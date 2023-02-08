using Web_Prom.Core.Blazor.Core.Entities.ReferenceInformation;

namespace Web_Prom.Core.Blazor.ApplicationLayer.ReferenceInformation
{
    // Вероятно, не на этом уровне, так как получится, что класс с DataAccess, реализовывающий этот интерфейс, видит UI
    public interface IDynamicReferenceInformationRepositoryAndMaker : IDynamicReferenceInformationRepository, IDynamicReferenceInformationRepositoryMaker
    {
    }
}
