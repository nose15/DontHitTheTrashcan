using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] StateManager stateManager;

    public float defaultTimeRemaining;

    public bool started;

    public float timeRemaining;

    private void Start()
    {
        timeRemaining = defaultTimeRemaining;
    }

    public void ResetValues()
    {
        started = false;
        timeRemaining = defaultTimeRemaining;
    }

    void Update()
    {
        if (started && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            ResetValues();
        }

        if (stateManager.gameState == StateManager.State.Home)
        {
            ResetValues();
        }
    }
}
