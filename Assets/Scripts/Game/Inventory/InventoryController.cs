using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    private static InventoryController instance;
    public static InventoryController Instance
    {
        get
        {
            if (instance == null)
            {
                InventoryController Inventory = Resources.Load<InventoryController>("Prefabs/Managers/Inventory");
                Inventory = Instantiate(Inventory, GameObject.FindGameObjectWithTag("CanvasOverlay").transform);
                instance = Inventory;
            }

            return instance;
        }
    }

    [SerializeField]
    private Button inventoryToggleBtn;

    [SerializeField]
    private GameObject inventoryPopup;

    [SerializeField]
    private Transform inventoryItemContainer;

    [SerializeField]
    private InventoryItemCollection itemCollection;

    private bool isShowInventory;
    public bool IsShowInventory
    {
        get
        {
            return isShowInventory;
        }
    }

    private Dictionary<string, InventoryItemData> currentInventoryItems;

    private List<InventoryItemController> inventoryItemControllers;

    public void Initialize(bool isShowInventory = false)
    {
        currentInventoryItems = new Dictionary<string, InventoryItemData>();
        inventoryItemControllers = new List<InventoryItemController>();
        inventoryToggleBtn.onClick.AddListener(OnShowInventoryToggleClicked);
        this.isShowInventory = isShowInventory;
        inventoryPopup.SetActive(isShowInventory);
    }

    public void AddItem(string itemId)
    {
        int idx = itemCollection.ItemList.FindIndex(x => x.inventoryItemId == itemId);

        if (idx == -1)
        {
            return;
        }

        InventoryItemData itemToGet = itemCollection.ItemList[idx];
        currentInventoryItems.Add(itemToGet.inventoryItemId, itemToGet);
        UpdateInventoryDisplays();
    }

    public void RemoveItem(string itemId)
    {
        if (currentInventoryItems.ContainsKey(itemId))
        {
            currentInventoryItems.Remove(itemId);
            UpdateInventoryDisplays();
        }
    }

    public void OnShowInventoryToggleClicked()
    {
        isShowInventory = !isShowInventory;

        if (isShowInventory)
        {
            UpdateInventoryDisplays();
        }

        inventoryPopup.gameObject.SetActive(isShowInventory);
    }

    public void UpdateInventoryDisplays()
    {
        ClearPool();

        foreach (string id in currentInventoryItems.Keys)
        {
            string x = id;
            InventoryItemController inventoryItem = GetPoolItem();
            inventoryItem.SetItemData(currentInventoryItems[x]);
            inventoryItem.UpdateDisplay();
            inventoryItem.gameObject.SetActive(true);
        }
    }

    private void ClearPool()
    {
        for (int i = 0; i < inventoryItemControllers.Count; i++)
        {
            inventoryItemControllers[i].gameObject.SetActive(false);
        }
    }

    private InventoryItemController GetPoolItem()
    {
        for (int i = 0; i < inventoryItemControllers.Count; i++)
        {
            if (!inventoryItemControllers[i].gameObject.activeInHierarchy)
            {
                return inventoryItemControllers[i];
            }
        }

        InventoryItemController InventoryItem = Resources.Load<InventoryItemController>("Prefabs/UI/InventoryItem");
        InventoryItem = Instantiate(InventoryItem, inventoryItemContainer);
        inventoryItemControllers.Add(InventoryItem);
        return InventoryItem;
    }
}
