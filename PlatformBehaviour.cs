using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    [SerializeField] MapManager mapManager;
    [SerializeField] StateManager stateManager;

    public float speed;


    private void Start()
    {
        mapManager = GameObject.Find("GameLevel").GetComponent<MapManager>();
        stateManager = GameObject.Find("GameLevel").GetComponent<StateManager>();

        speed = mapManager.speed;
    }

    void FixedUpdate()
    {
        speed = mapManager.speed;

        if (stateManager.gameState == StateManager.State.Play)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z - speed);
        }
    }
}
