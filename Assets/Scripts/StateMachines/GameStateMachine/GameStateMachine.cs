using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.States;

public class GameStateMachine : StateMachine
{
    public GameStateMachine(GameManager gameManager, int startingState = 0) : base()
    {
        States = new Dictionary<int, State>();
        States.Add(GameStateId.Menu, new GameMenuState(GameStateId.Menu));
        States.Add(GameStateId.CharacterSelect, new CharacterSelectState(gameManager, GameStateId.CharacterSelect));
        States.Add(GameStateId.POneChapterOne, new POneChapterOneState(GameStateId.POneChapterOne));
        States.Add(GameStateId.POneChapterOneEnd, new POneChapterOneEndState(GameStateId.POneChapterOneEnd));
        ChangeState(startingState);
    }
}

public class GameStateId
{
    public static readonly int Menu = 0;
    public static readonly int CharacterSelect = 1;
    public static readonly int POneChapterOne = 2;
    public static readonly int PTwoChapterOne = 3;
    public static readonly int POneChapterOneEnd = 4;
}

[System.Serializable]
public enum GameStateIdEnum
{
    Menu = 0,
    CharacterSelect = 1,
    POneChapterOne = 2,
}

namespace Game.States
{
    public class POneChapterOneEndState : State
    {
        public POneChapterOneEndState(int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {

        }

        public override async UniTask PreLoadState()
        {
            StateMachineManager.Instance.RemoveStateMachine(StateMachineId.POneChapterOne);
            await AssetManager.Instance.LoadView<MainMenuController>("MainMenu", AssetParentType.CanvasOverlay,
                (menu) =>
                {

                });
        }
    }
}
/*
 * Enigma machine buttons
 * Time -> Hour glass 1 1
 * Death -> Skull 1 <-
 * Heart -> Heart 1 1
 * Power -> Lightning
 * War -> Swords 1 <-
 * Protect -> Shield 1 1
 * Game -> Controller 1 <-
 * Enemies -> Evil / Devil (?) 1 
 * World -> Earth 1 1
 * Justice -> Scales <-
 *
 *Book answer titles
 * Family
 * Gear -> Shorten
 * Shield -> shorten
 * Game
 * 
 * 
 */
