using System.Windows.Media;
using TimeSpaceDiagramControl.Interfaces;

namespace ControlExample.Services
{
    /// <summary>
    /// Determines what colors are used for phasing by using hard-coded regular, ol stop light colors.
    /// </summary>
    public class HardCodedColorManager : IGradientColorManager
    {
        public Color GetInboundFlowColor()
        {
            return Colors.AliceBlue;
        }

        /// <summary>
        /// This is the color used for the coordinated phases (the green lights).
        /// </summary>
        /// <returns></returns>
        public Color GetGreenColor()
        {
            return Colors.ForestGreen;
        }

        /// <summary>
        /// This is the color used for the flow bandwidths bands.
        /// </summary>
        /// <returns></returns>
        public Color GetOutboundFlowColor()
        {
            return Colors.LightSalmon;
        }

        /// <summary>
        /// This is the color for yellow lights.
        /// </summary>
        /// <returns></returns>
        public Color GetYellowColor()
        {
            return Colors.Gold;
        }

        /// <summary>
        /// This is the color for red lights.
        /// </summary>
        /// <returns></returns>
        public Color GetRedColor()
        {
            return Colors.Crimson;
        }
    }
}