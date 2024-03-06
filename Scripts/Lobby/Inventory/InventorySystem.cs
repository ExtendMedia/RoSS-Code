using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the inventory
/// </summary>
public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }


    [SerializeField] InventorySO _inventory;
    [SerializeField] InventorySettingsSO _inventorySettings;

    [SerializeField] Transform _inventoryMenu;
    [SerializeField] Transform _inventoryContent;
    [SerializeField] SpaceshipPanel _spaceshipPanel;

    public Transform draggingLayer;

    public InventorySettingsSO InventorySettings { get => _inventorySettings; private set => _inventorySettings = value; }

    Dictionary<ItemType, List<ItemSO>> _sortedItems = new Dictionary<ItemType, List<ItemSO>>();

    public InventoryTab ActiveTab;
    List<InventoryTab> _inventoryTabs = new List<InventoryTab>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            SortInventoryItemsByType();
            CreateInventory();
            _spaceshipPanel.CreatePanel();
        }
    }

    void SortInventoryItemsByType()
    {
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            _sortedItems.Add(itemType, new List<ItemSO>());
        }

        foreach (var item in _inventory.Items)
        {
            if (_sortedItems.TryGetValue(item.Type, out var itemList))
            {
                itemList.Add(item);
            }
        }
    }

    void SetActiveTab(ItemType itemType)
    {
        if (ActiveTab.Type == itemType) return;
        ActiveTab.SetActive(false);
        foreach (var inventoryTab in _inventoryTabs)
        {
            if (inventoryTab.Type == itemType)
            {
                ActiveTab = inventoryTab;
                ActiveTab.SetActive(true);
                return;
            }
        }
    }




    void CreateInventory()
    {
        bool first = true;
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            if (!first) AddInventoryTabButtonSeparator();
            var inventoryTab = AddInventoryTab(itemType, first);

            if (first)
            {
                ActiveTab = inventoryTab;
                first = false;
            }
        }
    }

    InventoryTab AddInventoryTab(ItemType type, bool active)
    {
        var inventoryTabGO = Instantiate<GameObject>(_inventorySettings.ItemListPrefab, _inventoryContent);
        var inventoryTab = inventoryTabGO.GetComponent<InventoryTab>();
        _inventoryTabs.Add(inventoryTab);
        _sortedItems.TryGetValue(type, out var itemList);
        inventoryTab.Init(type, itemList, _inventoryMenu, out var inventoryTabButton);
        inventoryTabButton.OnInventoryTabButtonClicked += ChangeActiveTab;
        inventoryTab.SetActive(active);
        return inventoryTab;


    }


    void AddInventoryTabButtonSeparator()
    {
        Instantiate<GameObject>(_inventorySettings.MenuIconSeparator, _inventoryMenu.transform);
    }

    public void ChangeActiveTab(ItemType type)
    {
        SetActiveTab(type);
        _spaceshipPanel.SetSpaceshipSlotsState();
    }

}
