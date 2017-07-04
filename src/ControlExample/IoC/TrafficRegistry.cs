namespace ControlExample.IoC
{
    using Ninject.Modules;

    using ControlExample.Services;

    using TimeSpaceDiagramControl.Controls;
    using TimeSpaceDiagramControl.Interfaces;
    using TimeSpaceDiagramControl.Services;

    /// <summary>
    /// Sets up our contracts and services
    /// </summary>
    public class TrafficRegistry : NinjectModule
    {
        /// <summary>
        /// Configure all bindings
        /// </summary>
        public override void Load()
        {
            Bind<ITrafficSignalService>().To<FakeIntersectionService>()
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