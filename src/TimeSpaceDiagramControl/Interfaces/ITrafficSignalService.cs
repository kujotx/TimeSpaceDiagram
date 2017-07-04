namespace TimeSpaceDiagramControl.Interfaces
{
    using System.Collections.Generic;
    using TimeSpaceDiagramControl.Domain;

    public interface ITrafficSignalService
    {
        IList<TrafficSignal> GetTrafficSignals(string arterialName);
    }
}