using TMPro;
using UnityEngine;

/// <summary>
/// Inventory tab visuals
/// </summary>
public class InventoryTabView : MonoBehaviour
{
    [SerializeField] TMP_Text _tabTitle;
    [SerializeField] Transform _itemsContainer;


    void SetParent(Transform item)
    {
        item.SetParent(_itemsContainer);
    }

    void SetTitle(string title) => _tabTitle.text = title;

    public Transform GetItemsContainer() => _itemsContainer;

    public void UpdateView(ItemType type)
    {
        SetTitle(type.ToString());
    }

}
