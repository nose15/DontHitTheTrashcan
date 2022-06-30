using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActive : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    Button button;

    private void Start()
    {
        button = transform.GetComponent<Button>();
    }

    void Update()
    {
        if (scoreManager.reviveTokens > 0) button.interactable = true;
        else button.interactable = false;
    }
}
