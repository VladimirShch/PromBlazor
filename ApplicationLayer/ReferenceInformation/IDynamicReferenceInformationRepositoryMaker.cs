using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.ApplicationLayer.ReferenceInformation
{
    // Вероятно, не на этом уровне, так как получится, что класс с DataAccess, реализовывающий этот интерфейс, видит UI
    public interface IDynamicReferenceInformationRepositoryMaker
    {
        Task FillRepositoryAsync();
    }
}
