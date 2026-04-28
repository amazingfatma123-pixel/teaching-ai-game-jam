using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]private float MaxSpeed = 1; // in m/s
    [SerializeField]private float clampLength = 5; // in meters

    private Rigidbody2D playerRigidbody;
    private PlayerInput playerInput;
    private InputAction pressAction;
    private InputAction doubleTapAction;

    private void Awake(){
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        pressAction = playerInput.actions["press"];
        doubleTapAction = playerInput.actions["doubleTap"];
    }
    private void OnEnable(){
        pressAction.performed += Move;
        doubleTapAction.performed += Attack;
    }
    private void OnDisable(){
        pressAction.performed -= Move;
        doubleTapAction.performed -= Attack;
    }

    private void Move(InputAction.CallbackContext context)
    {
        var touch = context.ReadValue<TouchState>();
        //Debug.Log("move");
        Vector2 trackedPosition = Camera.main.ScreenToWorldPoint( touch.position ); // in world space
        playerRigidbody.AddForce(_getForce(trackedPosition));
    }
    
    void Attack(InputAction.CallbackContext context)
    {  
        var touch = context.ReadValue<TouchState>();
        Debug.Log("Attack");
        Vector2 trackedPosition = Camera.main.ScreenToWorldPoint( touch.position ); // in world space
        playerRigidbody.AddForce(_getForce(trackedPosition), ForceMode2D.Impulse);
    }
    Vector2 _getForce(Vector2 trackedPosition)
    {
        Vector2 currentPosition = new Vector2(this.transform.position.x,this.transform.position.y);
        Vector2 difference = trackedPosition - currentPosition;
        Vector2 direction = difference.normalized;
        float forceMagnitude = MaxSpeed * Math.Clamp(value:difference.magnitude, min:0, max:clampLength) / clampLength;
        return forceMagnitude*direction;
    }
    /*
    private void CheckTap(TouchState touch)
    {
        if (!touch.isTap)
            return;

        if (_lastTapTime > 0f)
        {
            _tapCount++;
        } else {
            _tapCount = 1;
        }

        //Debug.Log($"Tap detected. Tap count: {_tapCount}");
        _lastTapTime = TAP_THRESHOLD;
    }*/

}
