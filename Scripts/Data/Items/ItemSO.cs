using UnityEngine;

/// <summary>
/// Scriptable object for items
/// </summary>
public abstract class ItemSO : ScriptableObject
{
    [SerializeField] protected string _name;
    public string Name { get => _name; }

    [SerializeField] protected int _level;
    public int Level { get => _level; }

    [SerializeField] protected Sprite _icon;
    public Sprite Icon { get => _icon; }

    [SerializeField] protected ItemType _type;
    public ItemType Type { get => _type; }

    [SerializeField] private ItemRarity _itemRarity;
    public ItemRarity Rarity { get => _itemRarity; }

    
    
    //Deprecated    
    /*[SerializeField] protected ItemType _typeSO;
    public ItemType TypeSO { get => _typeSO; }

    [SerializeField] private ItemRaritySO _itemRaritySO;
    public ItemRaritySO RaritySO { get => _itemRaritySO; }*/

}
