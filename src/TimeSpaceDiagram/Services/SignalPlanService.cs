namespace TimeSpaceDiagram.Services
{
    using TimeSpaceDiagramControl.Domain;
    using TimeSpaceDiagramControl.Interfaces;
    using System.Collections.Generic;

    public class SignalPlanService : ISignalPlanService
    {
        private readonly ITrafficSignalService _intersectionService;

        public SignalPlanService(ITrafficSignalService intersectionService)
        {
            _intersectionService = intersectionService;
        }

        /// <summary>
        /// Retrieve the signal plan from the arterial
        /// </summary>
        /// <param name="cycles">The number of cycles</param>
        /// <param name="thoroughfareName">Name of the arterial to be planned</param>
        /// <returns>A signal plan object</returns>
        public SignalPlan CreateSignalPlan(int cycles, string thoroughfareName)
        {
            // TODO Provide a more robust repository method for getting intersections
            IList<TrafficSignal> intersections = _intersectionService.GetTrafficSignals(thoroughfareName); 
            IEnumerable<Segment> segments = CreateSegments(intersections, cycles);
            SignalPlan signalPlan = new SignalPlan(segments, cycles);
            return signalPlan;
        }

        /// <summary>
        /// Provide a collection of straightaways for the intersections along a throughfare.
        /// </summary>
        /// <param name="trafficSignals">A collection of traffic signals</param>
        /// <param name="cycles">The number of cycles to display</param>
        /// <returns>A collection of straightaways for the intersections provided</returns>
        private static IEnumerable<Segment> CreateSegments(IList<TrafficSignal> trafficSignals, int cycles)
        {
            var straightaways = new List<Segment>();

            for (int i = 0; i < trafficSignals.Count - 1; i++)
            {
                var straightaway = new Segment(trafficSignals[i], trafficSignals[i + 1], cycles, trafficSignals[i].SpeedLimit, trafficSignals[i].CycleLimit);
                straightaways.Add(straightaway);
            }
            return straightaways;
        }
    }
}