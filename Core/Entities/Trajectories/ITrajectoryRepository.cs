using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_Prom.Core.Blazor.Core.Entities.Trajectories
{
    public interface ITrajectoryRepository
    {
        public Task<ICollection<TrajectoryStation>> Get(string uwi);
    }
}
