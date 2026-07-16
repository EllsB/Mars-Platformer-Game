using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class AimingReticle : MonoBehaviour
{
    [field: SerializeField] public bool locked { get; protected set; } = false;
    [field: SerializeField, Range(1f, 3f)] public float rangeMult { get; protected set; } = 1.5f;
    private Vector2 aimInput;

    public void onAim(InputAction.CallbackContext context)
    {
        if (!locked)
        {
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
            aimInput = context.ReadValue<Vector2>();
            aimInput = aimInput * rangeMult;
            Debug.Log($"Move Input: {aimInput}");
            this.gameObject.transform.localPosition = aimInput;
        }
        if (aimInput.x == 0 && aimInput.y == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void onLock(InputAction.CallbackContext context)
    {
        if (!locked && context.ReadValue<bool>())
        {
            locked = true;
        }
        else if (locked && context.ReadValue<bool>())
        {
            locked = false;
        }
    }
}
