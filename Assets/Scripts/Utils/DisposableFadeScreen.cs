using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class DisposableFadeScreen : IDisposable
{
    private Transform uIRoot;
    private string FADE_SCREEN_PATH = "Prefabs/UI/DisposableFadeScreen";
    private FadeScreenController fadeScreenController;

    public DisposableFadeScreen()
    {
        uIRoot = GameObject.FindGameObjectWithTag("UIRoot").transform;
        fadeScreenController = Resources.Load<FadeScreenController>(FADE_SCREEN_PATH);
        fadeScreenController = GameObject.Instantiate(fadeScreenController, uIRoot);
    }

    public void SetOpacity(float opacity)
    {
        fadeScreenController.SetFadeScreenOpacity(opacity);
    }

    public async UniTask StartFadeIn(float fadeSpeed = 1f)
    {
       await fadeScreenController.StartFadeIn(fadeSpeed);
    }

    public async UniTask StartFadeOut(float fadeSpeed = 1f)
    {
        await fadeScreenController.StartFadeOut(fadeSpeed);
    }

    public void Dispose()
    {
        fadeScreenController.Destroy();
        fadeScreenController = null;
        uIRoot = null;        
    }
}