using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Script for moving the player.
/// Adapted from video https://www.youtube.com/watch?v=qEtLamo_-_g
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Variables

    #region Movement Modifiers
    [field: SerializeField, Range(0f, 10f)] public float speed { get; protected set; } = 5f;
    [field: SerializeField, Range(0f,50f)] public float jumphight { get; protected set; } = 2f;
    [field: SerializeField, Range(0f, 20f)] public float gravity { get; protected set; } = 10f;
    [field: SerializeField, Range(0f, 30f)] public float maxVelocity { get; protected set; } = 20f;
    #endregion

    public bool grounded;

    private CharacterController controller;
    protected Rigidbody rb;
    private Vector2 moveInput;
    //private Vector3 velocity;
    #endregion

    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Vector3 move = new Vector3(moveInput.x, 0, 0);
        //controller.Move(move * speed *  Time.deltaTime);
        //velocity.y += -gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //Find target velocity
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = new Vector3(moveInput.x, 0);
        targetVelocity *= speed;

        //Align direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate forces
        Vector3 velocityCharge = (targetVelocity - currentVelocity);
        velocityCharge = new Vector3(velocityCharge.x, 0);

        //Limit Force
        Vector3.ClampMagnitude(velocityCharge, maxVelocity);

        rb.AddForce(velocityCharge, ForceMode.VelocityChange);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"Move Input: {moveInput}");
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector3 jumpForce = Vector3.zero;
        if(context.started &&  grounded)
        {
            Debug.Log("Jump");
            jumpForce = Vector3.up * jumphight;
        }
        else if (context.started && !grounded)
        {
            Debug.Log("Not Grounded");
        }
        rb.AddForce(jumpForce, ForceMode.VelocityChange);
    }
}
