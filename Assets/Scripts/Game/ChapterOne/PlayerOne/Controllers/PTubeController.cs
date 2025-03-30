using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using TMPro;
using Cysharp.Threading.Tasks;

namespace Game.ChapterOne.PlayerOne.Controllers
{
    public class PTubeController : MonoBehaviour
    {
        public enum PTubeStates : int
        {
            Initial_State = 0,            // Input code: 1234 if correct, door state would say unlocked, door button activates, tube state would say Delivery Request Code: 4444
            Locked_State = 1,             // Player collects 2 items, upon collection, door closes, door state would say locked, numpad state would say ^_^
            After_Cryptex_State = 2,      // On player solve cryptex 4th digit, increase tube key state, input code: 4321, Update door state, update numpad state would say Delivery Request Code: 3333
            Locked_State_2 = 3,           // After Player Collects the gem, Door closes, door state would say locked, numpad state would say ^_^ 
            After_Key_Collect_state = 4,  // At this point, Unlock door, door state says Unlocked,
            After_Key_placement_state = 5, // upon placement of key, Increase tube key state, input code: 4444, upon input, door closes, tube state would say sending Code: 1111
            Done = 6,                      // Lock
        }

        [SerializeField]
        private GameScreenController TubeGameScreen;

        [SerializeField]
        private NumpadController numpadController;

        [SerializeField]
        private Button doorButton;

        [SerializeField]
        private GameObject doorClosedState;

        [SerializeField]
        private GameObject doorOpenState;

        [SerializeField]
        private Transform itemContainer;

        [SerializeField]
        private TextMeshProUGUI tubeStateTxt;

        [SerializeField]
        private TextMeshProUGUI numpadStateTxt;

        [SerializeField]
        private PTubeStateKeys pTubeKeys;

        private UnityAction onAnswerKeyCorrect;
        private int currentStateIdx = -1;
        private bool isLocked;

        public void Initialize(UnityAction onAnswerKeyCorrect, int initialState = 0)
        {
            this.onAnswerKeyCorrect = onAnswerKeyCorrect;
            TubeGameScreen.Initialize();
            numpadController.Initialize(OnNumpadSubmitClicked);
            currentStateIdx = initialState;
            UpdateDisplays();
        }

        #region Numpad Region

        private void OnNumpadSubmitClicked(string answer)
        {
            CheckAnswerKey(answer).Forget();
        }

        public void SetPTubeState(PTubeStates state)
        {
            currentStateIdx = (int)state;
        }

        public async UniTask CheckAnswerKey(string answerKey)
        {
            if (answerKey == pTubeKeys.numpadKeys[currentStateIdx].answerKey)
            {
                onAnswerKeyCorrect?.Invoke();
                Debug.Log("Correct! updating state");
            }
            else
            {
                numpadStateTxt.text = "Incorrect";
                numpadController.SetLock(true);
                await UniTask.Delay(500);
                numpadController.SetLock(false);
                numpadStateTxt.text = pTubeKeys.tubeStateKeyes[currentStateIdx];
            }
        }

        #endregion


        public void SetTubeDoorState(bool isLocked)
        {
            this.isLocked = isLocked;
            tubeStateTxt.text = isLocked ? "Locked" : "Unlocked";
            doorButton.enabled = !isLocked;
            ResetDoor();
        }

        public void ResetDoor()
        {
            doorClosedState.SetActive(true);
            doorOpenState.SetActive(false);
        }

        public void SetNumpadLock(bool isLocked)
        {
            numpadController.SetLock(isLocked);
        }

        public void ClearItemsInTube()
        {
            foreach (Transform t in itemContainer)
            {
                GameObject.Destroy(t.gameObject);
            }
        }

        public void UpdateDisplays()
        {
            numpadController.Clear();
            numpadStateTxt.text = pTubeKeys.tubeStateKeyes[currentStateIdx];
        }


        public void Destroy()
        {

        }
    }
}