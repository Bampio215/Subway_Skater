using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenlanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSidewaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalvelocity = 20.0f;
    public CharacterController controller;
    private BaseState state;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        state = GetComponent<RunningState>();
        state.Construct();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMotor();
    }
    private void UpdateMotor()
    {
        //Check if we're grounded
        isGrounded = controller.isGrounded;

        //How should we be moving right now? based on state
        moveVector = state.ProcessMotion();

        //Are we trying to chance state?
        state.Transition();

        //Move the player
        controller.Move(moveVector * Time.deltaTime);
    }
    public float SnapToLane()
    {

        float r = 0.0f;
        if (transform.position.x != (currentLane * distanceInBetweenlanes))
        {
            float deltaToDesiredPosition = (currentLane * distanceInBetweenlanes) - transform.position.x;
            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= baseSidewaySpeed;

            float actualDistance = r * Time.deltaTime;
            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesiredPosition))
            {
                r = deltaToDesiredPosition * (1 / Time.deltaTime);
            }
        }
        else
        {

        }
        return r;
    }
    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }
    public void ChangeState(BaseState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }
    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -terminalvelocity)
        {
            verticalVelocity = -terminalvelocity;
        }
    }
}
