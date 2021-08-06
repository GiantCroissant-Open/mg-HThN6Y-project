namespace MGPC.Game.HThN6Y.App.Assist
{
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;
    using System.Threading.Tasks;
    using UniRx;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.SceneManagement;
    using GameHud = MGPC.Game.Hud;
    using GameResource = MGPC.Game.Resource;

    public partial class Bootstrap :
        Zenject.IInitializable,
        System.IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

        //
        private Zenject.DiContainer _diContainer;

        private Zenject.SignalBus _signalBus;

        private Zenject.ZenjectSceneLoader _sceneLoader;

        //
        private Serilog.ILogger _logger;

        public Bootstrap(
            Zenject.DiContainer diContainer,
            Zenject.SignalBus signalBus,
            Zenject.ZenjectSceneLoader sceneLoader,
            [Zenject.Inject(Id = "App")] Serilog.ILogger logger)
        {
            _diContainer = diContainer;
            _signalBus = signalBus;
            _sceneLoader = sceneLoader;
            _logger = logger;
        }

        // private void Setup()
        public void Initialize()
        {
            _logger
                .ForContext(typeof(Bootstrap))
                .ForContext("Method", nameof(Initialize))
                .Debug($"");


            // Observable.Timer(System.TimeSpan.FromSeconds(2))
            //     .Subscribe(_ =>
            //     {
            //         LoadScene().ToObservable()
            //             .ObserveOnMainThread()
            //             .SubscribeOnMainThread()
            //             .Subscribe(result =>
            //             {
            //                 _logger
            //                     .ForContext(typeof(Bootstrap))
            //                     .ForContext("Method", nameof(Initialize))
            //                     .Debug($"Scene Loaded");
            //             })
            //             .AddTo(_compositeDisposable);
            //     })
            //     .AddTo(_compositeDisposable);
        }

        private async Task LoadScene()
        {
            var scene = "Scene - HThN6Y - Main";
            var loadAsync = Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive);
            var sceneInstance = await loadAsync.Task;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}
