namespace TimeSpaceDiagram.Interfaces
{
    using System.Collections.Generic;

    using TimeSpaceDiagram.Domain;

    public interface ITrafficSignalService
    {
        IList<TrafficSignal> GetTrafficSignals(string arterialName);
    }
}