// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cycle.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for Cycle.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TimeSpaceDiagramControl.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using TimeSpaceDiagramControl.Converters;
    using TimeSpaceDiagramControl.Domain;
    using TimeSpaceDiagramControl.Interfaces;

    /// <summary>
    /// Interaction logic for Cycle.xaml
    /// </summary>
    public partial class Cycle : UserControl
    {

        // Dependency Property
        public static readonly DependencyProperty SegmentProperty =
             DependencyProperty.Register("Segment", typeof(Segment),
             typeof(Cycle), new FrameworkPropertyMetadata(new Segment()));

        private readonly IOffsetService _offsetService;
        private readonly IGradientColorManager _gradientColorManager;
        private readonly Color _green;
        private readonly Color _downstreamFlowColor;
        private readonly Color _upstreamFlowColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cycle"/> class.
        /// </summary>
        public Cycle(Segment segment, IOffsetService offsetService, IGradientColorManager gradientColorManager)
        {
            InitializeComponent();
            _gradientColorManager = gradientColorManager;
            _green = _gradientColorManager.GetGreenColor();
            _downstreamFlowColor = _gradientColorManager.GetDownstreamFlowColor();
            _upstreamFlowColor = _gradientColorManager.GetUpstreamFlowColor();
            Segments = segment;
            _offsetService = offsetService;
            CreateConverters(segment);
            CreateGradientStops();
        }

        private Segment Segments
        {
            get { return (Segment)GetValue(SegmentProperty); }
            set { SetValue(SegmentProperty, value); }
        }

        /// <summary>
        /// Creates Gradient Offsets in the bar and flow rectangles for both directions.
        /// </summary>
        private void CreateGradientStops()
        {
            AddGradientStops(Segments.UpstreamIntersection, DownstreamPhaseBar, DownstreamFlow, TrafficDirection.Downstream);
            AddGradientStops(Segments.DownstreamIntersection, UpstreamPhaseBar, UpstreamFlow, TrafficDirection.Upstream);
        }

        /// <summary>
        /// Configure the converters that set the speed angle of the bandwidth bars
        /// TODO Find out how to inject contracts so we can switch out the standard converter for a metric one
        /// </summary>
        /// <param name="segment"></param>
        private void CreateConverters(Segment segment)
        {
            CycleConverter cycleConverter = CycleGrid.Resources["cycleConverter"] as CycleConverter;
            cycleConverter.Cycles = segment.CycleCount;

            CycleBackgroundConverter cycleBackgroundConverter = CycleGrid.Resources["cycleBackgroundConverter"] as CycleBackgroundConverter;
            cycleBackgroundConverter.Cycles = segment.CycleCount;
            
            SpeedLimitAngleConverter angleConverter = CycleGrid.Resources["angleConverter"] as SpeedLimitAngleConverter;
            angleConverter.Distance = segment.Distance;
            angleConverter.CycleLength = segment.CycleLength;
            angleConverter.SpeedLimit = segment.SpeedLimit;
            
            FlowConverter obConverter = CycleGrid.Resources["widthConverter"] as FlowConverter;
            obConverter.LeftBarWidth = UpstreamPhaseBar.Width;
            obConverter.RightBarWidth = DownstreamPhaseBar.Width;
        }

        private void AddGradientStops(TrafficSignal intersection, Rectangle bar, Rectangle flow, TrafficDirection trafficDirection)
        {
            var phaseGradientBrush = bar.Fill as LinearGradientBrush;
            var flowGradientBrush = flow.Fill as LinearGradientBrush;

            if (intersection == null)
            {
                bar.Visibility = Visibility.Hidden;
                flow.Visibility = Visibility.Hidden;
                return;
            }

            // Get the Gradient Offsets for this intersection
            var colorOffsets = _offsetService.GetColorOffsets(intersection, trafficDirection);
            
            phaseGradientBrush.GradientStops.Clear();
            flowGradientBrush.GradientStops.Clear();

            var fillColor = trafficDirection == TrafficDirection.Downstream ? _downstreamFlowColor : _upstreamFlowColor;
            
            // Add the colors for the phase and a matching background for flows to match the coordinated phase.
            foreach (var colorOffset in colorOffsets)
            {
                phaseGradientBrush.GradientStops.Add(new GradientStop(colorOffset.Color, 1 - (double)colorOffset.Offset));
                flowGradientBrush.GradientStops.Add(new GradientStop(colorOffset.Color == _green ? fillColor : Colors.Transparent, 1 - (double)colorOffset.Offset));
            }
            
            flowGradientBrush.Opacity = 0.6D;
        }

    }
}