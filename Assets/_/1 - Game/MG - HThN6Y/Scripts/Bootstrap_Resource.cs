namespace MGPC.Game.Extension.Foundation
{
    using System.Threading.Tasks;
    using UniRx;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public partial class Bootstrap
    {
        // private MGPC.Game.Extension.Common.ResourceService _resourceService;

        // public void RequestToLoadPrefab(GameObject requester, string key)
        // {
        //     _RequestToLoadPrefab(key).ToObservable()
        //         .ObserveOnMainThread()
        //         .SubscribeOnMainThread()
        //         .Subscribe(result =>
        //         {
        //             CustomEvent.Trigger(requester, $"Load Prefab - {key}", new object[] { result });
        //         })
        //         .AddTo(_compositeDisposable);
        // }
        //
        // private async Task<GameObject> _RequestToLoadPrefab(string key)
        // {
        //     var asyncLoad = Addressables.LoadAssetAsync<GameObject>(key);
        //     var result = await asyncLoad.Task;
        //
        //     return result;
        // }
        //
        // public void RequestToLoadScriptableObject(GameObject requester, string key)
        // {
        //     _RequestToLoadScriptableObject(key).ToObservable()
        //         .ObserveOnMainThread()
        //         .SubscribeOnMainThread()
        //         .Subscribe(result =>
        //         {
        //             CustomEvent.Trigger(requester, $"Load SO - {key}", new object[] { result });
        //         })
        //         .AddTo(_compositeDisposable);
        // }
        //
        // private async Task<ScriptableObject> _RequestToLoadScriptableObject(string key)
        // {
        //     var asyncLoad = Addressables.LoadAssetAsync<ScriptableObject>(key);
        //     var result = await asyncLoad.Task;
        //
        //     return result;
        // }

    }
}
