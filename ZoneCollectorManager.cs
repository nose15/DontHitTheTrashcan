using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCollectorManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    public bool zoneMode;

    void Start()
    {
        zoneMode = false;
    }

    void OnTriggerEnter(Collider hitObject)
    {
        if (zoneMode && hitObject.CompareTag("Coin"))
        {
            scoreManager.AddCoins();
            hitObject.gameObject.SetActive(false);
        }
    }
}
