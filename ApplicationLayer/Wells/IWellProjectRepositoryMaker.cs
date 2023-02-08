using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.ApplicationLayer.Wells
{
    // TODO: костыль, наверное, не должен на этом уровне находиться к тому же. Но другого выхода нет
    public interface IWellProjectRepositoryMaker
    {
        Task MakeRepositoryAsync();
    }
}
