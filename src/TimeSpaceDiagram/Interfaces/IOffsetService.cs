using TimeSpaceDiagram.Services;

namespace TimeSpaceDiagram.Interfaces
{
    using System.Collections.Generic;

    using Controls;
    using Domain;

    public interface IOffsetService
    {
        IEnumerable<ColorOffset> GetColorOffsets(TrafficSignal intersection, TrafficDirection trafficDirection);
    }
}