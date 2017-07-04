namespace TimeSpaceDiagramControl.Interfaces
{
    using TimeSpaceDiagramControl.Domain;

    public interface ISignalPlanService
    {
        SignalPlan CreateSignalPlan(int cycles, string thoroughfareName);
    }
}