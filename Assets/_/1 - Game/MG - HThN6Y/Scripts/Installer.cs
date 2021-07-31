namespace MGPC.Game.Extension.Foundation
{
    using Serilog;
    using Serilog.Sinks.Unity3D;
    using UnityEngine;
    using Zenject;

    using Logger = Serilog.Core.Logger;

    public class Installer : MonoInstaller<Installer>
    {
        [System.Serializable]
        public class Settings
        {
            public GameObject cameraGO;
        }

        public Settings settings;

        private Logger _logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Debug()
            .WriteTo.Unity3D()
            .CreateLogger();

        public override void InstallBindings()
        {
            // //
            // Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();

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
            Container.Bind<Settings>().FromInstance(settings).AsSingle();

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
