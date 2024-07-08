using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayerInput : MonoBehaviour
{

    [SerializeField] private InputAction inputAction;

    void OnEnable()
    {
        inputAction.Enable();
    }

    void OnDisable()
    {
        inputAction.Disable();
    }

    void Update()
    {
        if(inputAction.WasPressedThisFrame())
        {
            PlayerEvents.onPlayerInputPressed.Invoke();
            return;
        }

        if(inputAction.WasReleasedThisFrame())
        {
            PlayerEvents.onPlayerInputReleased.Invoke();
            return;
        }

    }
}
