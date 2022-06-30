using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public struct GameArea
    {
        public GameArea(int bottomYBound, int topYBound)
        {
            this.bottomYBound = bottomYBound;
            this.topYBound = topYBound;
        }

        public int bottomYBound;
        public int topYBound;
    }

    [SerializeField] StateManager stateManager;
    [SerializeField] ZoneCollectorManager zoneCollectorManager;

    public bool left;
    public bool right;
    public bool zoneRequest;
    public bool startRequest;

    public float screenLongitudeRatio;

    System.DateTime time1;
    System.DateTime time2;
    System.TimeSpan doubleClickTime;

    GameArea ControllArea = new GameArea(310, 1350);
    GameArea SpecialButtonArea = new GameArea(0, 310);
    GameArea UIArea = new GameArea(1350, Screen.height);

    bool touchInGameArea(float touchPositionY, GameArea area)
    {
        if (area.bottomYBound < touchPositionY && touchPositionY < area.topYBound) return true;
        else return false;
    }

    void Start()
    {
        time1 = System.DateTime.Now;
        doubleClickTime = new System.TimeSpan(0, 0, 0, 0, 300);

        left = false;
        right = false;
        startRequest = false;
    }

    void Update()
    {
        if (stateManager.gameState == StateManager.State.Play)
        {
            if (Input.touchCount != 0)
            {
                time2 = System.DateTime.Now;
                Touch touch = Input.GetTouch(0);

                if (touchInGameArea(touch.position.y, ControllArea))
                {
                        screenLongitudeRatio = (touch.position.x - Screen.width / 2) / Screen.width;
                }

                else if (touchInGameArea(touch.position.y, SpecialButtonArea))
                {
                    if (time2 - time1 < doubleClickTime && !zoneCollectorManager.zoneMode)
                    {
                        zoneRequest = true;
                    }
                }

                time1 = System.DateTime.Now;
            }
            else
            {
                screenLongitudeRatio = 0;
            }
        }
        else if (stateManager.gameState == StateManager.State.Home)
        {
            if (Input.touchCount != 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touchInGameArea(touch.position.y, ControllArea)) startRequest = true;
            }
        }
    }
}
