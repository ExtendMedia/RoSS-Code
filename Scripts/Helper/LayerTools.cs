using UnityEngine;

/// <summary>
/// Helper methods for layers
/// </summary>
namespace RoSS
{
    public class LayerTools : MonoBehaviour
    {

        public static void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;

            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }


}