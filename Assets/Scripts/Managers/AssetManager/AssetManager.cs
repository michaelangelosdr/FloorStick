using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class AssetManager : MonoBehaviour
{
    private static AssetManager instance;

    public static AssetManager Instance
    {
        get
        {
            if (instance == null)
            {
                AssetManager AM = Resources.Load<AssetManager>("Prefabs/Managers/AssetManager");
                AM = Instantiate(AM, GameObject.FindGameObjectWithTag("GameManager").transform);
                instance = AM;
            }

            return instance;
        }
    }

    [SerializeField]
    private Transform ui_canvas;

    [SerializeField]
    private Transform ui_root;

    [SerializeField]
    private Transform main_container;

    public async UniTask Initialize()
    {
        await Addressables.InitializeAsync().Task;
        instance = this;
    }
    public async UniTask LoadView<T>(string viewId, AssetParentType assetType, Action<T> onLoadDone) where T : new()
    {
        AsyncOperationHandle<GameObject> asyncOpHandle = Addressables.LoadAssetAsync<GameObject>(viewId);
        await asyncOpHandle.ToUniTask();

        if (asyncOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Transform parent = main_container;

            switch (assetType)
            {
                case AssetParentType.GameRoot:
                    parent = main_container;
                    break;
                case AssetParentType.CanvasOverlay:
                    parent = ui_canvas;
                    break;
                case AssetParentType.UIRoot:
                    parent = ui_root;
                    break;
            }

            GameObject gameobject = Instantiate(asyncOpHandle.Result, parent);
            onLoadDone?.Invoke(gameobject.GetComponent<T>());
        }
        else
        {
            Debug.Log("Failed to load");
        }
    }

    public void DestroyView(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    public void Destroy()
    {

    }
}
