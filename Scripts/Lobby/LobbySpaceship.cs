using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Controls the spaceship gameobject in the lobby
/// </summary>
namespace RoSS
{
    public class LobbySpaceship : MonoBehaviour
    {
        void Awake()
        {

            AssetReference lobbySpaceship = GameManager.Instance.Player.ActiveSpaceship.PrefabARef;
            if (lobbySpaceship.IsDone)
            {
                lobbySpaceship.InstantiateAsync(transform);
            }
            else
            {
                lobbySpaceship.LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) =>
                {
                    if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        Instantiate(asyncOperationHandle.Result, transform);
                    }
                    else
                    {
                        Debug.LogError("Failed to load lobby spaceship prefab asset reference: " + GameManager.Instance.Player.ActiveSpaceship.PrefabARef);
                    }
                };

            }
        }


    }
}