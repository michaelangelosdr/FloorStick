using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Game/Inventory/Item")]
public class InventoryItemData : ScriptableObject
{
    [SerializeField]
    public string inventoryItemId;

    [SerializeField]
    public Sprite inventoryIcon;

    [SerializeField]
    public bool isDraggable;
}
