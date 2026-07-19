using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputTest : MonoBehaviour
{

    Vector2 moveValue;

    private void Start()
    {

    }

    void Update()
    {

    }

    public void OnMod1(InputAction.CallbackContext context)
    {
        switch(context.phase) {
            case InputActionPhase.Started:
                if (context.interaction is SlowTapInteraction)
                {
                    //Holfd
                    Debug.Log("Hold Started");
                }
                break;
            case InputActionPhase.Performed:
                if (context.interaction is SlowTapInteraction)
                {
                    Debug.Log($"context.duration {context.duration}");
                    Debug.Log("Hold Release");
                    //HoldRelease
                }
                else
                {
                    Debug.Log("Tap");
                    //Tap
                }
                break;
            case InputActionPhase.Canceled:
                //HoldRelease
                break;
            }
        }
}
