using System.Collections.Generic;
using TimeSpaceDiagram.Interfaces;
using TimeSpaceDiagram.Domain;

namespace TimeSpaceDiagram.Services.Intersections
{
    /// <summary>
    /// IntersectionService returns all cross-streets for a given arterialName
    /// TODO You should be able to extend this with methods that list all
    /// cross streets between (and including) two cross streets.
    /// </summary>
    public class FakeIntersectionService : ITrafficSignalService
    {
        public IList<TrafficSignal> GetTrafficSignals(string arterialName)
        {
            int offset = 0;
            var intersections = new List<TrafficSignal>
                {
                    new TrafficSignal(120, arterialName, "Then", 60, offset, 0, 1300, 800, 2, 40),
                    new TrafficSignal(120, arterialName, "Dairy-Ashford", 60, offset+50, 0, 1200, 1300, 2, 40),
                    new TrafficSignal(120, arterialName, "Briar West", 60, offset+100, 0, 1000, 1200, 2, 40),
                    new TrafficSignal(120, arterialName, "Synott", 60, 30, 0, 800, 1000, 2, 40),
                    new TrafficSignal(120, arterialName, "Eldridge", 60, 80, 0, 1400, 800, 2, 40),
                    new TrafficSignal(120, arterialName, "Next", 60, 10, 0, 800, 1400, 2, 40)
                };

            return intersections;
        }
    }
}