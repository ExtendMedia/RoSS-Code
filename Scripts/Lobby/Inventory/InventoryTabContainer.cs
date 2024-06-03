using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls an items container for the inventory tabs
/// </summary>
namespace RoSS
{
    public class InventoryTabContainer : MonoBehaviour, IDropHandler
    {

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryItem>(out var inventoryItem))
            {
                if (inventoryItem.TryGetComponent<DragAndDrop>(out var dragableItem))
                {
                    if (TryFindTabToDrop(inventoryItem, out var tabToDrop))
                    {
                        if (InventorySystem.Instance.ActiveTab != tabToDrop) InventorySystem.Instance.ChangeActiveTab(tabToDrop.Type);
                        tabToDrop.AddItem(inventoryItem);
                    }
                }
            }

        }

        bool TryFindTabToDrop(InventoryItem inventoryItem, out InventoryTab inventoryTab)
        {
            inventoryTab = null;
            var inventoryTabContainer = transform.parent;
            var inventoryTabs = inventoryTabContainer.GetComponentsInChildren<InventoryTab>(true);
            foreach (var tab in inventoryTabs)
            {
                if (tab.Type == inventoryItem.GetItemSOType())
                {
                    inventoryTab = tab;
                    return true;
                }
            }
            return false;
        }
    }
}