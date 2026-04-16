using UnityEngine;
using UnityEngine.InputSystem; // This tells Unity to use the NEW system!

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    
    // This variable will hold our new input controls
    private InputAction moveAction;

    void Awake()
    {
        // Set up the WASD and Arrow Key bindings right here in the code
        moveAction = new InputAction("Move", binding: "2DVector");
        moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");
    }

    // We must enable and disable the input action when the object turns on/off
    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Start()
    {
        // Grab the components attached to the Player
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Read the movement vector from our New Input System action
        movement = moveAction.ReadValue<Vector2>();

        // 2. Update the Animator
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // 3. Apply Physics Movement
        rb.linearVelocity = movement * moveSpeed;
    }
}