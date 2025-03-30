using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    public void Initialize(UnityAction onStartClicked)
    {
        startButton.onClick.AddListener(() =>
        {
            onStartClicked?.Invoke();
            startButton.enabled = false;
        });
    }

    public void Destroy()
    {
        startButton.onClick.RemoveAllListeners();
        GameObject.Destroy(this.gameObject);
    }
}
