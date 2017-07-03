using System.Windows.Media;

namespace TimeSpaceDiagram.Interfaces
{
    public interface IGradientColorManager
    {
        Color GetOutboundFlowColor();
        Color GetInboundFlowColor();
        Color GetGreenColor();
        Color GetYellowColor();
        Color GetRedColor();
    }
}