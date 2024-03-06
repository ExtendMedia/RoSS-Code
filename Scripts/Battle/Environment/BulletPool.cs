using System;


/*
[Obsolete]
public class BulletPool : ComponentPool<Bullet>
{
    public ParticlePool particlePool;

    public override Bullet CreatePooledItem()
    {
        Bullet bullet = Instantiate(_prefab, _parent);
        var returnToPool = bullet.gameObject.AddComponent<ReturnBulletToPool>();
        returnToPool.pool = Pool;
        returnToPool.particlePool = particlePool.Pool;

        return bullet;
    }






}

*/