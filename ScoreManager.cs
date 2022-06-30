using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] MapManager mapManager;
    [SerializeField] StateManager stateManager;
    [SerializeField] PlatformMapGen platformMapGen;
    [SerializeField] CharMovement charMovement;
    [SerializeField] ZoneCollectorManager zoneManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] CountdownTimer countdownTimer;

    System.DateTime zoneStart;
    System.DateTime currentTime;

    System.TimeSpan zoneDuration;

    [Header("Constant Values")]
    public float speedIncreaseValue;
    public int coinValue;
    public int zoneDurationInSeconds;
    public int speedIncreaseInterval;

    [Header("Assets")]
    public int score;
    public int coins;
    public int gems;
    public int reviveTokens;
    public int zoneActivatorTokens;

    [Header("Default Values")]
    public int defaultHelmetDurability = 3;
    public int revivesLeft = 2;

    [Header("Special Modes")]
    public bool rocketModeRequest = false;
    public bool helmetMode = false;

    [Header("Temporary Values")]
    public int helmetDurability = 3;
    public int currentRevivesLeft;

    void Start()
    {
        coins = 0;
        gems = 0;
        reviveTokens = 3;
        zoneActivatorTokens = 3;

        currentRevivesLeft = revivesLeft;

        zoneDuration = new System.TimeSpan(0, 0, zoneDurationInSeconds);

        helmetDurability = defaultHelmetDurability;
        score = 0;
    }

    public void ResetValues()
    {
        currentRevivesLeft = revivesLeft;
        score = 0;
    }

    public void DecrementReviveTokens()
    {
        reviveTokens--;
    }

    public void ResumeGame()
    {
        currentRevivesLeft--;

        countdownTimer.ResetValues();

        StopAllCoroutines();

        countdownTimer.started = true;
        StartCoroutine(stateManager.StateTransition(StateManager.State.Resuming, StateManager.State.Play, 5f));
    }

    private void LostGameProcedure(GameObject hitObject)
    {
        Transform platform = hitObject.transform.parent.parent;

        for (int i = 0; i < 7; i++)
        {
            platform.GetChild(i + 1).gameObject.SetActive(false);
        }

        if (currentRevivesLeft > 0)
        {
            countdownTimer.started = true;
            StartCoroutine(stateManager.StateTransition(StateManager.State.Lost, StateManager.State.Home, 5f));
        }
        else
        {
            stateManager.SetState(StateManager.State.Home);
        }
    }

    public void AddCoins()
    {
        coins += coinValue;
    }

    private void OnTriggerEnter(Collider hitObject)
    {
        if (hitObject.CompareTag("Coin"))
        {
            AddCoins();
            hitObject.gameObject.SetActive(false);
        }

        if (hitObject.CompareTag("Rocket"))
        {
            rocketModeRequest = true;
            hitObject.gameObject.SetActive(false);
        }

        if (hitObject.CompareTag("Helmet"))
        {
            helmetMode = true;
            helmetDurability = defaultHelmetDurability;
            hitObject.gameObject.SetActive(false);
        }

        if (hitObject.CompareTag("Wall"))
        {
            if (!helmetMode)
            {
                LostGameProcedure(hitObject.gameObject);
            }
            else
            {
                helmetDurability--;
                hitObject.gameObject.SetActive(false);
            }
        }

        if (hitObject.CompareTag("Bomb"))
        {
            LostGameProcedure(hitObject.gameObject);
        }

        if (0 != score && score % speedIncreaseInterval == 0 && mapManager.speed < 2.5f) mapManager.speed += speedIncreaseValue;
    }

    private void Update()
    {
        currentTime = System.DateTime.Now;

        if (inputManager.zoneRequest && zoneActivatorTokens > 0)
        {
            zoneActivatorTokens--;

            inputManager.zoneRequest = false;

            zoneManager.zoneMode = true;
            zoneStart = System.DateTime.Now;
        }

        if (helmetDurability == 0)
        {
            helmetMode = false;
            helmetDurability = defaultHelmetDurability;
        }

        if (currentTime - zoneStart > zoneDuration) zoneManager.zoneMode = false;

        if (stateManager.gameState == StateManager.State.Home)
        {
            ResetValues();
        }
    }
}
