// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TimeSpaceDiagram
{
    using System.Windows;
    using TimeSpaceDiagramControl.Interfaces;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ITimeSpaceGrid _grid;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow(ITimeSpaceGrid grid)
        {
            this.InitializeComponent();
            _grid = grid;
            this.AddChild(_grid);
        }

        #endregion
    }
}