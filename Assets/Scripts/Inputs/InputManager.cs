using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InputManager : MonoBehaviour
{
    // There should be only one InputManager in the scene
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    // Action schemes
    private RunnerInputAction actionScheme;

    // Configuration
    [SerializeField] private float sqrSwipeDeadzone = 50.0f;

    #region public properties
    public bool Tap { get { return tap; } }
    public Vector2 TouchPosition { get { return touchPosition; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    #endregion

    #region privates
    private bool tap;
    private Vector2 touchPosition;
    private Vector2 startDrag;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;
    #endregion

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SetupControl();
    }
    private void LateUpdate()
    {
        ResetInputs();
    }
    private void ResetInputs()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
    }

    private void SetupControl()
    {
        actionScheme = new RunnerInputAction();

        // Register different actions
        actionScheme.Gameplay.Tap.performed += ctx => OnTap(ctx);
        actionScheme.Gameplay.TouchPosition.performed += ctx => OnPosition(ctx);
        actionScheme.Gameplay.StartDag.performed += ctx => OnStartDrag(ctx);
        actionScheme.Gameplay.EndDag.performed += ctx => OnEndDrag(ctx);
    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        tap = true;
        Debug.Log("Tap detected at: " + touchPosition);
    }

    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        startDrag = touchPosition;
        Debug.Log("Start Drag at: " + startDrag);
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {
        Vector2 delta = touchPosition - startDrag;
        float sqrDistance = delta.sqrMagnitude;
        Debug.Log("End Drag at: " + touchPosition + " | Delta: " + delta);

        if (sqrDistance > sqrSwipeDeadzone)
        {
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y);

            if (x > y)
            {
                if (delta.x > 0)
                {
                    swipeRight = true;
                    Debug.Log("Swipe Right detected");
                }
                else
                {
                    swipeLeft = true;
                    Debug.Log("Swipe Left detected");
                }
            }
            else
            {
                if (delta.y > 0)
                {
                    swipeUp = true;
                    Debug.Log("Swipe Up detected");
                }
                else
                {
                    swipeDown = true;
                    Debug.Log("Swipe Down detected");
                }
            }
        }

        startDrag = Vector2.zero;
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        touchPosition = ctx.ReadValue<Vector2>();
    }


    public void OnEnable()
    {
        actionScheme.Enable();
    }
    public void OnDisable()
    {
        actionScheme.Disable();
    }
}

/*
 *          DYNAMIC UPDATE
 *          InputManager that processes the inputs
 *          PlayerMotor uses these inputs to move
 *          
 *          LATE UPDATE
 *          InputManager resets these inputs
 */
