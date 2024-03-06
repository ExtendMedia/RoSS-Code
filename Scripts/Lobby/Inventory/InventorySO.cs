using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object for the inventory
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Data/Inventory/New Inventory")]
public class InventorySO : ScriptableObject
{

    [SerializeField] List<ItemSO> _items;
    public List<ItemSO> Items { get => _items; }


}
