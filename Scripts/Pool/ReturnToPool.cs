using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// An abstract class that returns objects to the pool
/// </summary>
namespace RoSS
{
    public abstract class ReturnToPool<T> : MonoBehaviour where T : Component
    {
        public IObjectPool<T> pool;
    }
}