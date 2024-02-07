using core;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    public Rigidbody rigid;
    public CharacterInputAction actionMap;
    [SerializeField] private Vector2 directionValue;
    [SerializeField] public int forcePower;
    public GameState gameState;
    public Button startButton;
    public GameObject bombPrefab;
    public GameObject[] bombArray;
    public Button bombButton;
    public GameObject Floor;

    public bool bombButtonPressed = false;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.PreGame;
        actionMap = new CharacterInputAction();
        bombArray = new GameObject[1];
    }

    // Update is called once per frame
    void Update()
    {
        // Attach the method to the button click event
        startButton.onClick.AddListener(StartGame);
        directionValue = actionMap.Player.Movement.ReadValue<Vector2>();

        if (gameState == GameState.PreGame)
        {
            actionMap.Disable();
        }
        else if (gameState == GameState.Gameplay && count <= 2)
        {
            // Check for space key press to instantiate bomb
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bombButtonPressed = true;
                InstantiateBomb();
            }
            bombButton.onClick.AddListener(() => bombButtonPressed = true); InstantiateBomb();
            actionMap.Enable();
            // Check for space key press to instantiate bomb
        }
        else
        {
            bombButton.onClick.AddListener(() => bombButtonPressed = true);
        }
    }

    void StartGame()
    {
        gameState = GameState.Gameplay; 
        actionMap.Enable();
    }

    void InstantiateBomb()
    {
        if (bombButtonPressed)
        {
            count += 1;
            Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
            bombButtonPressed = false; // Reset the flag after instantiation
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
