namespace TimeSpaceDiagramControl.Services
{
    using System;

    public static class CycleCalculationUtility
    {
        public static decimal PercentageOfCycle(decimal seconds, decimal cycle)
        {
            if (seconds == 0m)
            {
                return 0m;
            }

            if (cycle == 0m)
            {
                throw new InvalidOperationException("Cycle is set to zero.");
            }

            return seconds / cycle;
        }
    }
}