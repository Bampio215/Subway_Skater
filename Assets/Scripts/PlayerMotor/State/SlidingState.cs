
using UnityEngine;

public class SlipingState : BaseState
{
    public float slideDuration = 1.0f;

    //Collider logic 
    private Vector3 initialCenter;
    private float initalSize;

    private float slideStart;

    public override void Construct()
    {
        slideStart = Time.time;

        initalSize = motor.controller.height;
        initialCenter = motor.controller.center;

        motor.controller.height = initalSize * 0.5f;
        motor.controller.center = initialCenter * 0.5f;
    }

    public override void Destruct()
    {
        motor.controller.height = initalSize;
        motor.controller.center = initialCenter;
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }
        if (!motor.isGrounded)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
        if (InputManager.Instance.SwipeUp)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
    }
}
