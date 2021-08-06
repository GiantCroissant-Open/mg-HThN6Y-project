namespace MGPC.Game.HThN6Y.App.Assist
{
    using UnityEngine.ResourceManagement.AsyncOperations;
    using UnityEngine.ResourceManagement.ResourceProviders;

    public partial class Bootstrap :
        MGPC.Game.Resource.IResourceRepo
    {
        public void AddAsset<T>(string contextName, string key, AsyncOperationHandle<T> asyncOperationHandle)
        {
            // throw new System.NotImplementedException();
        }

        public void AddSceneAsset(string contextName, string key, AsyncOperationHandle<SceneInstance> asyncOperationHandle)
        {
            // throw new System.NotImplementedException();
        }

        public void RemoveAll(string contextName)
        {
            // throw new System.NotImplementedException();
        }
    }
}
