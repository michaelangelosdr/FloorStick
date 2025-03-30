using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Cysharp.Threading.Tasks;

namespace Game.ChapterOne.PlayerOne.Controllers
{
    public class DoorController : GameScreenController
    {
        [SerializeField]
        private TextMeshProUGUI lockStateTxt;

        [SerializeField]
        private HoldButtonWithFill doorHoldButton;

        [SerializeField]
        private GameObject doorLockGameObject;

        [SerializeField]
        private NumpadController numpadController;

        [SerializeField]
        private NumpadConfig config;

        private bool lockState;

        public void Initialize(UnityAction onChapterOneDone)
        {
            numpadController.Initialize(OnNumpadClicked);
            doorHoldButton.Initialize(onChapterOneDone);
            doorHoldButton.Enable(false);
            base.Initialize();
        }

        private void OnNumpadClicked(string key)
        {
            CheckKey(key).Forget();
        }

        public async UniTask CheckKey(string key)
        {
            if (key == config.answerKey)
            {
                doorLockGameObject.gameObject.SetActive(false);
                doorHoldButton.Enable(true);
                lockStateTxt.text = "Door Activated";
            }
            else
            {
                lockStateTxt.text = "Incorrect";
                numpadController.SetLock(true);
                await UniTask.Delay(500);
                numpadController.SetLock(false);
                lockStateTxt.text = lockState ? "Unlocked" : "Unlocked";
            }
        }
        
        public void SetDoorLock(bool isLocked)
        {
            doorLockGameObject.gameObject.SetActive(isLocked);
            doorHoldButton.Enable(!isLocked);
        }

        public void SetNumpadLockState(bool isLocked)
        {
            lockState = isLocked;
            lockStateTxt.text = isLocked ? "Locked" : "Unlocked";
            numpadController.SetLock(isLocked);
        }


        public void Destroy()
        {

        }
    }
}