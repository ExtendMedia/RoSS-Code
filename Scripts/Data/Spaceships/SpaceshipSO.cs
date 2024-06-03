using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// Scriptable Object class for spaceships
/// </summary>
namespace RoSS
{
    [CreateAssetMenu(fileName = "New Spaceship", menuName = "Data/Spaceship")]
    public class SpaceshipSO : SerializedScriptableObject
    {

        [SerializeField] string _name;
        public string Name => _name;

        [SerializeField] int _level;
        public int Level => _level;

        [SerializeField] float _health;
        public float Health { get => _health; private set => _health = value; }

        //[SerializeField] Transform _prefab;
        //public Transform Prefab { get => _prefab; private set => _prefab = value; }

        [SerializeField] AssetReference _prefabARef;
        public AssetReference PrefabARef { get => _prefabARef; private set => _prefabARef = value; }

        [SerializeField] EffectSO _fullDestructionEffect;
        [SerializeField] int _fullDestructionPoints;

        public EffectSO FullDestructionEffect { get => _fullDestructionEffect; private set => _fullDestructionEffect = value; }
        public int FullDestructionPoints { get => _fullDestructionPoints; private set => _fullDestructionPoints = value; }

        [DictionaryDrawerSettings(KeyLabel = "Group", ValueLabel = "Slots")]

        public Dictionary<int, List<ItemSlot>> ItemSlots = new Dictionary<int, List<ItemSlot>>();

        public List<WeaponItemSO> GetWeapons(ItemType weaponType)
        {
            List<WeaponItemSO> weaponList = new List<WeaponItemSO>();
            foreach (var slotsGroup in ItemSlots)
            {
                foreach (var slot in slotsGroup.Value.ToArray())
                {
                    if (slot.Item && (slot.Item?.Type == weaponType)) weaponList.Add(slot.Item as WeaponItemSO);
                }
            }
            return weaponList;
        }

        public List<WeaponItemSO> GetWeapons()
        {
            List<WeaponItemSO> weaponList = new List<WeaponItemSO>();
            foreach (var slotsGroup in ItemSlots)
            {
                foreach (var slot in slotsGroup.Value.ToArray())
                {
                    if (slot.Item) weaponList.Add(slot.Item as WeaponItemSO);
                }
            }
            return weaponList;
        }

        public Dictionary<int, List<WeaponItemSO>> GetWeaponsInSlots(ItemType weaponType)
        {

            Dictionary<int, List<WeaponItemSO>> weaponDict = new Dictionary<int, List<WeaponItemSO>>();
            foreach (var slotsGroup in ItemSlots)
            {

                List<WeaponItemSO> weaponList = new List<WeaponItemSO>();

                foreach (var slot in slotsGroup.Value.ToArray())
                {

                    if (slot.Item?.Type == weaponType)
                    {
                        weaponList.Add(slot.Item as WeaponItemSO);

                    }
                }
                weaponDict.Add(slotsGroup.Key, weaponList);
            }
            return weaponDict;
        }

        public List<ItemSlot> GetSlots()
        {
            List<ItemSlot> slotsList = new List<ItemSlot>();
            foreach (var slotsGroup in ItemSlots)
            {
                foreach (var slot in slotsGroup.Value.ToArray())
                {
                    if (slot.Item) slotsList.Add(slot);
                }
            }
            return slotsList;
        }

        public void GetWeaponProjectilesAndEffects(out Dictionary<ProjectileSO, int> projectileDict, out Dictionary<EffectSO, int> effectDict, bool addEffects = true, bool addDestructionEffects = false, bool addFullDestructionEffects = false)
        {
            projectileDict = new Dictionary<ProjectileSO, int>();
            effectDict = new Dictionary<EffectSO, int>();

            foreach (var weaponItemSO in GetWeapons())
            {
                int quantity = weaponItemSO.GetPoolQuantity();
                if (weaponItemSO.ProjectileSO) AddProjectileToDict(weaponItemSO.ProjectileSO, projectileDict, quantity);
                if (weaponItemSO.EffectSO && addEffects) AddEffectToDict(weaponItemSO.EffectSO, effectDict, quantity);
                if (weaponItemSO.DestructionEffectSO && addDestructionEffects) AddEffectToDict(weaponItemSO.DestructionEffectSO, effectDict, 1);
            }

            if (addFullDestructionEffects)
                AddEffectToDict(FullDestructionEffect, effectDict, FullDestructionPoints);
        }

        void AddProjectileToDict(ProjectileSO projectileSO, Dictionary<ProjectileSO, int> projectileDict, int quantity)
        {
            if (projectileDict.ContainsKey(projectileSO))
                projectileDict[projectileSO] += quantity;
            else
                projectileDict.Add(projectileSO, quantity);
        }

        void AddEffectToDict(EffectSO effectSO, Dictionary<EffectSO, int> effectDict, int quantity)
        {
            if (effectDict.ContainsKey(effectSO))
                effectDict[effectSO] += quantity;
            else
                effectDict.Add(effectSO, quantity);
        }


    }

    public struct ItemSlot
    {
        //public ItemTypeSO Type;
        public ItemType Type;
        public ItemSO Item;
        public Vector3 Position;

    }
}