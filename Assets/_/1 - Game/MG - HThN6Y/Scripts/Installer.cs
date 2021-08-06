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

            public Transform hudParent;
        }

        public Settings settings;

// #if !MGPC_COMPLETE_PROJECT
//         private Logger _logger = new LoggerConfiguration()
//             .Enrich.FromLogContext()
//             .MinimumLevel.Debug()
//             .WriteTo.Unity3D()
//             .CreateLogger();
// #endif

        public override void InstallBindings()
        {
            //
// #if !MGPC_COMPLETE_PROJECT
//             Zenject.SignalBusInstaller.Install(Container);
// #endif

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

// #if !MGPC_COMPLETE_PROJECT
//             Container
//                 .Bind<Serilog.ILogger>()
//                 .WithId("App")
//                 .FromInstance(_logger)
//                 .AsSingle();
// #endif
        }
    }
}
