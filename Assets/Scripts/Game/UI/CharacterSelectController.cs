using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterSelectController : MonoBehaviour
{
    [SerializeField]
    private Button playerOneSelect;

    [SerializeField]
    private Button playerTwoSelect;

    [SerializeField]
    private Button confirmationButton;

    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private GameObject ConfirmationWindow;

    private UnityAction<PlayerType> onConfirmClicked;

    private PlayerType selectedPlayerType;

    public void InitializeController(UnityAction<PlayerType> onConfirmClicked)
    {
        this.onConfirmClicked = onConfirmClicked;
        ConfirmationWindow.gameObject.SetActive(false);
        playerOneSelect.onClick.AddListener(() => { OnSelectClicked(true); });
        playerTwoSelect.onClick.AddListener(() => { OnSelectClicked(false); });
        confirmationButton.onClick.AddListener(OnConfirmButtonClicked);
        cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    private void OnSelectClicked(bool isPlayerOne)
    {
        ConfirmationWindow.gameObject.SetActive(true);
        selectedPlayerType = isPlayerOne ? PlayerType.PlayerOne : PlayerType.PlayerTwo;
    }

    private void OnConfirmButtonClicked()
    {
        confirmationButton.enabled = false;
        onConfirmClicked?.Invoke(selectedPlayerType);
    }

    private void OnCancelButtonClicked()
    {
        ConfirmationWindow.gameObject.SetActive(false);
        selectedPlayerType = PlayerType.None;
    }

    public void Destroy()
    {

    }
}
