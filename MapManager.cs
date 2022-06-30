using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int renderDistance;
    public int platformSize;
    public int score;

    public float speed;

    private bool startPopulated;

    [SerializeField] GameObject platformPrefab;

    [SerializeField] ScoreManager scoreManager;

    [SerializeField] StateManager stateManager;

    [SerializeField] PlatformPopulator platformPopulator;

    [SerializeField] Transform character;
    [SerializeField] Transform gameLevel;

    public List<GameObject> platforms;
    
    // Start is called before the first frame update
    void Start()
    {
        platforms = new List<GameObject>();
        // instantiation of 300 platforms ahead of the player

        for (int i = 0; i <= renderDistance; i++)
        {
            Vector3 position = new Vector3(0f, 0f, i * platformSize);
            GameObject platform = Instantiate(platformPrefab, position, platformPrefab.transform.rotation, gameLevel);

            platformPopulator.Populate(platform, false);

            platforms.Add(platform);
        }
    }

    void ShiftPlatform(GameObject shiftedPlatform)
    {
        Vector3 newPosition;

        bool rocketMode = platformPopulator.platformMapGen.rocketMode;

        if (!rocketMode) newPosition = new Vector3(0f, 0f, shiftedPlatform.transform.position.z + renderDistance * platformSize - 7);
        else newPosition = new Vector3(0f, 25f, shiftedPlatform.transform.position.z + renderDistance * platformSize - 7);

        shiftedPlatform.transform.position = newPosition;

        platformPopulator.Populate(shiftedPlatform, rocketMode);

        platforms.Remove(shiftedPlatform);
        platforms.Add(shiftedPlatform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stateManager.gameState == StateManager.State.Play)
        {
            startPopulated = false;

            if (platforms[0].transform.position.z < character.position.z - 7)
            {
                scoreManager.score++;
                ShiftPlatform(platforms[0]);
            }
        }
        else if (stateManager.gameState == StateManager.State.Home)
        {
            if (!startPopulated)
            {
                for (int i = 0; i <= renderDistance; i++)
                {
                    platformPopulator.platformMapGen.ResetValues();

                    platformPopulator.Populate(platforms[i], false);
                }

                startPopulated = true;
            }
            
            score = 0;
        }
    }
}
