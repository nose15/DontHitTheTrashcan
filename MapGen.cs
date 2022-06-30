using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField] Transform character;
    [SerializeField] PlatformSpawner platformSpawner;
    [SerializeField] StateManager stateManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] PlatformMapGen platformMapGen;

    public int rocketModeLength = 0;
    public int currentPlatformIndex = 0;

    [Range(1f, 10f)]
    public float platformSize = 1;

    void Update()
    {

        if (stateManager.gameState != StateManager.State.Idle)
        {
            if (currentPlatformIndex - character.position.z < 300)
            {
                bool[,] platformMap = platformMapGen.GetPlatformLayout();

                if (!platformMapGen.rocketMode) platformSpawner.CreateNewPlatform(currentPlatformIndex, platformSize, platformMap, PlatformSpawner.PlatformType.regular);
                else platformSpawner.CreateNewPlatform(currentPlatformIndex, platformSize, platformMap, PlatformSpawner.PlatformType.space);

                currentPlatformIndex++;
            }
            
        }
        else
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

            foreach (GameObject platform in platforms)
            {
                Object.Destroy(platform);
            }

            ResetValues();
        }
    }

    public void ResetValues()
    {
        currentPlatformIndex = 0;
        platformMapGen.ResetValues();
    }
}
