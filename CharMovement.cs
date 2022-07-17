using UnityEngine;

public class CharMovement : MonoBehaviour
{
    [SerializeField] Material regularMaterial;
    [SerializeField] PlatformMapGen platformMapGen;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] StateManager stateManager;
    [SerializeField] Transform character;

    public float forwardSpeed = 0.5f;
    public float sidewaysMovMultiplier;
    public float sidewaysRotMultiplier;

    readonly private float lerpSpeed = 0.5f;
    private float defaultForwardSpeed;
    
    private Vector3 newHolderPos;
    private Vector3 newModelLocalPos;
    private Quaternion newModelLocalRot;

    [SerializeField] public int rocketModeLength;

    public bool rocketMode = false;

    void Start()
    {
        stateManager.SetState(StateManager.State.Home);
        defaultForwardSpeed = forwardSpeed;
    }

    private void Update()
    {
        if (scoreManager.rocketModeRequest && !rocketMode)
        {
            rocketModeLength = platformMapGen.defaultRocketFlightLength + 100;

            rocketMode = true;
        }

        if (rocketModeLength == 0)
        {
            rocketMode = false;
            RenderSettings.skybox = regularMaterial;
        }
    }


    void FixedUpdate()
    {
        if (stateManager.gameState == StateManager.State.Play)
        {
            float xPos = Mathf.Clamp(inputManager.screenLongitudeRatio * sidewaysMovMultiplier, -3, 3);

            newModelLocalPos = new Vector3(xPos, transform.position.y, transform.position.z);
            newModelLocalRot = new Quaternion(0.8f, inputManager.screenLongitudeRatio * sidewaysRotMultiplier, inputManager.screenLongitudeRatio * sidewaysRotMultiplier, 1);

            if (!rocketMode) newHolderPos = new Vector3(transform.position.x, 0.75f, transform.position.z);
            else newHolderPos = new Vector3(transform.position.x, 25.75f, transform.position.z);
        }
        else
        {
            newHolderPos = new Vector3(0, 1f, 0);
            newModelLocalPos = new Vector3(0, 1f, 0);
            newModelLocalRot = new Quaternion(0, 0, 0, 1);

            forwardSpeed = defaultForwardSpeed;

            if (inputManager.startRequest)
            {
                stateManager.SetState(StateManager.State.Play);
                inputManager.startRequest = false;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newHolderPos, lerpSpeed * 5 * Time.deltaTime);
        character.SetPositionAndRotation(Vector3.Lerp(character.transform.position, newModelLocalPos, lerpSpeed), newModelLocalRot);
    }
}
