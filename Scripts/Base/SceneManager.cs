using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene manager
/// </summary>

namespace RoSS
{
    public class SceneManager : SerializedMonoBehaviour
    {

        public Dictionary<GameState, List<AssetLabelReference>> sceneAddressables = new Dictionary<GameState, List<AssetLabelReference>>();

        Dictionary<AssetLabelReference, AsyncOperationHandle<SceneInstance>> _sceneHandles = new Dictionary<AssetLabelReference, AsyncOperationHandle<SceneInstance>>();


        public void LoadScenes(GameState gameState)
        {
            if (sceneAddressables.TryGetValue(gameState, out List<AssetLabelReference> scenesToLoad))
            {
                foreach (var scene in scenesToLoad)
                {
                    if (!_sceneHandles.ContainsKey(scene))
                    {
                        Addressables.LoadSceneAsync(scene.RuntimeKey, LoadSceneMode.Additive).Completed += (asyncOperationHandle) =>
                        {
                            if (UnityEngine.SceneManagement.SceneManager.GetSceneByPath(asyncOperationHandle.Result.Scene.path).isLoaded)
                            {
                                _sceneHandles[scene] = asyncOperationHandle;
                            }
                            else
                            {
                                Debug.LogError("Scene " + asyncOperationHandle.Result.Scene.path + " loading error");
                            }
                        };

                    }
                }
            }
        }

        public void UnloadScenes(GameState gameState)
        {
            if (sceneAddressables.TryGetValue(gameState, out List<AssetLabelReference> scenesToUnload))
            {
                foreach (var scene in scenesToUnload)
                {
                    if (_sceneHandles.TryGetValue(scene, out AsyncOperationHandle<SceneInstance> sceneHandle))
                    {
                        Addressables.UnloadSceneAsync(sceneHandle).Completed += (asyncOperationHandle) =>
                        {
                            if (UnityEngine.SceneManagement.SceneManager.GetSceneByPath(asyncOperationHandle.Result.Scene.path).isLoaded)
                            {
                                Debug.LogError("Scene " + asyncOperationHandle.Result.Scene.path + " unloading error");
                            }
                            else
                            {
                                _sceneHandles.Remove(scene);
                                if (_sceneHandles.Count == 0) GameManager.Instance.LoadNewState();

                            }
                        };
                    }
                }
            }
            else
            {
                GameManager.Instance.LoadNewState();
            }
        }

        public void UnloadAllScenes()
        {
            if (UnityEngine.SceneManagement.SceneManager.sceneCount > 1)
            {
                for (int i = 1; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
                {
                    if (UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).isLoaded) UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetSceneAt(i));
                }
                _sceneHandles.Clear();
            }
        }

    }


}