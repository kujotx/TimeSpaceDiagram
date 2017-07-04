using System.Windows.Media;
using TimeSpaceDiagramControl.Controls;

namespace TimeSpaceDiagram.Services
{
    public static class ScaledColorOffsetFactory
    {
        public static ColorOffset Create(Color color, int offset, int total)
        {
            return new ColorOffset(color, CycleCalculationUtility.PercentageOfCycle(offset, total));
        }
    }
}