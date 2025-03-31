using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InventoryItemCollection", menuName = "Game/Inventory/Item Collection")]
public class InventoryItemCollection : ScriptableObject
{
    [SerializeField]
    public List<InventoryItemData> ItemList = new List<InventoryItemData>();
}