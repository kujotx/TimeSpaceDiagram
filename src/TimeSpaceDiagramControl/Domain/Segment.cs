namespace TimeSpaceDiagramControl.Domain
{
    /// <summary>
    /// A portion of a facility on which a capacity analysis is performed; it is the basic unit for the 
    /// analysis, a one-directional distance.A segment is defined by two endpoints. 
    /// A Segment in our domain is the area in between two intersections along an arterial.
    /// For our domain, a Segment is bounded by two linear gradients for the cycles, with a 
    /// sloped bandwidth for each direction in the middle.
    /// </summary>
    public class Segment 
    {
        public Segment()
        {
        }

        public Segment(TrafficSignal downstreamIntersection, TrafficSignal upstreamIntersection, int cycleCount, int speedLimit, double cycleLength)
        {
            DownstreamIntersection = downstreamIntersection;
            UpstreamIntersection = upstreamIntersection;
            Distance = downstreamIntersection.OutboundDistance;
            SpeedLimit = speedLimit;
            CycleCount = cycleCount;
            CycleLength = cycleLength;
        }

        public TrafficSignal DownstreamIntersection { get; set; }

        public TrafficSignal UpstreamIntersection { get; set; }

        /// <summary>
        /// The distance between the two TrafficSignal objects
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// The speed limit between the two Traffic Signals
        /// </summary>
        public double SpeedLimit { get; set; }

        /// <summary>
        /// The count of cycles to present on-screen
        /// </summary>
        public int CycleCount { get; set; }

        /// <summary>
        /// The time required for a complete sequence of signal indications. 
        /// </summary>
        public double CycleLength { get; set; }
    }
}