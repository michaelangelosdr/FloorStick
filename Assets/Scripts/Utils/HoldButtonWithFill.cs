using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HoldButtonWithFill : MonoBehaviour
{
    [SerializeField]
    private float holdDuration = 2.0f;

    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Image blockerImage;

    private UnityAction onHoldComplete;

    private bool isHolding = false;
    private float holdTime = 0f;

    public void Initialize(UnityAction onHoldComplete)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }

        this.onHoldComplete = onHoldComplete;
    }

    public void Enable(bool isEnabled)
    {
        blockerImage.gameObject.SetActive(!isEnabled);
    }

    private void Update()
    {
        if (isHolding)
        {
            holdTime += Time.deltaTime;
            if (fillImage != null)
            {
                fillImage.fillAmount = holdTime / holdDuration;
            }

            if (holdTime >= holdDuration)
            {
                isHolding = false;
                holdTime = 0f;
                if (fillImage != null)
                {
                    fillImage.fillAmount = 0f;
                }

                // Trigger the event
                onHoldComplete?.Invoke();
            }
        }
    }

    public void OnPointerDown()
    {
        isHolding = true;
        holdTime = 0f;
    }

    public void OnPointerUp()
    {
        isHolding = false;
        holdTime = 0f;
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }
    }
}