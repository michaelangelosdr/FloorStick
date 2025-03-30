using UnityEngine;

public class CameraRatioScaler : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private int prevWidth;
    private int prevHeight;

    public void Start()
    {
        prevWidth = Screen.width;
        prevHeight = Screen.height;
    }

    private void Update()
    {
        if (Screen.width != prevWidth || Screen.height != prevHeight)
        {
            AdjustCameraViewport();
            prevHeight = Screen.height;
            prevWidth = Screen.width;
        }
    }

    /// <summary>
    /// Camera script to adjust the camera view upon weird resolutions
    /// </summary>
    /// <param name="mainCamera"></param>
    public void AdjustCameraViewport()
    {
        float targetRatio = 16.0f / 9.0f;
        float windowScreen = Screen.width / Screen.height;
        float scaleHeight = windowScreen / targetRatio;

        if (scaleHeight < 1.0f)
        {
            Rect rect = mainCamera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            mainCamera.rect = rect;
        }
        else
        {
            float scaleWidht = 1.0f / scaleHeight;

            Rect rect = mainCamera.rect;

            rect.width = scaleWidht;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidht) / 2.0f;
            rect.y = 0;

            mainCamera.rect = rect;
        }
    }
}
