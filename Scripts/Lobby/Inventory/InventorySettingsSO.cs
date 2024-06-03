using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object for the inventory settings
/// </summary>
namespace RoSS
{
    [CreateAssetMenu(fileName = "New Inventory Settings", menuName = "Data/Inventory/Settings")]
    public class InventorySettingsSO : SerializedScriptableObject
    {
        //[SerializeField] List<ItemRarity> _itemRarities;
        //[SerializeField] List<ItemType> _itemTypes;
        //public List<ItemType> ItemTypes { get => _itemTypes; }


        [Header("Inventory Menu Settings")]
        [SerializeField] GameObject _menuIconPrefab;
        public GameObject MenuIconPrefab { get => _menuIconPrefab; }


        [SerializeField] GameObject _menuIconSeparator;
        public GameObject MenuIconSeparator { get => _menuIconSeparator; }

        [Header("Tab Settings")]
        [SerializeField] GameObject _itemListPrefab;
        public GameObject ItemListPrefab { get => _itemListPrefab; }

        [SerializeField] GameObject _itemPrefab;
        public GameObject ItemPrefab { get => _itemPrefab; }

        [Header("Spaceship Panel Settings")]
        [SerializeField] GameObject _spaceshipSlotPrefab;
        public GameObject SpaceshipSlotPrefab { get => _spaceshipSlotPrefab; }
        [SerializeField] GameObject _spaceshipSlotGroupPrefab;
        public GameObject SpaceshipSlotGroupPrefab { get => _spaceshipSlotGroupPrefab; }

        [Header("Items Type Settings")]
        [DictionaryDrawerSettings(KeyLabel = "Items type", ValueLabel = "Settings")]
        [SerializeField] Dictionary<ItemType, ItemTypeSettings> _itemTypesSettings = new Dictionary<ItemType, ItemTypeSettings>();
        public Dictionary<ItemType, ItemTypeSettings> ItemTypesSettings { get => _itemTypesSettings; }


        [Header("Items Rarity Settings")]
        [DictionaryDrawerSettings(KeyLabel = "Items rarity", ValueLabel = "Settings")]
        [SerializeField] Dictionary<ItemRarity, ItemRaritySettings> _itemRaritySettings = new Dictionary<ItemRarity, ItemRaritySettings>();
        public Dictionary<ItemRarity, ItemRaritySettings> ItemRaritySettings { get => _itemRaritySettings; }

    }

    public struct ItemTypeSettings
    {
        public Sprite MenuIcon;
        public Sprite MenuIconFocus;
        public Sprite SpaceshipPanelIcon;

    }

    public struct ItemRaritySettings
    {
        public Sprite ItemFrame;

    }

}