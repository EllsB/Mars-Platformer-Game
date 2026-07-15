using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script for moving the player.
/// Adapted from video https://www.youtube.com/watch?v=qEtLamo_-_g
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Variables

    #region Movement Modifiers
    [field: SerializeField, Range(0f, 10f)] public float speed { get; protected set; } = 5f;
    [field: SerializeField, Range(0f,4f)] public float jumphight { get; protected set; } = 2f;
    [field: SerializeField, Range(0f, 20f)] public float gravity { get; protected set; } = 10f;
    #endregion

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;
    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        Vector3 move = new Vector3(moveInput.x, 0, 0);
        controller.Move(move * speed *  Time.deltaTime);
        velocity.y += -gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed &&  controller.isGrounded)
        {
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt( jumphight * -2f * -gravity);
        }
        else if (context.performed && !controller.isGrounded)
        {
            Debug.Log("Not Grounded");
        }
    }
}
