using System;
using UnityEngine;

public class DisposableLoadingScreen : IDisposable
{
    private Transform uIRoot;
    private string LOADING_SCREEN_PATH = "Prefabs/UI/DisposableLoadingScreen";
    private LoadingScreenController loadingScreenController;

    public DisposableLoadingScreen()
    {
        uIRoot = GameObject.FindGameObjectWithTag("UIRoot").transform;
        loadingScreenController = Resources.Load<LoadingScreenController>(LOADING_SCREEN_PATH);
        loadingScreenController = GameObject.Instantiate(loadingScreenController, uIRoot);
    }

    public void SetLoadingBarPercent(float percent)
    {
        if (loadingScreenController != null)
        {
            loadingScreenController.SetLoadingBarPercent(percent);
        }
    }

    public void Dispose()
    {
        GameObject.Destroy(loadingScreenController.gameObject);
        uIRoot = null;
    }
}
