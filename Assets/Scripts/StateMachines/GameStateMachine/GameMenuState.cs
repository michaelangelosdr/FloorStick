using Cysharp.Threading.Tasks;

public class GameMenuState : State
{
    private MainMenuController menuController;

    private DisposableFadeScreen fadeScreen;

    public GameMenuState(int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
    {
        fadeScreen = null;
    }

    public override async UniTask OnStart()
    {
        await AssetManager.Instance.LoadView<MainMenuController>("MainMenu", AssetParentType.CanvasOverlay,
            (menu) =>
            {
                menuController = menu;
                menuController.Initialize(OnStartButtonClicked);
            });
    }

    private async void OnStartButtonClicked()
    {
        fadeScreen = new DisposableFadeScreen();
        await fadeScreen.StartFadeIn();
        if (StateMachineManager.Instance.TryGetStateMachine(StateMachineId.Game, out StateMachine stateMachine))
        {
            stateMachine.ChangeState(GameStateId.CharacterSelect);
        }
    }

    public override async UniTask OnExit()
    {
        if (fadeScreen != null)
        {
            if (menuController != null)
            {
                menuController.Destroy();
            }

            await fadeScreen.StartFadeOut();
            fadeScreen.Dispose();
        }
    }
}
