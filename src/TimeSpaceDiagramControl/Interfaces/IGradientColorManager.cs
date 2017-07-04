namespace TimeSpaceDiagramControl.Interfaces
{
    using System.Windows.Media;

    public interface IGradientColorManager
    {
        Color GetDownstreamFlowColor();
        Color GetUpstreamFlowColor();
        Color GetGreenColor();
        Color GetYellowColor();
        Color GetRedColor();
    }
}