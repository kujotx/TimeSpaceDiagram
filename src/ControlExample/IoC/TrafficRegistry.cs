namespace ControlExample.IoC
{
    using Ninject.Modules;

    using ControlExample.Services.Intersections;
    using ControlExample.Services;

    using TimeSpaceDiagramControl.Controls;
    using TimeSpaceDiagramControl.Interfaces;
    

    /// <summary>
    /// Sets up our contracts and services
    /// </summary>
    public class TrafficRegistry : NinjectModule
    {
        public override void Load()
        {
            Bind<ITrafficSignalService>().To<FakeIntersectionService>()
                .InTransientScope();
            Bind<ISignalPlanService>().To<SignalPlanService>()
                .InTransientScope();
            Bind<IGradientColorManager>().To<HardCodedColorManager>()
                .InTransientScope();
            Bind<IOffsetService>().To<CalculatedOffsetService>()
                .InTransientScope();
            Bind<ITimeSpaceGrid>().To<TimeSpaceGrid>()
                .InTransientScope()
                .WithPropertyValue("ArterialName", "FM 1093");
            Bind<MainWindow>().ToSelf()
                .InTransientScope();
            Bind<Cycle>().ToSelf()
                .InTransientScope();
        }
    }
}