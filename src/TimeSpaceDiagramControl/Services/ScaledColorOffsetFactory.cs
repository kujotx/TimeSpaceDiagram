namespace TimeSpaceDiagramControl.Services
{
    using System.Windows.Media;
    using TimeSpaceDiagramControl.Controls;

    public static class ScaledColorOffsetFactory
    {
        public static ColorOffset Create(Color color, int offset, int total)
        {
            return new ColorOffset(color, CycleCalculationUtility.PercentageOfCycle(offset, total));
        }
    }
}