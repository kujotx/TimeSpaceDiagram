namespace TimeSpaceDiagramControl.Domain
{
    /// <summary>
    /// An TrafficSignal is a single endpoint traffic system with upstream and downstream traffic flow along an arterial
    /// </summary>
    public class TrafficSignal
    {
        public TrafficSignal(int cycleLimit, string thoroughfare, string arterial, int phaseLength, int downstreamOffset, int upstreamOffset, int downstreamDistance, int upstreamDistance, int yellowLength, int speedLimit)
        {
            CycleLimit = cycleLimit;
            Thoroughfare = thoroughfare;
            Arterial = arterial;
            PhaseLength = phaseLength;
            DownstreamOffset = downstreamOffset;
            UpstreamOffset = upstreamOffset;
            DownstreamDistance = downstreamDistance;
            UpstreamDistance = upstreamDistance;
            YellowLength = yellowLength;
            SpeedLimit = speedLimit;
        }

        public int CycleLimit { get; set; }

        public string Thoroughfare { get; set; }

        public string Arterial { get; set; }

        public int PhaseLength { get; set; }

        public int DownstreamOffset { get; set; }

        public int UpstreamOffset  { get; set; }

        public int DownstreamDistance { get; set; }

        public int UpstreamDistance { get; set; }

        public int YellowLength { get; set; }

        public int SpeedLimit { get; set; }
    }
}
