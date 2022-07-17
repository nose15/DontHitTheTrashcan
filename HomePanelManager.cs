using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanelManager : MonoBehaviour
{
    [SerializeField] StateManager stateManager;

    void Update()
    {
        if (stateManager.gameState == StateManager.State.Home) transform.GetChild(0).gameObject.SetActive(true);
        else transform.GetChild(0).gameObject.SetActive(false);
    }
}
