using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{

    [SerializeField]
    private UnityEngine.UI.Slider slider;

    public void SetLoadingBarPercent(float percent)
    {
        slider.value = percent;
    }
}