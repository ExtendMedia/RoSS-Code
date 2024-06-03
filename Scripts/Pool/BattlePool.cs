using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// An abstract class for pools used in battle
/// </summary>
namespace RoSS
{
    public abstract class BattlePool<T> where T : Component
    {
        protected bool _collectionChecks = true;
        protected int _maxPoolSize = 100;
        protected const int _defaultPoolSize = 10;
        protected Transform _parent;

        protected IObjectPool<T> _Pool;
        protected T _prefab;
        protected AudioClip[] _audioClips;




        public IObjectPool<T> Pool
        {
            get
            {
                if (_Pool == null)
                {
                    _Pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, _collectionChecks, _defaultPoolSize, _maxPoolSize);
                }
                return _Pool;
            }
        }

        public virtual T CreatePooledItem()
        {
            var go = GameObject.Instantiate(_prefab, _parent);
            var ps = go.GetComponent<T>();

            return ps;
        }

        public void PreAllocate(int poolSize = _defaultPoolSize)
        {
            T[] items = new T[poolSize];

            for (int i = 0; i < poolSize; i++)
            {
                items[i] = Pool.Get();
            }
            for (int i = 0; i < poolSize; i++)
            {
                Pool.Release(items[i]);
            }

        }

        protected void OnReturnedToPool(T component)
        {
            component.gameObject.SetActive(false);
        }

        protected void OnTakeFromPool(T component)
        {
            component.gameObject.SetActive(true);
        }

        protected void OnDestroyPoolObject(T component)
        {
            GameObject.Destroy(component.gameObject);
        }
    }
}