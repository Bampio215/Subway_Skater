using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    protected PlayerMotor motor;
    private void Awake()
    {
        motor = GetComponent<PlayerMotor>();
    }
    public virtual void Construct()
    {

    }
    public virtual void Destruct()
    {

    }
    public virtual void Transition()
    {

    }
    public virtual Vector3 ProcessMotion()
    {
        return Vector3.zero;
    }
}
