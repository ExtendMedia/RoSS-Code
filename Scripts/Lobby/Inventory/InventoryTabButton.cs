using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Controls the button for the inventory tab 
/// </summary>
public class InventoryTabButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject _iconFocusGO;
    [SerializeField] GameObject _iconUnderlineGO;
    ItemType _type;

    public event Action<ItemType> OnInventoryTabButtonClicked;

    public void OnPointerDown(PointerEventData eventData) => OnInventoryTabButtonClicked?.Invoke(_type);

    public void SetActive(bool active)
    {

        _iconFocusGO.SetActive(active);
        _iconUnderlineGO.SetActive(active);
    }

    void SetImage(Sprite image, Sprite imageFocus)
    {
        gameObject.GetComponent<Image>().sprite = image;
        _iconFocusGO.GetComponent<Image>().sprite = imageFocus;
    }

    public void Init(ItemType type)
    {
        _type = type;
        if (InventorySystem.Instance.InventorySettings.ItemTypesSettings.TryGetValue(type, out var itemTypeSettings))
        {
            SetImage(itemTypeSettings.MenuIcon, itemTypeSettings.MenuIconFocus);
        }
    }
}
