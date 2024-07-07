using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayerInput : MonoBehaviour
{

    const float HELD_THRESH = 0.15f;

    bool _pushed = false;
    float _holdTimer = 0;

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
        if(_pushed && inputAction.IsPressed())
        {
            _holdTimer += Time.deltaTime;
            // Debug.Log("Hold timer: " + _holdTimer);
        }
        
        if(_holdTimer >= HELD_THRESH)
            PlayerEvents.onPlayerStop.Invoke();
        
        if (inputAction.WasReleasedThisFrame() && _holdTimer >= HELD_THRESH)
        {
            PlayerEvents.onPlayerMove.Invoke();
            _holdTimer = 0;
            _pushed = false;
            return;
        }

        if (_pushed && (!inputAction.IsPressed() || inputAction.WasReleasedThisFrame()))
        {
            _holdTimer = 0;
            _pushed = false;
            PlayerEvents.onPlayerSwitchDirection.Invoke();
            return;
        }

        if(inputAction.WasPressedThisFrame())
            _pushed = true;
    }
}
