using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the inventory tab in the lobby
/// </summary>
namespace RoSS
{
    public class InventoryTab : MonoBehaviour, IInventoryItemOwner
    {
        [SerializeField] InventoryTabView _inventoryTabView;
        InventoryTabButton _inventoryTabButton;

        public ItemType Type;
        //public ItemTypeSO Type;

        void CreateItem(ItemSO itemSO)
        {
            GameObject itemGO = Instantiate<GameObject>(InventorySystem.Instance.InventorySettings.ItemPrefab);
            InventoryItem inventoryItem = itemGO.GetComponent<InventoryItem>();
            inventoryItem.Create(itemSO, this, GetItemsContainer());
        }

        InventoryTabButton CreateTabButton(Transform menuParent)
        {
            var inventoryTabButtonGO = Instantiate<GameObject>(InventorySystem.Instance.InventorySettings.MenuIconPrefab, menuParent);
            var inventoryTabButton = inventoryTabButtonGO.GetComponent<InventoryTabButton>();
            inventoryTabButton.Init(Type);
            return inventoryTabButton;
        }

        public void Init(ItemType type, List<ItemSO> itemList, Transform menuParent, out InventoryTabButton inventoryTabButton)
        {
            Type = type;
            _inventoryTabView.UpdateView(Type);

            foreach (var item in itemList)
                CreateItem(item);

            inventoryTabButton = CreateTabButton(menuParent);
            _inventoryTabButton = inventoryTabButton;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
            _inventoryTabButton.SetActive(active);

        }

        public Transform GetItemsContainer() => _inventoryTabView.GetItemsContainer();

        public void AddItem(InventoryItem item)
        {
            item.transform.SetParent(GetItemsContainer());
            item.SetOwner(this);
        }

    }
}