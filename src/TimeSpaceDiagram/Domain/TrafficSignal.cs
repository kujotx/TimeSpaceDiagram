namespace TimeSpaceDiagram.Domain
{
    /// <summary>
    /// An TrafficSignal is a single endpoint traffic system with inbound and outbound traffic flow along an arterial
    /// </summary>
    public class TrafficSignal
    {
        public TrafficSignal(int cycleLimit, string thoroughfare, string arterial, int phaseLength, int outboundOffset, int inboundOffset, int outboundDistance, int inboundDistance, int yellowLength, int speedLimit)
        {
            CycleLimit = cycleLimit;
            Thoroughfare = thoroughfare;
            Arterial = arterial;
            PhaseLength = phaseLength;
            OutboundOffset = outboundOffset;
            InboundOffset = inboundOffset;
            OutboundDistance = outboundDistance;
            InboundDistance = inboundDistance;
            YellowLength = yellowLength;
            SpeedLimit = speedLimit;
        }

        public int CycleLimit { get; set; }

        public string Thoroughfare { get; set; }

        public string Arterial { get; set; }

        public int PhaseLength { get; set; }

        public int OutboundOffset { get; set; }

        public int InboundOffset  { get; set; }

        public int OutboundDistance { get; set; }

        public int InboundDistance { get; set; }

        public int YellowLength { get; set; }

        public int SpeedLimit { get; set; }
    }
}
