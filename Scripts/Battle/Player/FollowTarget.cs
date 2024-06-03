using UnityEngine;

/// <summary>
/// Projectile spawners follow the player's crosshair
/// </summary>
namespace RoSS
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] Transform _target;
        Vector3 _offset = new Vector3(0, -5f, -10f);

        void Update()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y + _offset.y, _offset.z);

        }
    }

}