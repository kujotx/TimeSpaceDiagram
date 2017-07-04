namespace ControlExample.IoC
{
    using Ninject.Modules;

    using ControlExample.Services;

    using TimeSpaceDiagramControl.Controls;
    using TimeSpaceDiagramControl.Interfaces;
    using TimeSpaceDiagramControl.Services;

    /// <summary>
    /// Sets up our contracts and services in Ninject
    /// </summary>
    public class TrafficRegistry : NinjectModule
    {
        /// <summary>
        /// Configure all bindings
        /// </summary>
        public override void Load()
        {
            // this app has its own service for delivering intersection data
            Bind<ITrafficSignalService>().To<FakeTrafficSignalData>()
                .InTransientScope();
            RegisterTimeSpaceDiagramControl();
        }

        /// <summary>
        /// Set up bindings for services in the TimeSpaceDiagramControl
        /// </summary>
        private void RegisterTimeSpaceDiagramControl()
        {
            Bind<ISignalPlanService>().To<SignalPlanService>()
                .InTransientScope();
            Bind<IGradientColorManager>().To<HardCodedColorManager>()
                .InTransientScope();
            Bind<IOffsetService>().To<CalculatedOffsetService>()
                .InTransientScope();
            Bind<ITimeSpaceGrid>().To<TimeSpaceGrid>()
                .InTransientScope()
                .WithPropertyValue("ArterialName", "FM 1093");
        }
    }
}