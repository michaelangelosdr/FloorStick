using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Game.Puzzles.Data;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.Puzzles.Inputs
{
    public class SpinnerInputController : MonoBehaviour
    {
        [SerializeField]
        private Button testButton;

        [SerializeField]
        private SpinnerInputConfig spinnerInputConfig;

        [SerializeField]
        private Transform spinnerItemContainer;

        private List<SpinnerInputItem> spinnerInputItems;
        private UnityAction onInputUpdated;
        private UnityAction onInputTargetAchieved;

        public void Initialize(UnityAction onInputUpdated, UnityAction onInputTargetAchieved)
        {
            this.onInputTargetAchieved = onInputTargetAchieved;
            this.onInputUpdated = onInputUpdated;
            spinnerInputItems = new List<SpinnerInputItem>();
            CreateInputItems().Forget();
            testButton.onClick.AddListener(TestOutput);
        }

        private async UniTask CreateInputItems()
        {
            for (int i = 0; i < spinnerInputConfig.inputChar.Count; i++)
            {
                int x = i;
                await AssetManager.Instance.LoadView<SpinnerInputItem>("SpinnerInputItem", AssetParentType.UIRoot,
                (spinnerItem) =>
                {
                    spinnerItem.Initialize(spinnerInputConfig.inputChar[x], OnInputUpdated);
                    spinnerItem.SetEnabled(true);
                    spinnerItem.transform.SetParent(spinnerItemContainer);
                    spinnerInputItems.Add(spinnerItem);
                });
            }
        }

        private void OnInputUpdated()
        {
            if (GetInput() == spinnerInputConfig.SpinnerTargetTxt)
            {
                onInputTargetAchieved?.Invoke();
            }

            onInputUpdated?.Invoke();
        }

        private void TestOutput()
        {
            Debug.Log(GetInput());
        }

        public string GetInput()
        {
            string input = string.Empty;

            for (int i = 0; i < spinnerInputItems.Count; i++)
            {
                input = input + spinnerInputItems[i].currentChar;
            }

            return input;
        }

        public void SetCryptexLockState(bool isLocked)
        {
            for (int i = 0; i < spinnerInputItems.Count; i++)
            {
                spinnerInputItems[i].SetEnabled(!isLocked);
            }
        }
    }
}