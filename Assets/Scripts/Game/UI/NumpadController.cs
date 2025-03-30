using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using TMPro;

public class NumpadController : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> numpadTxts;

    [SerializeField]
    private Button clearButton;

    [SerializeField]
    private Button enterButton;

    [SerializeField]
    private GameObject numpadLock;

    private string input;
    public string Input
    {
        get
        {
            return input;
        }
    }

    private UnityAction<string> onSubmitClicked;

    public void Initialize(UnityAction<string> onSubmitClicked)
    {
        this.onSubmitClicked = onSubmitClicked;
        clearButton.onClick.AddListener(OnClearClick);
        enterButton.onClick.AddListener(OnSubmitClick);
        input = string.Empty;
        UpdateDisplay();
    }

    // This function is attached directly in the editor
    public void OnButtonClick(string value)
    {
        if (input.Length < numpadTxts.Count)
        {
            input += value;
            UpdateDisplay();
        }
    }

    public void Clear()
    {
        input = string.Empty;
        UpdateDisplay();
    }

    private void OnClearClick()
    {
        Clear();
    }

    public void OnSubmitClick()
    {
        onSubmitClicked?.Invoke(input);
        Clear();
    }

    public void SetLock(bool isLocked)
    {
        numpadLock.gameObject.SetActive(isLocked);
    }

    // Method to update the display
    private void UpdateDisplay()
    {
        // Clear all display texts
        for (int i = 0; i < numpadTxts.Count; i++)
        {
            numpadTxts[i].text = string.Empty;
        }

        // Display each character in the corresponding text element
        for (int i = 0; i < input.Length; i++)
        {
            numpadTxts[i].text = input[i].ToString();
        }
    }
}
