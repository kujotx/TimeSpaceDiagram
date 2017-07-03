using TimeSpaceDiagram.Services.Intersections;

namespace TimeSpaceDiagram.IoC
{
    using Ninject.Modules;

    using Controls;
    using Interfaces;
    using Services;

    /// <summary>
    /// Sets up our contracts and services
    /// </summary>
    public class TrafficRegistry : NinjectModule
    {
        public override void Load()
        {
            Bind<ITrafficSignalService>().To<FakeIntersectionService>().InTransientScope();
            Bind<ISignalPlanService>().To<SignalPlanService>().InTransientScope();
            Bind<IGradientColorManager>().To<HardCodedColorManager>().InTransientScope();
            Bind<IOffsetService>().To<CalculatedOffsetService>().InTransientScope();
            Bind<Cycle>().ToSelf().InTransientScope();
        }
    }
}