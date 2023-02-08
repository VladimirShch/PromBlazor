using Web_Prom.Core.Blazor.Core.Entities.Horizons;

namespace Web_Prom.Core.Blazor.ApplicationLayer.Wells
{
    // TODO: костыль, наверное, не должен на этом уровне находиться к тому же. Но другого выхода нет.
    // Раньше Maker был на DataAccess-уровне, а RepositoryAndMaker - на UI. Но вызывается Make и в UI тоже, поэтому получалось, что UI видит DataAccess
    public interface IWellProjectRepositoryAndMaker : IWellProjectRepository, IWellProjectRepositoryMaker
    {
    }
}
