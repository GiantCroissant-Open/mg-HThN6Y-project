namespace MGPC.Game.Extension.Foundation
{
    using System.Threading.Tasks;
    using UniRx;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public partial class Bootstrap
    {
        private GameObject _managerGO;

        private void Setup()
        {
            _logger
                .ForContext(typeof(Bootstrap))
                .ForContext("Method", nameof(Setup))
                .Debug($"");

            _Setup().ToObservable()
                .ObserveOnMainThread()
                .SubscribeOnMainThread()
                .Subscribe(result => {
                    _logger
                        .ForContext(typeof(Bootstrap))
                        .ForContext("Method", nameof(Setup))
                        .Debug($"_Setup done");
                })
                .AddTo(_compositeDisposable);
        }

        private async Task _Setup()
        {
            var loadAsync = Addressables.LoadAssetAsync<GameObject>("Main - HThN6Y - Game Controller");
            var managerPrefab = await loadAsync.Task;

            _managerGO = _diContainer.InstantiatePrefab(managerPrefab);

            var variablesComp = _managerGO.GetComponent<Variables>();
            if (variablesComp != null)
            {
                variablesComp.declarations.Set("resourceService", _resourceService);
                variablesComp.declarations.Set("creationService", _creationService);
                variablesComp.declarations.Set("logService", _logService);

                variablesComp.declarations.Set("hudParent", _settings.hudParent);

                //
                variablesComp.declarations.Set("camera", _settings.cameraGO);
            }

            _managerGO.tag = "ManagerRank1";

            _managerGO.transform.SetParent(_settings.hudParent);

            Addressables.ReleaseInstance(managerPrefab);
        }
    }
}
