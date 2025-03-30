using UnityEngine;

public class GameTaskInitiator : MonoBehaviour
{
    [SerializeField]
    private AssetManager assetManager;

    [SerializeField]
    private GameManager gameManager;


    private async void Start()
    {
        using (var loadingScreen = new DisposableLoadingScreen())
        {
            loadingScreen.SetLoadingBarPercent(0);
            await assetManager.Initialize();
            loadingScreen.SetLoadingBarPercent(0.5f);
            gameManager.Initialize();
            await Awaitable.WaitForSecondsAsync(0.1f);
            loadingScreen.SetLoadingBarPercent(0.99f);
            await Awaitable.WaitForSecondsAsync(0.5f);
        }

        GameObject.Destroy(this.gameObject);
    }
}

