using Game.ChapterOne.PlayerOne.States;
using Game.ChapterOne.PlayerOne.Managers;
using Cysharp.Threading.Tasks;

public class POneChapterOneStateMachine : StateMachine
{
    private POneChapterOneManager chapterManager;

    public POneChapterOneStateMachine(POneChapterOneManager chapterManager, int startingState = 0) : base()
    {
        this.chapterManager = chapterManager;
        States = new System.Collections.Generic.Dictionary<int, State>();
        States.Add(POneChapterOneStateId.Initialization, new InitializationState(chapterManager, POneChapterOneStateId.Initialization));
        States.Add(POneChapterOneStateId.Start, new StartState(chapterManager, POneChapterOneStateId.Start));
        States.Add(POneChapterOneStateId.Ptube_Solved_1, new PTubeSolvedOneState(chapterManager, POneChapterOneStateId.Ptube_Solved_1));
        States.Add(POneChapterOneStateId.Cryptex_Placed, new CryptexPlacedState(chapterManager, POneChapterOneStateId.Cryptex_Placed));
        States.Add(POneChapterOneStateId.Cryptex_Solved_1, new CryptexSolvedOneState(chapterManager, POneChapterOneStateId.Cryptex_Solved_1));
        States.Add(POneChapterOneStateId.PTube_Solved_2, new PTubeSolvedTwoState(chapterManager, POneChapterOneStateId.PTube_Solved_2));
        States.Add(POneChapterOneStateId.Cryptex_Open, new CryptexOpenState(chapterManager, POneChapterOneStateId.Cryptex_Open));
        States.Add(POneChapterOneStateId.PTube_Solved_3, new PTubeSolvedThreeState(chapterManager, POneChapterOneStateId.PTube_Solved_3));
        States.Add(POneChapterOneStateId.Door_Sequence_idle, new DoorSequenceIdleState(chapterManager, POneChapterOneStateId.Door_Sequence_idle));
        States.Add(POneChapterOneStateId.Door_Sequence_solved, new DoorSequenceSolvedState(chapterManager, POneChapterOneStateId.Door_Sequence_solved));
        States.Add(POneChapterOneStateId.POneChapterOneEndState, new ChapterEndState(chapterManager, POneChapterOneStateId.POneChapterOneEndState));
    }
}


namespace Game.ChapterOne.PlayerOne.States
{

    public class InitializationState : State
    {
        private POneChapterOneManager chapterManager;

        public InitializationState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }

