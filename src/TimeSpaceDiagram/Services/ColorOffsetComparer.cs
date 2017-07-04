using System;
using System.Collections.Generic;
using System.Windows.Media;
using TimeSpaceDiagramControl.Controls;
using TimeSpaceDiagramControl.Domain;
using TimeSpaceDiagramControl.Interfaces;

namespace ControlExample.Services
{
    /// <summary>
    /// Orders ColorOffsets by offset, then by color according to:
    /// yellow > green > red > yellow
    /// </summary>
    public class ColorOffsetComparer : IComparer<ColorOffset>
    {
        private readonly Color _green;
        private Color _red;
        private Color _yellow;

        public ColorOffsetComparer()
        {
            _green = Colors.Green;
            _red = Colors.Red;
            _yellow = Colors.Yellow;
        }

        public ColorOffsetComparer(TrafficSignal intersection, IGradientColorManager gradientColorManager)
        {
            this.Intersection = intersection;
            _green = gradientColorManager.GetGreenColor();
            _red = gradientColorManager.GetRedColor();
            _yellow = gradientColorManager.GetYellowColor();
        }

        public TrafficSignal Intersection { get; set; }

        public int Compare(ColorOffset x, ColorOffset y)
        {
            decimal xoffset = decimal.Round(x.Offset * 1000, MidpointRounding.AwayFromZero);
            decimal yoffset = decimal.Round(y.Offset * 1000, MidpointRounding.AwayFromZero);

            if (xoffset != yoffset)
            {
                return xoffset.CompareTo(yoffset);
            }

            return CompareColors(x, y);
        }

        /// <summary>
        /// Decide order based on color: yellow > green > red > yellow
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int CompareColors(ColorOffset x, ColorOffset y)
        {
            if (x.Color == y.Color)
            {
                return 0;
            }

            if (x.Color == _green && y.Color == _red)
            {
                return 1;
            }

            if (x.Color == _red && y.Color == _yellow)
            {
                return 1;
            }

            if (x.Color == _yellow && y.Color == _green)
            {
                return 1;
            }

            return -1;
        }
    }
}