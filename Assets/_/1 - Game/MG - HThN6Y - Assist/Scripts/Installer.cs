namespace MGPC.Game.HThN6Y.App.Assist
{
    using Serilog;
    using Serilog.Sinks.Unity3D;
    using Zenject;

    using Logger = Serilog.Core.Logger;

    public class Installer : MonoInstaller<Installer>
    {
        private Logger _logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Debug()
            .WriteTo.Unity3D()
            .CreateLogger();

        public override void InstallBindings()
        {
            //
            Zenject.SignalBusInstaller.Install(Container);

            //
            // Container.DeclareSignal<AllModuleSetupDoneSignal>();
            // Container.DeclareSignal<ModuleSetupDoneSignal>();
            //
            // //
            // Container.DeclareSignal<UserSignedInSignal>();
            // Container.DeclareSignal<UserSignedOutSignal>();

            //
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();

            //
            Container
                .Bind<Serilog.ILogger>()
                .WithId("App")
                .FromInstance(_logger)
                .AsSingle();
        }
    }
}
