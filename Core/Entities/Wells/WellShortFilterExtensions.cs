using System;
using System.Collections.Generic;
using System.Linq;

namespace Web_Prom.Core.Blazor.Core.Entities.Wells
{
    public static class WellShortFilterExtensions
    {
        public static IEnumerable<WellShort> GetWellboresSatisfiedBy(this WellShort well, Func<WellShort, bool> predicate)
        {
            List<WellShort> satisfiedWellbores = new();
            if (predicate(well))
            {
                satisfiedWellbores.Add(well);
            }
            satisfiedWellbores.AddRange(well.ChildWellbores?.Where(wb => predicate(wb)) ?? Enumerable.Empty<WellShort>());
            return satisfiedWellbores;
        }

        public static bool IsSatisfiedBy(this WellShort well, Func<WellShort, bool> predicate)
        {
            return well.GetWellboresSatisfiedBy(predicate).Any();
        }
    }
}
