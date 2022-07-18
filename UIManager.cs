using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] StateManager stateManager;
    [SerializeField] CountdownTimer countdownTimer;

    [SerializeField] GameObject storeButton;
    [SerializeField] GameObject revivePanel;

    [SerializeField] Text scoreText;
    [SerializeField] Text coinsText;
    [SerializeField] Text gemsText;
    [SerializeField] Text reviveTokensText;
    [SerializeField] Text zoneActivatorsText;

    public int coins;
    public int gems;
    public int revivalTokens;
    public int zoneActivatorTokens;
    public int score;

    void UpdateStats()
    {
        coins = scoreManager.coins;
        gems = scoreManager.gems;
        revivalTokens = scoreManager.reviveTokens;
        zoneActivatorTokens = scoreManager.fuelCanisters;
        score = scoreManager.score;

        if (stateManager.gameState == StateManager.State.Play)
        {
            scoreText.text = score.ToString();
        }
        else if (stateManager.gameState != StateManager.State.Play)
        {
            scoreText.text = " ";
        }

        coinsText.text = coins.ToString();
        gemsText.text = gems.ToString();
        reviveTokensText.text = revivalTokens.ToString();
        zoneActivatorsText.text = zoneActivatorTokens.ToString();
    }

    void Start()
    {
        coins = 0;
        gems = 0;
        // later initialize with user stats
    }

    void Update()
    {
        UpdateStats();

        if (stateManager.gameState == StateManager.State.Lost)
        {
            revivePanel.SetActive(true);
            revivePanel.transform.GetChild(0).GetComponent<Text>().text = "Abolished \n\n" + Mathf.RoundToInt(countdownTimer.timeRemaining).ToString();
            revivePanel.transform.GetChild(1).gameObject.SetActive(true);
            revivePanel.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (stateManager.gameState == StateManager.State.Resuming)
        {
            revivePanel.SetActive(true);
            revivePanel.transform.GetChild(0).GetComponent<Text>().text = "Resuming \n\n" + Mathf.RoundToInt(countdownTimer.timeRemaining).ToString();
            revivePanel.transform.GetChild(1).gameObject.SetActive(false);
            revivePanel.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            revivePanel.SetActive(false);
        }

        if (stateManager.gameState == StateManager.State.Home)
        {
            storeButton.SetActive(true);
        }
        else
        {
            storeButton.SetActive(false);
        }

    }
}
