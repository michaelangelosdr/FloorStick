using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Game.Puzzles.Inputs
{
    public class SpinnerInputItem : MonoBehaviour
    {
        [SerializeField]
        private Button upButton;

        [SerializeField]
        private Button downButton;

        [SerializeField]
        private TextMeshProUGUI spinnerTxt;

        private string currentSpinnerTxt;
        public string currentChar
        {
            get
            {
                return currentSpinnerTxt;
            }
        }

        private int inputIdx;
        private string inputChars;

        private UnityAction onInputUpdate;

        public void Initialize(string inputChars, UnityAction onInputUpdate)
        {
            this.onInputUpdate = onInputUpdate;
            this.inputChars = inputChars;
            inputIdx = 0;
            currentSpinnerTxt = inputChars[inputIdx].ToString();
            UpdateDisplay();

            upButton.onClick.AddListener(() => { CycleInput(true); });
            downButton.onClick.AddListener(() => { CycleInput(false); });
        }

        private void CycleInput(bool isUp)
        {
            if (isUp)
            {
                inputIdx++;
            }
            else
            {
                inputIdx--;
            }

            if (inputIdx >= inputChars.Length)
            {
                inputIdx = 0;
            }
            else if (inputIdx < 0)
            {
                inputIdx = inputChars.Length - 1;
            }

            currentSpinnerTxt = inputChars[inputIdx].ToString();
            onInputUpdate?.Invoke();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            spinnerTxt.text = currentSpinnerTxt;
        }

        public void SetEnabled(bool enabled)
        {
            upButton.gameObject.SetActive(enabled);
            downButton.gameObject.SetActive(enabled);
        }
    }
}