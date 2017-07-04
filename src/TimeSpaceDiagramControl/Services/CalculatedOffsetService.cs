using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using TimeSpaceDiagramControl.Controls;
using TimeSpaceDiagramControl.Domain;
using TimeSpaceDiagramControl.Interfaces;

namespace TimeSpaceDiagramControl.Services
{
    /// <summary>
    /// Creates color offsets for a linear gradient bar to represent traffic cycles in a time-space diagram
    /// </summary>
    public class CalculatedOffsetService : IOffsetService
    {
        // colorOffsets contains our results
        private List<ColorOffset> _colorOffsets = new List<ColorOffset>();
        private int _adjustedGreenStop; // green stop interval adjusted for cycle length
        private int _actualGreenStop; // green stop unadkisted for cycle length
        
        private int _actualYellowStop; // yellow stop interval unadjusted for cycle length
        private int _adjustedYellowStop; // yellow stop interval adjusted for cycle length

        private IGradientColorManager _gradientColorManager;
        private Color _green;
        private Color _red;
        private Color _yellow;
        private int _offset;

        public CalculatedOffsetService(IGradientColorManager gradientColorManager)
        {
            this._gradientColorManager = gradientColorManager;
            this._green = _gradientColorManager.GetGreenColor();
            this._yellow = _gradientColorManager.GetYellowColor();
            this._red = _gradientColorManager.GetRedColor();
        }

        public IEnumerable<ColorOffset> GetColorOffsets(TrafficSignal intersection, TrafficDirection trafficDirection)
        {
            _offset = trafficDirection == TrafficDirection.Inbound ? intersection.InboundOffset : intersection.OutboundOffset;
            
            _adjustedGreenStop = GetAdjustedGreenStop(intersection);
            _adjustedYellowStop = GetAdjustedYellowStop(intersection);

            _actualGreenStop = _offset + intersection.PhaseLength;
            _actualYellowStop = _actualGreenStop + intersection.YellowLength;

            return AddColorOffsetRanges(intersection);
        }

        /// <summary>
        /// Populates the result with our color offset collections and ordered them
        /// </summary>
        /// <param name="intersection"></param>
        /// <returns></returns>
        private IEnumerable<ColorOffset> AddColorOffsetRanges(TrafficSignal intersection)
        {
            _colorOffsets = new List<ColorOffset>();
            _colorOffsets.AddRange(GetYellows(intersection));
            _colorOffsets.AddRange(GetReds(intersection));
            _colorOffsets.AddRange(GetGreens(intersection));

            IOrderedEnumerable<ColorOffset> orderedOffsets = this._colorOffsets.OrderByDescending(c => c, new ColorOffsetComparer(intersection, new HardCodedColorManager()));

            return orderedOffsets;
        }

        /// <summary>
        /// Calculate when the phase plus the yellow length ends relative to the cycle length
        /// </summary>
        /// <param name="intersection"></param>
        /// <returns></returns>
        private int GetAdjustedYellowStop(TrafficSignal intersection)
        {
            var regular = _offset + intersection.PhaseLength + intersection.YellowLength;
            if (regular > intersection.CycleLimit)
            {
                return regular - intersection.CycleLimit;
            }
            return regular;
        }

        /// <summary>
        /// Calculates when the phase ends relative to the cycle length
        /// </summary>
        /// <param name="intersection"></param>
        /// <returns></returns>
        private int GetAdjustedGreenStop(TrafficSignal intersection)
        {
            int regular = _offset + intersection.PhaseLength;
            
            if (regular > intersection.CycleLimit)
            {
                return regular - intersection.CycleLimit;
            }

            return regular;
        }

        /// <summary>
        /// Returns a collection of the green ColorOffsets
        /// </summary>
        /// <param name="intersection"></param>
        /// <returns></returns>
        private IEnumerable<ColorOffset> GetGreens(TrafficSignal intersection)
        {
            IList<ColorOffset> greens = new List<ColorOffset>();

            greens.Add(ScaledColorOffsetFactory.Create(_green, _offset, intersection.CycleLimit));
            greens.Add(ScaledColorOffsetFactory.Create(_green, _adjustedGreenStop, intersection.CycleLimit));

            if (_actualGreenStop > intersection.CycleLimit)
            {
                greens.Add(new ColorOffset(_green, 1m));
                greens.Add(new ColorOffset(_green, 0m));
            }

            return greens;
        }

        /// <summary>
        /// Returns a collection of red color offsets
        /// </summary>
        /// <param name="intersection"></param>
        /// <returns></returns>
        private IEnumerable<ColorOffset> GetReds(TrafficSignal intersection)
        {
            IList<ColorOffset> reds = new List<ColorOffset>();

            if (_offset == 0 && _actualYellowStop < intersection.CycleLimit)
            {
                reds.Add(ScaledColorOffsetFactory.Create(_red, _actualYellowStop, intersection.CycleLimit));
                reds.Add(new ColorOffset(_red, 1m));
            }

            if (_offset > 0 && _actualYellowStop < intersection.CycleLimit)
            {
                reds.Add(ScaledColorOffsetFactory.Create(_red, _actualYellowStop, intersection.CycleLimit));
                reds.Add(new ColorOffset(_red, 1m));
                reds.Add(new ColorOffset(_red, 0m));
                reds.Add(ScaledColorOffsetFactory.Create(_red, _offset, intersection.CycleLimit));
            }

            if (_offset > 0 && _actualYellowStop == intersection.CycleLimit)
            {
                reds.Add(new ColorOffset(_red, 0m));
                reds.Add(ScaledColorOffsetFactory.Create(_red, _offset, intersection.CycleLimit));
            }

            if (_offset > 0 && _actualYellowStop > intersection.CycleLimit && _actualGreenStop <= intersection.CycleLimit)
            {
                reds.Add(ScaledColorOffsetFactory.Create(_red, _adjustedYellowStop, intersection.CycleLimit));
                reds.Add(ScaledColorOffsetFactory.Create(_red, _offset, intersection.CycleLimit));
            }

            if (_offset > 0 && _actualYellowStop > intersection.CycleLimit && _actualGreenStop > intersection.CycleLimit)
            {
                reds.Add(ScaledColorOffsetFactory.Create(_red, _adjustedYellowStop, intersection.CycleLimit));
                reds.Add(ScaledColorOffsetFactory.Create(_red, _offset, intersection.CycleLimit));
            }
            
            return reds;
        }

        private IEnumerable<ColorOffset> GetYellows(TrafficSignal intersection)
        {
            IList<ColorOffset> yellows = new List<ColorOffset>();

            yellows.Add(ScaledColorOffsetFactory.Create(_yellow, _adjustedGreenStop, intersection.CycleLimit));
            yellows.Add(ScaledColorOffsetFactory.Create(_yellow, _adjustedYellowStop, intersection.CycleLimit));

            if (_actualGreenStop < intersection.CycleLimit && _actualYellowStop >= intersection.CycleLimit)
            {
                yellows.Add(new ColorOffset(_yellow, 1m));
            }

            if (_actualGreenStop < intersection.CycleLimit && _actualYellowStop == intersection.CycleLimit)
            {
                yellows.Add(new ColorOffset(_yellow, 0m));
            }

            return yellows;
        }
    }
}