using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryItemDropHandler : MonoBehaviour
{
    [SerializeField]
    private string targetItemId;

    [SerializeField]
    private Image glowImage;

    [SerializeField]
    private UnityEvent onItemInput;


    public void OnPointerEnter()
    {
        if (InventoryController.Instance.curHeldItemId == targetItemId)
        {
            glowImage.gameObject.SetActive(true);
        }
    }

    public void OnPointerUp()
    {
        if (InventoryController.Instance.curHeldItemId == targetItemId)
        {
            glowImage.gameObject.SetActive(false);
            onItemInput?.Invoke();
        }
    }

    public void OnPointerExit()
    {
        glowImage.gameObject.SetActive(false);
    }
}