        public override async UniTask OnStart()
        {
            // Dont know what to do with this state yet, I should preload VFX and stuff here
            await UniTask.Yield();
        }
    }

    public class StartState : State
    {
        private POneChapterOneManager chapterManager;

        public StartState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
            // Idle start state
        }

        public override UniTask OnStart()
        {
            // InventoryManager.Clear();
            chapterManager.pTubeController.ClearItemsInTube();
            chapterManager.pTubeController.SetPTubeState(Controllers.PTubeController.PTubeStates.Initial_State);
            chapterManager.pTubeController.SetTubeDoorState(true);
            chapterManager.tableController.SetCryptexState(false);
            chapterManager.tableController.SetGemState(false);
            chapterManager.doorController.SetNumpadLockState(true);
            chapterManager.doorController.SetDoorLock(true);

            return base.OnStart();
        }
    }

    public class PTubeSolvedOneState : State
    {
        private POneChapterOneManager chapterManager;

        public PTubeSolvedOneState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }

        public override UniTask OnStart()
        {
            chapterManager.pTubeController.ClearItemsInTube();
            chapterManager.pTubeController.SetPTubeState(Controllers.PTubeController.PTubeStates.Locked_State);
            chapterManager.pTubeController.SetTubeDoorState(true);
            chapterManager.pTubeController.SetNumpadLock(true);
            chapterManager.pTubeController.UpdateDisplays();

            // Inventory manager.Add("Cryptex")
            // InventoryManager.Add("Note");

            return base.OnStart();
        }
    }

    public class CryptexPlacedState : State
    {
        private POneChapterOneManager chapterManager;

        public CryptexPlacedState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }


        public override UniTask OnStart()
        {
            //InventoryManager.Remove("Cryptex");
            chapterManager.tableController.SetCryptexState(true);
            return base.OnStart();
        }
    }

    public class CryptexSolvedOneState : State
    {
        private POneChapterOneManager chapterManager;

        public CryptexSolvedOneState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }

        public override UniTask OnStart()
        {
            chapterManager.pTubeController.SetNumpadLock(false);
            chapterManager.pTubeController.SetPTubeState(Controllers.PTubeController.PTubeStates.After_Cryptex_State);
            chapterManager.pTubeController.UpdateDisplays();
            return base.OnStart();
        }
    }

    public class PTubeSolvedTwoState : State
    {
        private POneChapterOneManager chapterManager;

        public PTubeSolvedTwoState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }

        public override UniTask OnStart()
        {
            //InventoryManager.Add("Gem");
            chapterManager.pTubeController.SetNumpadLock(true);
            chapterManager.pTubeController.SetPTubeState(Controllers.PTubeController.PTubeStates.Locked_State);
            chapterManager.pTubeController.UpdateDisplays();
            return base.OnStart();
        }
    }


    public class CryptexOpenState : State
    {
        private POneChapterOneManager chapterManager;

        public CryptexOpenState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }

        public override UniTask OnStart()
        {
            //InventoryManager.Add("Key");
            chapterManager.pTubeController.SetPTubeState(Controllers.PTubeController.PTubeStates.After_Key_Collect_state);
            chapterManager.pTubeController.SetTubeDoorState(false);
            chapterManager.pTubeController.SetNumpadLock(false);
            chapterManager.pTubeController.ResetDoor();
            chapterManager.pTubeController.UpdateDisplays();
            chapterManager.tableController.SetGemState(true);
            return base.OnStart();
        }
    }

    public class PTubeSolvedThreeState : State
    {
        private POneChapterOneManager chapterManager;

        public PTubeSolvedThreeState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }

        public override UniTask OnStart()
        {
            chapterManager.pTubeController.SetPTubeState(Controllers.PTubeController.PTubeStates.After_Key_placement_state);
            chapterManager.pTubeController.SetTubeDoorState(true);
            chapterManager.pTubeController.SetNumpadLock(true);
            chapterManager.pTubeController.ResetDoor();
            chapterManager.pTubeController.UpdateDisplays();

            if (StateMachineManager.Instance.TryGetStateMachine(StateMachineId.POneChapterOne, out StateMachine stateMachine))
            {
                stateMachine.ChangeState(POneChapterOneStateId.Door_Sequence_idle);
            }

            return base.OnStart();
        }
    }


    public class DoorSequenceIdleState : State
    {
        private POneChapterOneManager chapterManager;

        public DoorSequenceIdleState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }

        public override UniTask OnStart()
        {
            chapterManager.doorController.SetDoorLock(true);
            chapterManager.doorController.SetNumpadLockState(false);
            return base.OnStart();
        }
    }

    public class DoorSequenceSolvedState : State
    {
        private POneChapterOneManager chapterManager;

        public DoorSequenceSolvedState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
        }

        public override UniTask OnStart()
        {
            chapterManager.doorController.SetDoorLock(false);
            chapterManager.doorController.SetNumpadLockState(true);
            return base.OnStart();
        }
    }

    public class ChapterEndState : State
    {
        private POneChapterOneManager chapterManager;
        private DisposableFadeScreen fadeScreen;

        public ChapterEndState(POneChapterOneManager chapterManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {
            this.chapterManager = chapterManager;
            fadeScreen = null;
        }

        public override async UniTask OnStart()
        {
            UnityEngine.Debug.Log("TEEST");
            fadeScreen = new DisposableFadeScreen();
            await fadeScreen.StartFadeIn();

            if (StateMachineManager.Instance.TryGetStateMachine(StateMachineId.Game, out StateMachine stateMachine))
            {
                stateMachine.ChangeState(GameStateId.POneChapterOneEnd);
            }
        }

        public override async UniTask OnExit()
        {
            if (fadeScreen != null)
            {
                if (chapterManager != null)
                {
                    chapterManager.Destroy();
                }

                await fadeScreen.StartFadeOut();
                fadeScreen.Dispose();
            }
        }
    }
}