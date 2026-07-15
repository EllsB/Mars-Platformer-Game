using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class AimingReticle : MonoBehaviour
{
    [field: SerializeField, Range(1f, 3f)] public float rangeMult { get; protected set; } = 1.5f;
    private Vector2 aimInput;

    public void onAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
        aimInput = aimInput * rangeMult;
        Debug.Log($"Move Input: {aimInput}");
        this.gameObject.transform.localPosition = aimInput;
    }
}
