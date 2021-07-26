namespace MGPC.Game.HThN6Y.App
{
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;
    using System.Threading.Tasks;
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

        public bool ProvideExternalAsset { get; set; }

        private ScriptableObject _hudSettingDataAsset;
        private GameHud.Template.HudData _hudData;
        private List<GameObject> _canvasPrefabs;

        private readonly List<GameObject> _canvasCollection = new List<GameObject>();

        public Bootstrap(
            Zenject.DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        // private void Setup()
        public void Initialize()
        {
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
            LoadingAll();
            //     })
            //     .AddTo(_compositeDisposable);

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
                                    Debug.Log($"result: {result}");
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
