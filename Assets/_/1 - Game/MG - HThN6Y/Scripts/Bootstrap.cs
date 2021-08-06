namespace MGPC.Game.Extension.Foundation
{
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using UniRx;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
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

        private Installer.Settings _settings;

#if false
        private MGPC.Game.Extension.Common.IRankOneControl _rankOneControl;
#endif

        //
        private MGPC.Game.Extension.Common.IResourceService _resourceService;
        private MGPC.Game.Extension.Common.ICreationService _creationService;
        private MGPC.Game.Extension.Common.ILoginInfo _loginInfo;
        private MGPC.Game.Extension.Common.ILogService _logService;

        private MGPC.Game.Resource.IResourceRepo _resourceRepo;

        public bool ProvideExternalAsset { get; set; }

        private ScriptableObject _hudSettingDataAsset;
        private GameHud.Template.HudData _hudData;
        private List<GameObject> _canvasPrefabs;

        private readonly List<GameObject> _canvasCollection = new List<GameObject>();

        private GameObject _gameController;

        public Bootstrap(
            Zenject.DiContainer diContainer,
            Zenject.SignalBus signalBus,
            Zenject.ZenjectSceneLoader sceneLoader,
            MGPC.Game.Resource.IResourceRepo resourceRepo,

            [Zenject.Inject(Id = "App")] Serilog.ILogger logger,
            Installer.Settings settings
#if false

            , MGPC.Game.Extension.Common.IRankOneControl rankOneControl
#endif
            )
        {
            _diContainer = diContainer;
            _signalBus = signalBus;
            _sceneLoader = sceneLoader;
            _logger = logger;
            _settings = settings;

#if false
            _rankOneControl = rankOneControl;
#endif
        }

        // private void Setup()
        public void Initialize()
        {
            _logger
                .ForContext(typeof(Bootstrap))
                .ForContext("Method", nameof(Initialize))
                .Debug($"");

            //
            //
            _creationService = new MGPC.Game.Extension.Common.CreationService(_diContainer);
            // _hudService = new MGPC.Game.Extension.Common.HudService(_appHudProvider);
            // _loginInfo = new MGPC.Game.Extension.Common.LoginInfo(_loginStatus);
            _logService = new MGPC.Game.Extension.Common.LogService(_logger);
            _resourceService = new MGPC.Game.Extension.Common.ResourceService(_logger, _resourceRepo, "MG");

            Setup();

            //LoadingGameController("Game Controller");
            // LoadingAsset("Hud - HThN6Y");

            // LoadScriptableObject(assetName).ToObservable()

            // Addressables.GetDownloadSizeAsync("Game Controller").ToObservable()
            //     .ObserveOnMainThread()
            //     .SubscribeOnMainThread()
            //     .Subscribe(result =>
            //     {
            //
            //
            // LoadingAll();
            //     })
            //     .AddTo(_compositeDisposable);

            // Observable.Timer(System.TimeSpan.FromSeconds(2))
            //     .Subscribe(_ =>
            //     {
            //         LoadGameController().ToObservable()
            //             .ObserveOnMainThread()
            //             .SubscribeOnMainThread()
            //             .Subscribe(result =>
            //             {
            //                 _logger
            //                     .ForContext(typeof(Bootstrap))
            //                     .ForContext("Method", nameof(Initialize))
            //                     .Debug($"GameController Loaded");
            //             })
            //             .AddTo(_compositeDisposable);
            //     })
            //     .AddTo(_compositeDisposable);
        }

        private async Task LoadGameController()
        {
            var asyncLoad = Addressables.LoadAssetAsync<GameObject>("Main - HThN6Y - Game Controller");
            var prefab = await asyncLoad.Task;

            if (prefab != null)
            {
                _gameController = _diContainer.InstantiatePrefab(prefab);

                var variablesComp = _gameController.GetComponent<Variables>();
                if (variablesComp != null)
                {
                    variablesComp.declarations.Set("logService", _logService);
                    variablesComp.declarations.Set("resourceService", _resourceService);
                    // variablesComp.declarations.Set("logService", (MGPC.Game.Extension.Common.ILogService)this);
                    // variablesComp.declarations.Set("resourceService", (MGPC.Game.Extension.Common.IResourceService)this);

                    //
                    variablesComp.declarations.Set("camera", _settings.cameraGO);
                }
            }

            Addressables.Release(prefab);
        }

        private async Task<long> Check()
        {
            var aaa = await Addressables.GetDownloadSizeAsync("Hud - HThN6Y").Task;

            return aaa;
        }

        private void LoadingAll()
        {
            LoadScriptableObject("Hud - HThN6Y").ToObservable()
                .ObserveOnMainThread()
                .SubscribeOnMainThread()
                .Subscribe(result =>
                {
                    var hudData = (result as GameHud.Template.HudData);

                    LoadPrefab("Game Controller").ToObservable()
                        .ObserveOnMainThread()
                        .SubscribeOnMainThread()
                        .Subscribe(result =>
                        {
                            var prefab = (result as GameObject);
                            if (prefab != null)
                            {
                                // var instanceGO = _diContainer.InstantiatePrefab(prefab);
                                var variablesComp = prefab.GetComponent<Variables>();
                                variablesComp.declarations.Set("canvasPrefab", hudData.canvasPrefabs.First());

                                var instanceGO = _diContainer.InstantiatePrefab(prefab);
                            }

                            Check().ToObservable()
                                .ObserveOnMainThread()
                                .SubscribeOnMainThread()
                                .Subscribe(result =>
                                {
                                    // Debug.Log($"result: {result}");
                                })
                                .AddTo(_compositeDisposable);
                        })
                        .AddTo(_compositeDisposable);
                })
                .AddTo(_compositeDisposable);
        }

        private void LoadingGameController(string assetName)
        {
            //
            LoadPrefab(assetName).ToObservable()
                .ObserveOnMainThread()
                .SubscribeOnMainThread()
                .Subscribe(result =>
                {
                    var prefab = (result as GameObject);
                    if (prefab != null)
                    {
                        var instanceGO = _diContainer.InstantiatePrefab(prefab);
                    }
                })
                .AddTo(_compositeDisposable);
        }

        private void LoadingCanvas(string assetName)
        {
            //
            LoadScriptableObject(assetName).ToObservable()
                .ObserveOnMainThread()
                .SubscribeOnMainThread()
                .Subscribe(result =>
                {
                })
                .AddTo(_compositeDisposable);
        }


        private void LoadingAsset(string hudAssetName)
        {
            if (ProvideExternalAsset)
            {
                // Since the asset is provided, just notify instantly
// #if WAIO_FLOWCONTROL
                // FlowControl.FinishIndividualLoadingAsset(new GameFlowControl.FlowControlContext
                // {
                //     Name = AtPart,
                //     Description = $"Hud - {AtPart}"
                // });

// #endif
            }
            else
            {
                // Load internally then notify
                InternalLoadAsset(
                    hudAssetName,
                    () =>
                    {
// #if WAIO_FLOWCONTROL
                        // _logger
                        //     .ForContext(typeof(Bootstrap))
                        //     .ForContext("Method", nameof(LoadingAsset))
                        //     .Debug($"{hudAssetName}");

                        // FlowControl.FinishIndividualLoadingAsset(new GameFlowControl.FlowControlContext
                        // {
                        //     Name = AtPart,
                        //     Description = $"Hud - {AtPart}"
                        // });
// #endif
                    });
            }
        }

        private void InternalLoadAsset(
            string hudAssetName,
            System.Action loadingDoneAction)
        {
            //
            Load(hudAssetName).ToObservable()
                .ObserveOnMainThread()
                .SubscribeOnMainThread()
                .Subscribe(result =>
                {
                    _hudSettingDataAsset = result;

                    _hudData = (_hudSettingDataAsset as GameHud.Template.HudData);

                    if (_hudData != null)
                    {
                        // _logger
                        //     .ForContext(typeof(Bootstrap))
                        //     .ForContext("Method", nameof(InternalLoadAsset))
                        //     .Debug($"hud data is not null");

                        _canvasPrefabs = _hudData.canvasPrefabs;
                        _canvasPrefabs.ForEach(canvasPrefab =>
                        {
                            // var canvas = GameObject.Instantiate(canvasPrefab);
                            var canvas = _diContainer.InstantiatePrefab(canvasPrefab);
                            _canvasCollection.Add(canvas);
#if UNITY_EDITOR
                            canvas.name = canvas.name.Replace("(Clone)", "");
                            // canvas.name = canvas.name + $" - {AtPart}";
#endif
                            // CommandService.AddCommandStreamProducer(canvas);
                            // CommandService.AddInfoStreamPresenter(canvas);
                            // ExtensionService.SetReferenceToExtension(canvas);
                            //
                            // SceneService.MoveToCurrentScene(canvas);
                        });
                    }
                    else
                    {
                        // _logger
                        //     .ForContext(typeof(Bootstrap))
                        //     .ForContext("Method", nameof(InternalLoadAsset))
                        //     .Debug($"hud data is null");
                        // _logger.Debug($"Module - Hud - {AtPart} - PrepareAssetSystem - InternalLoadAsset - hud data is null");
                    }

                    loadingDoneAction();
                })
                .AddTo(_compositeDisposable);
        }

        private async Task<GameObject> LoadPrefab(string assetName)
        {
            var prefabSettingAssetTask = GameResource.Utility.AssetLoadingHelper.GetAsset<GameObject>(assetName);

            var prefabSettingAsset = await prefabSettingAssetTask;

            return prefabSettingAsset;
        }

        private async Task<ScriptableObject> LoadScriptableObject(string assetName)
        {
            var soAssetTask = GameResource.Utility.AssetLoadingHelper.GetAsset<ScriptableObject>(assetName);

            var soAsset = await soAssetTask;

            return soAsset;
        }

        private async Task<ScriptableObject> Load(string hudAssetName)
        {
            //
            var hudSettingAssetName = hudAssetName;
            var hudSettingAssetTask = GameResource.Utility.AssetLoadingHelper.GetAsset<ScriptableObject>(hudSettingAssetName);

            var hudSettingAsset = await hudSettingAssetTask;

            return hudSettingAsset;
        }

        public void Dispose()
        {
            // CleanupRemoteConfiguration();
            _compositeDisposable?.Dispose();
        }
    }
}
