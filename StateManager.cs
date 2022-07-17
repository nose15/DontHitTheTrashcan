using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        Home,
        Shop,
        Resuming,
        Play,
        Idle,
        Lost
    }

    public State gameState;

    public IEnumerator StateTransition(State originState, State destinationState, float duration)
    {
        SetState(originState);

        yield return new WaitForSeconds(duration);

        SetState(StateManager.State.Idle);

        yield return new WaitForSeconds(0.5f);

        SetState(destinationState);

        yield return null;
    }

    public void SetState(State state)
    {
        gameState = state;
    }

    public void EnableShop(bool enable)
    {
        if (enable) SetState(State.Shop);
        else StartCoroutine(StateTransition(State.Shop, State.Home, 0.1f));
    }
}
