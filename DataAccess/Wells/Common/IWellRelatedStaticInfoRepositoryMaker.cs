using System.Data;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Common
{
    public interface IWellRelatedStaticInfoRepositoryMaker
    {
        void MakeRepository(string fieldId, DataSet wellRelatedInfo);
    }
}
