using UnityEngine;
using Game.ChapterOne.PlayerOne.States;
using Game.ChapterOne.PlayerOne.Controllers;

namespace Game.ChapterOne.PlayerOne.Managers
{
    public class POneChapterOneManager : MonoBehaviour
    {
        [SerializeField]
        private POneChapterOneConfig config;

        [SerializeField]
        public BookshelfController bookshelfController;

        [SerializeField]
        public DoorController doorController;

        [SerializeField]
        public TableController tableController;

        [SerializeField]
        public PTubeController pTubeController;

        private POneChapterOneStateMachine pOneChapterOneStateMachine;

        public POneChapterOneStateIdEnum currentStateEnum;
        private int currentStateIdx;

        public void InitializeManager()
        {
            pOneChapterOneStateMachine = new POneChapterOneStateMachine(this);
            StateMachineManager.Instance.AddNewStateMachine(StateMachineId.POneChapterOne, pOneChapterOneStateMachine);
            currentStateIdx = config.GetInitialState();
            pOneChapterOneStateMachine.ChangeState(currentStateIdx);

            bookshelfController.Initialize();
            doorController.Initialize(OnChapterOneEnd);
            doorController.SetNumpadLockState(false);
            tableController.Initialize(UpdateState);
            pTubeController.Initialize(UpdateState);
        }

        bool isDown = false;
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isDown == false)
                {
                    UpdateState();
                }

                isDown = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isDown = false;
            }
        }

        public void UpdateState()
        {
            currentStateIdx++;
            pOneChapterOneStateMachine.ChangeState(currentStateIdx);
            currentStateEnum = (POneChapterOneStateIdEnum)currentStateIdx;
        }

        public void OnChapterOneEnd()
        {
            Debug.Log("DONE");
            // Change state to end
            pOneChapterOneStateMachine.ChangeState(POneChapterOneStateId.POneChapterOneEndState);
        }

        public void Destroy()
        {
            bookshelfController.Destroy();
            doorController.Destroy();
            tableController.Destroy();
            pTubeController.Destroy();

            Destroy(this.gameObject);
        }
    }
}
