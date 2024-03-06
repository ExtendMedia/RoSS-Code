using UnityEngine;

/// <summary>
/// Controls the inventory item
/// </summary>
public class InventoryItem : MonoBehaviour
{
    [SerializeField] InventoryItemView _inventoryItemView;

    ItemSO _itemSO;
    IInventoryItemOwner _owner;

    public void SetOwner(IInventoryItemOwner owner) => _owner = owner;
    public IInventoryItemOwner GetOwner() => _owner;

    public ItemType GetItemSOType() => _itemSO.Type;
    public ItemSO GetItemSO() => _itemSO;


    public void Create(ItemSO itemSO, IInventoryItemOwner owner, Transform parent)
    {
        _itemSO = itemSO;
        SetOwner(owner);
        gameObject.transform.SetParent(parent);
        gameObject.transform.localPosition = Vector3.zero;
        _inventoryItemView.UpdateView(_itemSO);
    }

}
