using Geolog.Wells.Trajectories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Prom.Core.Blazor.Core.Entities.Trajectories;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Trajectories
{
    public class TrajectoryRepository : ITrajectoryRepository
    {
        private readonly ITrajectoryAppService _trajectoryAppService;
        private readonly IAdapter<IEnumerable<TrajectoryStationDto>?, ICollection<TrajectoryStation>> _trajectoryAdapter;

        public TrajectoryRepository(ITrajectoryAppService trajectoryAppService, IAdapter<IEnumerable<TrajectoryStationDto>?, ICollection<TrajectoryStation>> trajectoryAdapter)
        {
            _trajectoryAppService = trajectoryAppService;
            _trajectoryAdapter = trajectoryAdapter;
        }

        public async Task<ICollection<TrajectoryStation>> Get(string uwi)
        {
            IEnumerable<TrajectoryStationDto> trajectoryDto = await _trajectoryAppService.GetAsync(uwi);
            ICollection<TrajectoryStation> resultTrajectory = _trajectoryAdapter.Convert(trajectoryDto);

            return resultTrajectory;
        }
    }
}
