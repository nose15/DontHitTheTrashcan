using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMapGen : MonoBehaviour
{
    [SerializeField] Material regularMaterial;
    [SerializeField] Material rocketModeMaterial;

    [SerializeField] ScoreManager scoreManager;
    [SerializeField] CharMovement charMovement;

    private bool[] chainCoordinates = { false, false, false };
    readonly private bool[] randBool = { true, false, false, false, false, false, false, false };

    [Header("Obstacle")]
    [Range(2f, 50f)]
    public int startingBuffer;
    [Range(2f, 50f)]
    public int minObstacleDist = 2;
    [Range(2f, 50f)]
    public int maxObstacleDist = 2;

    [Header("Blank Space")]
    [Range(100f, 2000f)]
    public int minBlankSpaceDist = 300;
    [Range(100f, 2000f)]
    public int maxBlankSpaceDist = 300;
    [Range(20f, 200f)]
    public int minBlankSpaceLength = 100;
    [Range(20f, 200f)]
    public int maxBlankSpaceLength = 100;

    [Header("Chain")]
    [Range(20f, 200f)]
    public int minChainDistance = 100;
    [Range(20f, 200f)]
    public int maxChainDistance = 100;
    [Range(20f, 200f)]
    public int minChainLength = 100;
    [Range(20f, 200f)]
    public int maxChainLength = 100;

    [Header("Rocket")]
    [Range(20f, 300f)]
    public int rocketFlightLength = 100;
    [Range(1f, 20f)]
    public int minRocketOffset = 50;
    [Range(1f, 20f)]
    public int maxRocketOffset = 10;

    [Header("Helmet")]
    [Range(20f, 10000f)]
    public int minHelmetDistance = 100;
    [Range(20f, 10000f)]
    public int maxHelmetDistance = 100;



    [Header("Temp Ints")]
    [SerializeField] private int obstacleDistance = 0;
    //[SerializeField] private int currentPlatformIndex = 0;
    [SerializeField] private int blankSpaceDistance = 0;
    [SerializeField] private int blankSpaceLength = 0;
    [SerializeField] private int chainDistance = 0;
    [SerializeField] private int chainLength = 0;
    [SerializeField] private int helmetDistance = 0;
    [SerializeField] private int rocketOffset = 0;
    [SerializeField] public int defaultRocketFlightLength = 0;
    [SerializeField] private int defaultSectionLength = 0;
    [SerializeField] private int sectionLength = 0;

    [Header("Temp Bools")]
    [SerializeField] private bool blankSpaceMode;
    [SerializeField] private bool chainMode;
    [SerializeField] private bool rocket = false;
    [SerializeField] public bool rocketMode;
    [SerializeField] private bool coinSection = false;

    private int GetIndex(bool[] array, bool element)
    {
        int index;

        for (index = 0; index < array.Length; index++)
            if (element == array[index]) break;

        return index;
    }

    public void ResetValues()
    {
        obstacleDistance = startingBuffer;

        helmetDistance = Random.Range(minHelmetDistance, maxHelmetDistance);
        blankSpaceDistance = Random.Range(minBlankSpaceDist + startingBuffer, maxBlankSpaceDist);
        chainDistance = Random.Range(minChainDistance + startingBuffer, maxChainDistance + startingBuffer);

        rocketMode = false;
        chainMode = false;
        blankSpaceMode = false;

        charMovement.rocketMode = false;

        RenderSettings.skybox = regularMaterial;
    }

    void Start()
    {
        defaultRocketFlightLength = rocketFlightLength;
        obstacleDistance = startingBuffer;

        blankSpaceDistance = Random.Range(minChainDistance + startingBuffer, maxBlankSpaceDist + startingBuffer);
        chainDistance = Random.Range(minChainDistance + startingBuffer, maxChainDistance + startingBuffer);
        helmetDistance = Random.Range(minHelmetDistance + startingBuffer, maxHelmetDistance + startingBuffer);
    }

    void UpdateDistances()
    {
        charMovement.rocketModeLength--;
        chainDistance--;
        chainLength--;
        blankSpaceDistance--;
        blankSpaceLength--;
        obstacleDistance--;
        helmetDistance--;
        rocketFlightLength--;
        sectionLength--;
    }

    void Clear2DArray(bool[,] platformLayout)
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 3; j++)
                platformLayout[i, j] = false;
        }
    }

    void StillRocketMode()
    {
        if (rocketFlightLength < 0)
        {
            rocketMode = false;
        }
    }

    void StartRocketMode()
    {
        rocketMode = true;
        scoreManager.rocketModeRequest = false;

        RenderSettings.skybox = rocketModeMaterial;

        rocketFlightLength = defaultRocketFlightLength;
        defaultSectionLength = rocketFlightLength / 30;

        blankSpaceDistance = rocketFlightLength;
        blankSpaceLength = 900;
    }

    void StartBlankSpaceMode()
    {
        blankSpaceMode = true;

        blankSpaceLength = Random.Range(minBlankSpaceLength, maxBlankSpaceLength);
        blankSpaceDistance = Random.Range(minBlankSpaceDist, maxBlankSpaceDist) + blankSpaceLength;

        rocket = randBool[Random.Range(0, 3)];

        if (rocket) rocketOffset = blankSpaceLength - Random.Range(minRocketOffset, maxRocketOffset);
    }

    void StartChainMode()
    {
        chainMode = true;

        bool doubleChain;
        float doubleChainChance = Random.Range(0f, 1f);
        int firstCoord = Random.Range(0, 3);

        doubleChain = doubleChainChance > 0.5f;
        chainLength = Random.Range(minChainLength, maxChainLength);
        chainDistance = Random.Range(minChainDistance, maxChainDistance) + chainLength;

        for (int i = 0; i < 3; i++)
            chainCoordinates[i] = false;

        chainCoordinates[firstCoord] = true;

        if (doubleChain)
        {
            int secondCoord = Random.Range(0, 3);
            chainCoordinates[secondCoord] = true;
        }
    }

    void PlaceHelmet(bool[,] platformLayout)
    {
        bool helmetPresence = Random.Range(0f, 1f) > 0.5f;
        int helmetPos = Random.Range(0, 3);

        for (int i = 0; i < 7; i++)
        {
            if (platformLayout[i, helmetPos] == true)
            {
                helmetPresence = false;
                break;
            }
        }

        if (helmetPresence)
        {
            platformLayout[5, helmetPos] = true;
        }

        helmetDistance = Random.Range(minHelmetDistance, maxHelmetDistance);
    }

    void SetBlank(bool[,] platformLayout)
    {
        if (blankSpaceLength > 0)
        {
            if (blankSpaceLength == rocketOffset && !rocketMode)
            {
                int rocketPosition = Random.Range(0, 3);
                platformLayout[4, rocketPosition] = true;
            }
        }
        else
        {
            rocketOffset = 0;
            blankSpaceMode = false;
        }
    }

    void SetChain(bool[,] platformLayout)
    {
        if (chainLength > 0)
        {
            if (chainLength % 2 == 0)
            {
                for (int i = 0; i < 3; i++)
                    platformLayout[1, i] = chainCoordinates[i];
            }
        }
        else
        {
            chainMode = false;
        }
    }


    void SetObstacle(bool[,] platformLayout)
    {
        bool[] obstacleOccupied = { false, false, false };
        int platformObstacleCount = 0;
        bool obstaclePresence;

        for (int i = 0; i < 3; i++)
        {
            if (platformObstacleCount < 2)
            {
                obstaclePresence = randBool[Random.Range(0, 2)];

                if (obstaclePresence)
                {
                    platformObstacleCount++;
                    obstacleOccupied[i] = true;
                }


                if (obstaclePresence && !chainMode)
                {
                    bool bomb = randBool[Random.Range(0, 6)];

                    if (!bomb) platformLayout[0, i] = obstaclePresence;
                    else platformLayout[2, i] = obstaclePresence;
                }
                else if (obstaclePresence && chainMode)
                {
                    int chainCoord = GetIndex(chainCoordinates, true);

                    if (i == chainCoord)
                    {
                        if (platformObstacleCount < 2)
                        {
                            platformLayout[2, i] = true;
                            platformLayout[1, i] = false;
                        }
                    }
                }
            }

            if (!obstacleOccupied[i])
            {
                bool coin = randBool[Random.Range(0, 3)];
                if (coin) platformLayout[1, i] = true;
            }
        }
        obstacleDistance = Random.Range(minObstacleDist, maxObstacleDist);
    }

    void SetDiamonds(bool [,] platformLayout)
    {
        Clear2DArray(platformLayout);
        for (int i = 0; i < 3; i++)
        {
            platformLayout[3, i] = true;
        }
    }

    void SetRocketCoinPool(bool[,] platformLayout)
    {
        if (sectionLength <= 0)
        {
            coinSection = !coinSection;
            sectionLength = defaultSectionLength;
        }

        if (sectionLength % 2 == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                platformLayout[1, i] = coinSection;
            }
        }
    }

    public bool[,] GetPlatformLayout()
    {
        bool[,] platformLayout;
        platformLayout = new bool[7, 3];

        UpdateDistances();
        Clear2DArray(platformLayout);

        StillRocketMode();

        if (!rocketMode)
        {
            if (scoreManager.rocketModeRequest) StartRocketMode();
            if (blankSpaceDistance == 0 && !blankSpaceMode) StartBlankSpaceMode();
            if (chainDistance <= 0 && !chainMode) StartChainMode();

            if (helmetDistance <= 0) PlaceHelmet(platformLayout);

            if (blankSpaceMode) SetBlank(platformLayout);
            if (chainMode) SetChain(platformLayout);
            if (!blankSpaceMode && obstacleDistance <= 0) SetObstacle(platformLayout);
            if (scoreManager.score % 1000 < 5 && scoreManager.score >= 1000) SetDiamonds(platformLayout);
        }
        else
        {
            SetRocketCoinPool(platformLayout);
        }

        return platformLayout;
    }
}
