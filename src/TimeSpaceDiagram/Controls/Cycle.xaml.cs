// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cycle.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for Cycle.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using TimeSpaceDiagram.Services;

namespace TimeSpaceDiagram.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using TimeSpaceDiagram.Converters;
    using TimeSpaceDiagram.Domain;
    using TimeSpaceDiagram.Interfaces;

    /// <summary>
    /// Interaction logic for Cycle.xaml
    /// </summary>
    public partial class Cycle : UserControl
    {

        // Dependency Property
        public static readonly DependencyProperty StraightawayProperty =
             DependencyProperty.Register("Segment", typeof(Segment),
             typeof(Cycle), new FrameworkPropertyMetadata(new Segment()));

        private readonly IOffsetService offsetService;
        private readonly IGradientColorManager _gradientColorManager;
        private readonly Color _green;
        private readonly Color _outboundFlowColor;
        private readonly Color _inboundFlowColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cycle"/> class.
        /// </summary>
        public Cycle(Segment straightaway, IOffsetService offsetService, IGradientColorManager gradientColorManager)
        {
            this.InitializeComponent();
            this._gradientColorManager = gradientColorManager;
            this._green = this._gradientColorManager.GetGreenColor();
            this._outboundFlowColor = this._gradientColorManager.GetOutboundFlowColor();
            this._inboundFlowColor = this._gradientColorManager.GetInboundFlowColor();
            this.Straightaway = straightaway;
            this.offsetService = offsetService;
            CreateConverters(straightaway);
            CreateGradientStops();
        }

        private Segment Straightaway
        {
            get { return (Segment)GetValue(StraightawayProperty); }
            set { SetValue(StraightawayProperty, value); }
        }

        /// <summary>
        /// Creates Gradient Offsets in the bar and flow rectangles for both directions.
        /// </summary>
        private void CreateGradientStops()
        {
            this.AddGradientStops(this.Straightaway.OutboundIntersection, this.OutboundPhaseBar, this.OutboundFlow, TrafficDirection.Outbound);
            this.AddGradientStops(this.Straightaway.InboundIntersection, this.InboundPhaseBar, this.InboundFlow, TrafficDirection.Inbound);
        }

        /// <summary>
        /// Configure the converters that set the speed angle of the bandwidth bars
        /// TODO Find out how to inject contracts so we can switch out the standard converter for a metric one
        /// </summary>
        /// <param name="straightaway"></param>
        private void CreateConverters(Segment straightaway)
        {
            CycleConverter cycleConverter = this.CycleGrid.Resources["cycleConverter"] as CycleConverter;
            cycleConverter.Cycles = straightaway.Cycles;

            CycleBackgroundConverter cycleBackgroundConverter = this.CycleGrid.Resources["cycleBackgroundConverter"] as CycleBackgroundConverter;
            cycleBackgroundConverter.Cycles = straightaway.Cycles;
            
            SpeedLimitAngleConverter angleConverter = this.CycleGrid.Resources["angleConverter"] as SpeedLimitAngleConverter;
            angleConverter.Distance = straightaway.Distance;
            angleConverter.CycleLength = straightaway.CycleLength;
            angleConverter.SpeedLimit = straightaway.SpeedLimit;
            
            FlowConverter obConverter = this.CycleGrid.Resources["widthConverter"] as FlowConverter;
            obConverter.LeftBarWidth = this.InboundPhaseBar.Width;
            obConverter.RightBarWidth = this.OutboundPhaseBar.Width;
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
            var colorOffsets = offsetService.GetColorOffsets(intersection, trafficDirection);
            
            phaseGradientBrush.GradientStops.Clear();
            flowGradientBrush.GradientStops.Clear();

            var fillColor = trafficDirection == TrafficDirection.Outbound ? _outboundFlowColor : _inboundFlowColor;
            
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