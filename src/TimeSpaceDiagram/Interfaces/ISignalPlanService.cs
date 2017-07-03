namespace TimeSpaceDiagram.Interfaces
{
    using TimeSpaceDiagram.Domain;

    public interface ISignalPlanService
    {
        SignalPlan CreateSignalPlan(int cycles, string thoroughfareName);
    }
}