using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class StateMachine
{
    public Dictionary<int, State> States;
    private State currentState;

    public StateMachineId StateMachineId;

    public StateMachine()
    {
        States = new Dictionary<int, State>();
    }

    public async void ChangeState(int stateId, Action onStateChangeDone = null)
    {
        if (!States.ContainsKey(stateId))
        {
            Debug.LogWarning("State does not exist: " + stateId);
            return;
        }

        State stateToLoad = States[stateId];

        await stateToLoad.PreLoadState();

        if (currentState != null)
        {
            await currentState.OnExit();
        }

        currentState = States[stateId];
        await currentState.OnStart();
        onStateChangeDone?.Invoke();
    }

    public void Refresh()
    {
        if (currentState != null && currentState.ObserveUpdate)
        {            
            currentState.OnUpdate().Forget();
        }
    }

    public virtual void Destroy()
    {       
        currentState = null;
        States.Clear();
    }
}