using UnityEngine;

/// <summary>
/// Scriptable Object class for weapon items
/// </summary>
namespace RoSS
{
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "Data/Weapon Item")]
    public class WeaponItemSO : ItemSO
    {

        [SerializeField] float _damage;
        [SerializeField] float _health;
        [SerializeField] float _reloadTime;
        [SerializeField] float _speed;
        [SerializeField] float _lifeTime;
        [Range(0, 1)]
        [SerializeField] float _accuracy;
        [SerializeField] int _guns = 1;
        [SerializeField] bool _homing = false;
        [SerializeField] Transform _prefab;
        [SerializeField] ProjectileSO _projectileSO;
        [SerializeField] EffectSO _effectSO;
        [SerializeField] EffectSO _destructionEffectSO;


        public float Damage { get => _damage; private set => _damage = value; }
        public float Health { get => _health; private set => _health = value; }
        public float ReloadTime { get => _reloadTime; private set => _reloadTime = value; }
        public float Speed { get => _speed; private set => _speed = value; }
        public float LifeTime { get => _lifeTime; private set => _lifeTime = value; }
        public float Accuracy { get => _accuracy; private set => _accuracy = value; }
        public int Guns { get => _guns; private set => _guns = value; }
        public bool Homing { get => _homing; private set => _homing = value; }
        public Transform Prefab { get => _prefab; private set => _prefab = value; }
        public ProjectileSO ProjectileSO { get => _projectileSO; private set => _projectileSO = value; }
        public EffectSO EffectSO { get => _effectSO; private set => _effectSO = value; }
        public EffectSO DestructionEffectSO { get => _destructionEffectSO; private set => _destructionEffectSO = value; }

        public int GetPoolQuantity() => Mathf.CeilToInt(_lifeTime * _guns / _reloadTime);

    }
}