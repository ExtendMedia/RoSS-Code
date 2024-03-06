using System;
using UnityEngine;
using UnityEngine.Pool;

/*
[Obsolete]
public abstract class ComponentPool<T> : MonoBehaviour where T : Component
{
    bool _collectionChecks = true;
    int _maxPoolSize = 100;
    int _defaultPoolSize = 10;
    protected Transform _parent;

    protected IObjectPool<T> _Pool;
    [SerializeField] protected T _prefab;


    public virtual void Init(T prefab, Transform parent, int maxPoolSize = 100, int defaultPoolSize = 10, bool collectionChecks = true)
    {
        _prefab = prefab;
        _parent = parent;
        _maxPoolSize = maxPoolSize;
        _defaultPoolSize = defaultPoolSize;
        _collectionChecks = collectionChecks;
    }

    public virtual IObjectPool<T> Pool
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
        var go = Instantiate(_prefab, _parent);
        var ps = go.GetComponent<T>();

        return ps;
    }

    public virtual void PreAllocate()
    {
        T[] items = new T[_defaultPoolSize];

        for (int i = 0; i < _defaultPoolSize; i++)
        {
            items[i] = Pool.Get(); 
        }
        for (int i = 0; i < _defaultPoolSize; i++)
        {
            Pool.Release(items[i]);
        }

    }

    protected virtual void OnReturnedToPool(T component)
    {
        component.gameObject.SetActive(false);
    }

    protected virtual void OnTakeFromPool(T component)
    {
        component.gameObject.SetActive(true);
    }

    protected virtual void OnDestroyPoolObject(T component)
    {
        Destroy(component.gameObject);
    }
}
*/