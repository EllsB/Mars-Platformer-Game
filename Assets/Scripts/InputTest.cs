using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{

    Vector2 moveValue;

    private void Start()
    {

    }

    void Update()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveValue}");
    }
}
