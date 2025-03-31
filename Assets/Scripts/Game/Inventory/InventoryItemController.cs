using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField]
    public Image itemIcon;

    [SerializeField]
    public Image glowImage;

    public InventoryItemData InventoryItemData;


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

    public void OnPointerExit()
    {
        glowImage.gameObject.SetActive(false);

    }
}
