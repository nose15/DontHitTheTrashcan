using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        Home,
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
}
