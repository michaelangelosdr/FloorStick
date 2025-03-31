using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameStateIdEnum startState;

    [SerializeField]
    public InventoryController InventoryController;

    private PlayerType playerType;
    public PlayerType PlayerType
    { 
        get
        {
            return playerType;
        }
    }

    private StateMachineManager stateMachineManager;

    public void Initialize()
    {
        InventoryController = InventoryController.Instance;
        InventoryController.Initialize();
        stateMachineManager = StateMachineManager.Instance.Initialize();
        StateMachineManager.Instance.AddNewStateMachine(StateMachineId.Game, new GameStateMachine(this, (int)startState));
    }

    private void Update()
    {
        if (stateMachineManager != null)
        {
            stateMachineManager.Refresh();
        }
    }

    public void SetPlayerType(PlayerType playerType)
    {
        this.playerType = playerType;
    }
}


[System.Serializable]
public enum PlayerType
{
    None,
    PlayerOne,
    PlayerTwo,
}