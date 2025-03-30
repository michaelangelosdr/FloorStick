using Cysharp.Threading.Tasks;

public class CharacterSelectState : State
{
    private GameManager gameManager;
    private CharacterSelectController charSelectController;

    private DisposableFadeScreen fadeScreen;

    public CharacterSelectState(GameManager gameManager, int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
    {
        this.gameManager = gameManager;
        fadeScreen = null;
    }

    public override async UniTask PreLoadState()
    {
        await AssetManager.Instance.LoadView<CharacterSelectController>("CharacterSelect", AssetParentType.CanvasOverlay,
            (charSelect) =>
            {
                charSelectController = charSelect;
                charSelectController.InitializeController(OnPlayerSelect);
            });
    }

    private async void OnPlayerSelect(PlayerType playerType)
    {
        gameManager.SetPlayerType(playerType);
        fadeScreen = new DisposableFadeScreen();
        await fadeScreen.StartFadeIn();
        if (StateMachineManager.Instance.TryGetStateMachine(StateMachineId.Game, out StateMachine stateMachine))
        {
            if (playerType == PlayerType.PlayerOne)
            {
                stateMachine.ChangeState(GameStateId.POneChapterOne);
            }
            else if (playerType == PlayerType.PlayerTwo)
            {
                stateMachine.ChangeState(GameStateId.PTwoChapterOne);
            }
        }
    }

    public override async UniTask OnExit()
    {
        if (fadeScreen != null)
        {
            if (charSelectController != null)
            {
                charSelectController.Destroy();
            }

            await fadeScreen.StartFadeOut();
            fadeScreen.Dispose();
        }
    }
}
