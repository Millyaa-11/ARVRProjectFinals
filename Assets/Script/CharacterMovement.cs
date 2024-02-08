using core;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody rigid;
    public PlayerInputActions actionMap;
    [SerializeField] private Vector2 directionValue;
    [SerializeField] public int forcePower;
    public GameState gameState;

    private PlayerInputActions inputActions;

    void Start()
    {
        gameState = GameState.Scanning;
        actionMap = new PlayerInputActions();
    }

    void Update()
    {
        // Attach the method to the button click event
        directionValue = actionMap.Player.MovementXY.ReadValue<Vector2>();

        if (gameState == GameState.Scanning)
        {
            actionMap.Disable();
        }
        else if (gameState == GameState.Gameplay)
        {
            // Check for space key press to instantiate bomb
            if (Input.GetKeyDown(KeyCode.Space))
            {
                actionMap.Enable();
            }
        }
    }
    private void FixedUpdate()
    {
        ApplyForce();
    }
    void ApplyForce()
    {
        Vector3 applyingForce = (directionValue * forcePower);
        rigid.AddForce(applyingForce, ForceMode.VelocityChange);
    }
}
