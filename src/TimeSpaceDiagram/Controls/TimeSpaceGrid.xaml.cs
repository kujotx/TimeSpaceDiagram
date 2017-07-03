namespace TimeSpaceDiagram.Controls
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using Ninject;

    using TimeSpaceDiagram.Domain;
    using TimeSpaceDiagram.Interfaces;

    /// <summary>
    /// Interaction logic for TimeSpaceGrid.xaml
    /// </summary>
    public partial class TimeSpaceGrid : TsUserControl
    {
        private const int Cycles = 3;

        private SignalPlan _signalPlan;

        private ISignalPlanService _signalPlanService;

        private IOffsetService _offsetService;

        public TimeSpaceGrid()
        {
            InitializeComponent();
            CreateSignalPlan();
        }


        // Dependency Property
        public static readonly DependencyProperty ArterialNameProperty =
             DependencyProperty.Register("ArterialName", typeof(string),
             typeof(TimeSpaceGrid), new FrameworkPropertyMetadata(string.Empty));
        
        public string ArterialName 
        {
            get { return (string)GetValue(ArterialNameProperty); }
            set { SetValue(ArterialNameProperty, value); }
        }

        private void CreateSignalPlan()
        {
            _signalPlanService = Kernel.Get<ISignalPlanService>();
            _signalPlan = _signalPlanService.CreateSignalPlan(Cycles, ArterialName);
            IEnumerable<Segment> segments = _signalPlan.Arterials;
            SetGridColumnDefinitions(segments, CycleGrid);
            AddSegmentCellsToGrid(segments, CycleGrid);
        }

        private void AddSegmentCellsToGrid(IEnumerable<Segment> segments, Grid grid)
        {
            IGradientColorManager gradientColorManager = Kernel.Get<IGradientColorManager>();
            int i = 0;
            foreach (var straightaway in segments)
            {
                var cycle = new Cycle(straightaway, _offsetService, gradientColorManager);
                Grid.SetColumn(cycle, i++);
                grid.Children.Add(cycle);
            }
        }

        private void SetGridColumnDefinitions(IEnumerable<Segment> segments, Grid grid)
        {
            foreach (var straightaway in segments)
            {
                var column = new ColumnDefinition {
                    Width = new GridLength(GetColumnWidth(straightaway, segments), GridUnitType.Star) };
                grid.ColumnDefinitions.Add(column);
            }
        }

        private static double GetColumnWidth(Segment segment, IEnumerable<Segment> segments)
        {
            return segment.Distance / segments.Min(c => c.Distance);
        }

    }
}
