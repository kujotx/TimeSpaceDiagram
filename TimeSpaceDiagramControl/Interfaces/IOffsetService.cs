namespace TimeSpaceDiagramControl.Interfaces
{
    using System.Collections.Generic;
    using TimeSpaceDiagramControl.Controls;
    using TimeSpaceDiagramControl.Domain;

    public interface IOffsetService
    {
        IEnumerable<ColorOffset> GetColorOffsets(TrafficSignal intersection, TrafficDirection trafficDirection);
    }
}