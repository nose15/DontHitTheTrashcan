using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public int renderDistance;
    public int platformSize;
    public int score;

    public float speed;

    private bool startPopulated;

    [SerializeField] GameObject wallPairPrefab;

    [SerializeField] ScoreManager scoreManager;

    [SerializeField] StateManager stateManager;

    [SerializeField] Transform character;
    [SerializeField] Transform gameLevel;

    public List<GameObject> wallPairs;

    // Start is called before the first frame update
    void Start()
    {
        wallPairs = new List<GameObject>();
        // instantiation of 300 wallPairs ahead of the player

        for (int i = 0; i <= renderDistance; i++)
        {
            Vector3 position = new Vector3(0f, 0f, i * platformSize);
            Quaternion rotation = new Quaternion(0, 1, 0, 1);
            GameObject platform = Instantiate(wallPairPrefab, position, rotation, gameLevel);

            wallPairs.Add(platform);
        }
    }

    void ShiftPlatform(GameObject shiftedWallPair)
    {
        Vector3 newPosition;

        newPosition = new Vector3(0f, 0f, shiftedWallPair.transform.position.z + renderDistance * platformSize - 7);

        shiftedWallPair.transform.position = newPosition;

        wallPairs.Remove(shiftedWallPair);
        wallPairs.Add(shiftedWallPair);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stateManager.gameState == StateManager.State.Play)
        {
            if (wallPairs[0].transform.position.z < character.position.z - 7)
            {
                scoreManager.score++;
                ShiftPlatform(wallPairs[0]);
            }
        }
        else if (stateManager.gameState == StateManager.State.Home)
        {
            score = 0;
        }
    }
}
