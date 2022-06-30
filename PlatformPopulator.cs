using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPopulator : MonoBehaviour
{
    public PlatformMapGen platformMapGen;

    // Update is called once per frame
    public void Populate(GameObject platform, bool rocketMode)
    {
        bool[,] layout;
        layout = platformMapGen.GetPlatformLayout();

        if (rocketMode) platform.transform.GetChild(0).gameObject.SetActive(false);
        else platform.transform.GetChild(0).gameObject.SetActive(true);

        for (int i = 0; i < 7; i++)
        {
            Transform platformSection = platform.transform.GetChild(i + 1);

            for (int j = 0; j < 3; j++)
            {
                platformSection.GetChild(j).gameObject.SetActive(layout[i, j]);
            }
        }
    }
}
