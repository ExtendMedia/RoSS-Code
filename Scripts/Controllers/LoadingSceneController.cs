using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RoSS
{
    public class LoadingSceneController : MonoBehaviour
    {
        [SerializeField] AssetLabelReference scene;
        [SerializeField] TMP_Text loadingPercText;
        [SerializeField] Slider loadingPercSlider;

        AsyncOperationHandle<SceneInstance> _sceneHandle;
        void Start()
        {
            _sceneHandle = Addressables.LoadSceneAsync(scene.RuntimeKey);

            _sceneHandle.Completed += (asyncOperationHandle) =>
            {
                if (!UnityEngine.SceneManagement.SceneManager.GetSceneByPath(asyncOperationHandle.Result.Scene.path).isLoaded)
                    Debug.LogError("Scene " + asyncOperationHandle.Result.Scene.path + " loading error");
            };
        }

        void Update()
        {
            loadingPercText.text = _sceneHandle.PercentComplete.ToString() + "%";
            loadingPercSlider.value = _sceneHandle.PercentComplete;
        }

    }
}