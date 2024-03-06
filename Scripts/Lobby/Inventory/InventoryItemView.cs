using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the inventory items visuals
/// </summary>
public class InventoryItemView : MonoBehaviour
{
    [SerializeField] TMP_Text _levelText;
    [SerializeField] Image _icon;
    [SerializeField] Image _frame;

    void SetIcon(Sprite icon) => _icon.sprite = icon;

    void SetLevel(int level) => _levelText.text = level.ToString();

    void SetFrame(Sprite frame) => _frame.sprite = frame;

    public void UpdateView(ItemSO itemSO)
    {
        if (InventorySystem.Instance.InventorySettings.ItemRaritySettings.TryGetValue(itemSO.Rarity, out var itemRaritySettings))
            SetFrame(itemRaritySettings.ItemFrame);

        SetIcon(itemSO.Icon);
        SetLevel(itemSO.Level);
    }



}
