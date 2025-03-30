using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The state machine manager is a singleton and would control the states of each created statemachine.
/// </summary>
public class StateMachineManager : IEventObserver
{
    private static StateMachineManager instance;
    public static StateMachineManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new StateMachineManager();
            }
            return instance;
        }
    }

    private Dictionary<StateMachineId, StateMachine> stateMachineDictionary;
    private Dictionary<StateMachineId, StateMachine> stateMachinesObservingUpdate;

    public StateMachineManager Initialize()
    {
        stateMachineDictionary = new Dictionary<StateMachineId, StateMachine>();
        stateMachinesObservingUpdate = new Dictionary<StateMachineId, StateMachine>();
        EventManager.Instance.AddEventListener(EventId.UPDATE_STATE, this);
        return this;
    }

    public void OnEvent(EventId eventId, object payload)
    {
        if (eventId == EventId.UPDATE_STATE)
        {
            StateMachineInfo info = (StateMachineInfo)payload;
            if (stateMachineDictionary.ContainsKey(info.StateMachineId))
            {
                stateMachineDictionary[info.StateMachineId].ChangeState(info.stateIdx);
            }
        }
    }

    public void Refresh()
    {
        // NOTE: (ADR) not the best statemachine runner, update once we have the full game design
        foreach (StateMachineId key in stateMachineDictionary.Keys)
        {
            stateMachineDictionary[key].Refresh();
        }
    }

    public void AddNewStateMachine(StateMachineId stateMachineId, StateMachine stateMachine)
    {
        stateMachineDictionary.Add(stateMachineId, stateMachine);
    }

    public bool TryGetStateMachine(StateMachineId stateMachineId, out StateMachine stateMachine)
    {
        return stateMachineDictionary.TryGetValue(stateMachineId, out stateMachine);
    }

    public void RemoveStateMachine(StateMachineId stateMachineId)
    {
        // We dont remove the statemachine yet we set it first to inactive state.
        if (stateMachineDictionary.ContainsKey(stateMachineId))
        {
            stateMachineDictionary[stateMachineId].ChangeState(StateMachineConstants.INACTIVE_STATE_ID, () =>
            {
                stateMachineDictionary.Remove(stateMachineId);
            });
        }
    }

    public void Destroy()
    {
        foreach (StateMachineId key in stateMachineDictionary.Keys)
        {
            stateMachineDictionary[key].Destroy();
        }

        stateMachineDictionary.Clear();
    }
}
