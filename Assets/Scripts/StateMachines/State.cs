using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class State
{
    public int StateId;

    public bool ObserveUpdate;

    public State(int stateId, bool observeUpdate)
    {
        StateId = stateId;
        ObserveUpdate = observeUpdate;
    }

    public virtual async UniTask PreLoadState()
    {
        await UniTask.Yield();
    }

    public virtual async UniTask OnStart()
    {
        await UniTask.Yield();
    }

    public virtual async UniTask OnUpdate()
    {
        await UniTask.Yield();
    }

    public virtual async UniTask OnExit()
    {
        await UniTask.Yield();
    }
}
