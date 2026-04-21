using UnityEngine;
using NewerInput;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(menuName = "Scriptable Objects/ Input Reader")]
public class InputReader : ScriptableObject, CustomInput.IPlayerActions, CustomInput.IDetectiveActions
{
    public CustomInput customInput;

    private void OnEnable()
    {
        if (customInput == null)
        {
            customInput = new CustomInput();
            customInput.Player.SetCallbacks(this);
            customInput.Detective.SetCallbacks(this);
        }

        SetGameplay();
    }

    private void OnDisable()
    {
        DisableAll();
    }

    //platforming controls
    public void SetGameplay()
    {
        DisableAll();
        customInput.Player.Enable();
    }

    //detective controls
    public void SetDetective()
    {
        DisableAll();
        customInput.Detective.Enable();
    }

    public void DisableAll()
    {
        customInput.Player.Disable();
        customInput.Detective.Disable();
    }

    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action DashEvent;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            DashEvent?.Invoke();
        }
    }
}
