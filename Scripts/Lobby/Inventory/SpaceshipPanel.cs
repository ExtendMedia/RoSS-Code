using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class for the spaceship panel in the shipyard scene
/// </summary>
public class SpaceshipPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _spaceshipNameText;
    [SerializeField] TMP_Text _spaceshipLevelText;
    [SerializeField] GameObject _spaceshipSlotsGO;
    [SerializeField] GameObject _spaceshipItemsContainerGO;

    List<SpaceshipPanelSlot> _spaceshipSlots = new List<SpaceshipPanelSlot>();

    const string SPACESHIP_LEVEL_TEXT = "Level";
    const int SPACESHIP_LEVEL_SIZE = 50;

    private void SetSpaceshipName() => _spaceshipNameText.text = GameManager.Instance.Player.PlayerSO.DefaultSpaceship.Name;

    private void SetSpaceshipLevel() => _spaceshipLevelText.text = SPACESHIP_LEVEL_TEXT + $" <size={SPACESHIP_LEVEL_SIZE}> " + GameManager.Instance.Player.PlayerSO.DefaultSpaceship.Level;

    public void CreatePanel()
    {
        SetSpaceshipName();
        SetSpaceshipLevel();
        SetSpaceshipSlots();
        SetSpaceshipSlotsState();
    }

    private void SetSpaceshipSlots()
    {
        var slots = GameManager.Instance.Player.PlayerSO.DefaultSpaceship.ItemSlots;

        foreach (var key in slots.Keys)
        {
            if (slots.TryGetValue(key, out var slotsList))
            {
                AddSpaceshipSlotsGroup(slotsList);
            }
        }
    }

    private void AddSpaceshipSlotsGroup(List<ItemSlot> slotsList)
    {
        var slotsGroup = Instantiate<GameObject>(InventorySystem.Instance.InventorySettings.SpaceshipSlotGroupPrefab, _spaceshipSlotsGO.transform);

        foreach (var item in slotsList)
        {
            AddSpaceshipSlot(item, slotsGroup);
        }
    }

    private void AddSpaceshipSlot(ItemSlot itemSlot, GameObject slotsGroup)
    {
        var slot = Instantiate<GameObject>(InventorySystem.Instance.InventorySettings.SpaceshipSlotPrefab, slotsGroup.gameObject.transform);

        if (slot.TryGetComponent<SpaceshipPanelSlot>(out var spaceshipPanelItem) && InventorySystem.Instance.InventorySettings.ItemTypesSettings.TryGetValue(itemSlot.Type, out var itemTypeSettings))
        {
            _spaceshipSlots.Add(spaceshipPanelItem);
            spaceshipPanelItem.Icon.sprite = itemTypeSettings.SpaceshipPanelIcon;
            spaceshipPanelItem.Type = itemSlot.Type;
            if (itemSlot.Item != null)
            {
                spaceshipPanelItem.ItemSO = itemSlot.Item;
                AddSpaceshipItem(itemSlot.Item, spaceshipPanelItem, slot.transform);
            }

        }
    }

    private void AddSpaceshipItem(ItemSO itemSO, IInventoryItemOwner spaceshipPanelItem, Transform slot)
    {
        GameObject itemGO = Instantiate<GameObject>(InventorySystem.Instance.InventorySettings.ItemPrefab);
        InventoryItem inventoryItem = itemGO.GetComponent<InventoryItem>();
        inventoryItem.Create(itemSO, spaceshipPanelItem, slot);
    }

    public void SetSpaceshipSlotsState()
    {
        foreach (var slot in _spaceshipSlots)
        {
            slot.UpdateState();

        }
    }


}
