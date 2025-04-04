using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField]
    public Image itemIcon;

    [SerializeField]
    public Image glowImage;

    public InventoryItemData InventoryItemData;

    private bool isUsed;
    public bool IsUsed
    {
        get
        {
            return isUsed;
        }
    }

    public void SetActiveState(bool isState)
    {
        this.isUsed = isState;
    }

    public void SetItemData(InventoryItemData inventoryItemData)
    {
        this.InventoryItemData = inventoryItemData;
    }

    public void UpdateDisplay()
    {
        itemIcon.sprite = InventoryItemData.inventoryIcon;
    }

    public void OnPointerEnter()
    {
        glowImage.gameObject.SetActive(true);
    }

    public void OnBeginDrag()
    {
        if (InventoryItemData.isDraggable)
        {
            InventoryController.Instance.OnItemDragStart(InventoryItemData);
        }
    }

    public void OnEndDrag()
    {
        if (InventoryItemData.isDraggable)
        {
            InventoryController.Instance.OnItemDragEnd();
        }
    }

    public void OnPointerExit()
    {
        glowImage.gameObject.SetActive(false);
    }
}