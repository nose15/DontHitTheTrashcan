using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public enum PlatformType
    {
        regular,
        space
    }

    [SerializeField] GameObject regularPlatformPrefab;
    [SerializeField] Transform gameLevel;

    public GameObject CreateNewPlatform(int positionOffset, float platformSize, bool[,] platformLayout, PlatformType platformType)
    {
        GameObject platform;
        PlatformBehaviour platformData;

        Vector3 pos = new Vector3(0, 0, positionOffset * platformSize * regularPlatformPrefab.transform.localScale.z);
        Quaternion rot = new Quaternion(0, 0, 0, 1);

        platform = Instantiate(regularPlatformPrefab, pos, rot, gameLevel);
        platformData = platform.GetComponent<PlatformBehaviour>();
        //platformData.platformLayout = platformLayout;

        platform.transform.GetChild(0).gameObject.SetActive(false);

        return platform;
    }
}
