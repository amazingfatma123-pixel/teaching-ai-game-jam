using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]private float MaxSpeed = 1; // in m/s
    [SerializeField]private float ImpulseMultiplier = 0.5f; // in m/s

    [SerializeField]private float clampLength = 5; // in meters

    public event Action AttackEvent;
    public event Action<Vector2> MoveEvent;

    private Rigidbody2D playerRigidbody;
    private PlayerInput playerInput;
    private InputAction pressAction;
    private InputAction doubleTapAction;
    private InputAction tapAction;
    private InputAction tapPositionAction;

    Coroutine moveRoutine;
    

    private void Awake(){
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        //pressAction = playerInput.actions["press"];
        doubleTapAction = playerInput.actions["doubleTap"];
        tapAction = playerInput.actions["tap"];
        tapPositionAction = playerInput.actions["tapPosition"];
    }
    private void OnEnable(){
        //pressAction.performed += Move;

        doubleTapAction.performed += Attack;
        tapAction.started += StartMove;
        tapAction.canceled += EndMove;
    }
    private void OnDisable(){
        //pressAction.performed -= Move;

        doubleTapAction.performed -= Attack;
        tapAction.started -= StartMove;
        tapAction.canceled -= EndMove;
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        //Debug.Log("tap");
        moveRoutine = StartCoroutine("Move");
        //MoveEvent(_getForce(_getTrackedPosition()).normalized);
    }
    IEnumerator Move()
    {
        //Debug.Log("Start Move");
        while (tapAction.IsInProgress())
        {
            playerRigidbody.AddForce(_getForce(_getTrackedPosition()));
            yield return new WaitForFixedUpdate();
        }
        //Debug.Log("End Move");
    }

    private void EndMove(InputAction.CallbackContext context)
    {
        StopCoroutine(moveRoutine);
        //Debug.Log("canceled Move");
        //MoveEvent(Vector2.zero);
    }
    
    void Attack(InputAction.CallbackContext context)
    {  
        //AttackEvent();
        Debug.Log("Attack");
        playerRigidbody.AddForce(ImpulseMultiplier*_getForce(_getTrackedPosition()), ForceMode2D.Impulse);
    }
    
    Vector2 _getTrackedPosition()
    {
        Vector2 screenPosition = tapPositionAction.ReadValue<Vector2>();
        Vector2 trackedPosition = Camera.main.ScreenToWorldPoint( screenPosition ); // in world space
        return trackedPosition;
    }
    
    Vector2 _getForce(Vector2 trackedPosition)
    {
        Vector2 currentPosition = new Vector2(this.transform.position.x,this.transform.position.y);
        Vector2 difference = trackedPosition - currentPosition;
        Vector2 direction = difference.normalized;
        float forceMagnitude = MaxSpeed * Math.Clamp(value:difference.magnitude, min:0, max:clampLength) / clampLength;
        return forceMagnitude*direction;
    }
}