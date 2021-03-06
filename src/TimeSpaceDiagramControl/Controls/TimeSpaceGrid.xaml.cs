﻿namespace TimeSpaceDiagramControl.Controls
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using TimeSpaceDiagramControl.Domain;
    using TimeSpaceDiagramControl.Interfaces;

    /// <summary>
    /// Interaction logic for TimeSpaceGrid.xaml
    /// </summary>
    public partial class TimeSpaceGrid : TsUserControl, ITimeSpaceGrid
    {
        private const int Cycles = 3;

        private readonly SignalPlan _signalPlan;

        private readonly ISignalPlanService _signalPlanService;

        private readonly IOffsetService _offsetService;

        public TimeSpaceGrid(
            ISignalPlanService signalPlanService, 
            IOffsetService offsetService, 
            IGradientColorManager gradientColorManager)
        {
            InitializeComponent();
            _signalPlanService = signalPlanService;
            _signalPlan = _signalPlanService.CreateSignalPlan(Cycles, ArterialName);
            _offsetService = offsetService;
            _gradientColorManager = gradientColorManager;
            CreateSignalPlan();
        }


        // Dependency Property
        public static readonly DependencyProperty ArterialNameProperty =
             DependencyProperty.Register("ArterialName", typeof(string),
             typeof(TimeSpaceGrid), new FrameworkPropertyMetadata(string.Empty));

        private IGradientColorManager _gradientColorManager;

        public string ArterialName 
        {
            get { return (string)GetValue(ArterialNameProperty); }
            set { SetValue(ArterialNameProperty, value); }
        }

        private void CreateSignalPlan()
        {
            IEnumerable<Segment> segments = _signalPlan.Arterials;
            SetGridColumnDefinitions(segments, CycleGrid);
            AddSegmentCellsToGrid(segments, CycleGrid);
        }

        private void AddSegmentCellsToGrid(IEnumerable<Segment> segments, Grid grid)
        {
            int i = 0;
            foreach (var straightaway in segments)
            {
                var cycle = new Cycle(straightaway, _offsetService, _gradientColorManager);
                Grid.SetColumn(cycle, i++);
                grid.Children.Add(cycle);
            }
        }

        private void SetGridColumnDefinitions(IEnumerable<Segment> segments, Grid grid)
        {
            foreach (var straightaway in segments)
            {
                var column = new ColumnDefinition { Width = new GridLength(GetColumnWidth(straightaway, segments), GridUnitType.Star) };
                grid.ColumnDefinitions.Add(column);
            }
        }

        private static double GetColumnWidth(Segment segment, IEnumerable<Segment> segments)
        {
            return segment.Distance / segments.Min(c => c.Distance);
        }

    }
}
