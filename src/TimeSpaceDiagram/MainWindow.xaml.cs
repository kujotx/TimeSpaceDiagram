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
    using Ninject;
    using System.Windows;
    using TimeSpaceDiagram.Interfaces;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow(IKernel kernel)
        {
            this.InitializeComponent();
            this.AddChild(kernel.Get<ITimeSpaceGrid>());
        }

        #endregion
    }
}