using System;
using Web_Prom.Core.Blazor.Core.Entities.Wells.Enums;

namespace Web_Prom.Core.Blazor.DataAccess.Wells.Common
{
    // TODO: оценить правильность такого решения, подумать, чем заменить
    // !! методы по идее должны быть статики
    public class WellPropertiesConverter
    {
        public WellState ConvertState(int dbWellState)
        {
            return dbWellState switch
            {
                0 => WellState.Active,
                1 => WellState.WaitingWorkingOver,
                2 => WellState.WaitingPluggingAccepted,
                3 => WellState.WaitingInitializingAccepted,
                4 => WellState.WaitingPluggingInaccepted,
                5 => WellState.Initializing,
                6 => WellState.WaitingInitializingInaccepted,
                7 => WellState.Drilling,
                8 => WellState.Project,
                9 => WellState.Conserved,
                110 => WellState.Inactive,
                120 => WellState.WaitingAbandoning,
                130 => WellState.Abandoned,
                111 => WellState.WorkingOver,
                _ => WellState.Unknown//throw new ArgumentException($"Unknown dbState: {dbState}")
            };
        }

        public bool ConvertBalance(DateTime dateBalance) => dateBalance > new DateTime(1900, 1, 1);

        public  WellType ConvertType(int dbWellType)
        {
            return dbWellType switch
            {
                629239 => WellType.Production,
                42411 => WellType.ObservationPerforated,
                42412 => WellType.ObservationNotPerforated,
                7790 => WellType.Absorbing,
                173070 => WellType.Piezometric,
                177350 => WellType.Exploration,
                8650 => WellType.Appraisal,
                175010 => WellType.Permafrost,
                _ => WellType.Unknown
            };
        }

        public int ConvertTypeBack(WellType wellType)
        {
            return wellType switch
            {
                WellType.Production => 629239,
                WellType.ObservationPerforated => 42411,
                WellType.ObservationNotPerforated => 42412,
                WellType.Absorbing => 7790,
                WellType.Piezometric => 173070,
                WellType.Exploration => 177350,
                WellType.Appraisal => 8650,
                WellType.Permafrost => 175010,
                WellType.Unknown => -999,
                _ => -999
            };
        }

        public WellShape ConvertShape(int dbWellShape)
        {
            return dbWellShape switch
            {
                507002 => WellShape.Vertical,
                507004 => WellShape.Inclined,
                507006 => WellShape.Subhorizontal,
                507008 => WellShape.Horizontal,
                _ => WellShape.Unknown
            };
        }

        public int ConvertShapeBack(WellShape wellShape)
        {
            return wellShape switch
            {
                WellShape.Vertical => 507002,
                WellShape.Inclined=> 507004,
                WellShape.Subhorizontal=> 507006,
                WellShape.Horizontal=> 507008,
                WellShape.Unknown => -999,
                _ => -999
            };
        }
    }
}
