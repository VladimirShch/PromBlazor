using Geolog.Wells.Trajectories;
using System;
using System.Collections.Generic;
using System.Linq;
using Web_Prom.Core.Blazor.Core.Entities.Trajectories;
using Web_Prom.Core.Blazor.DataAccess.Common;

namespace Web_Prom.Core.Blazor.DataAccess.Trajectories
{
    public class TrajectoryAdapter : IAdapter<IEnumerable<TrajectoryStationDto>?, ICollection<TrajectoryStation>>
    {
        public ICollection<TrajectoryStation> Convert(IEnumerable<TrajectoryStationDto>? trajectoryDto)
        {
            if(trajectoryDto is null)
            {
                return new List<TrajectoryStation>();
            }

            ICollection<TrajectoryStation> resultTrajectory = trajectoryDto.Where(t => t is not null).Select(Convert).ToList();
            return resultTrajectory;
        }

        public IEnumerable<TrajectoryStationDto>? ConvertBack(ICollection<TrajectoryStation> itemFrom)
        {
            throw new NotImplementedException();
        }

        private TrajectoryStation Convert(TrajectoryStationDto trajectoryStationDto)
        {
            if(trajectoryStationDto is null)
            {
                throw new ArgumentNullException(nameof(trajectoryStationDto));
            }

            var resultStation = new TrajectoryStation
            {
                Md = trajectoryStationDto.Md,
                Tvd = trajectoryStationDto.Tvd,
                Angle = trajectoryStationDto.Angle,
                Azimuth = trajectoryStationDto.Azimut,
                DX = trajectoryStationDto.DX,
                DY = trajectoryStationDto.DY
            };

            return resultStation;
        }
    }
}
