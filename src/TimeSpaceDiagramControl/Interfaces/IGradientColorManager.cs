namespace TimeSpaceDiagramControl.Interfaces
{
    using System.Windows.Media;

    public interface IGradientColorManager
    {
        Color GetOutboundFlowColor();
        Color GetInboundFlowColor();
        Color GetGreenColor();
        Color GetYellowColor();
        Color GetRedColor();
    }
}