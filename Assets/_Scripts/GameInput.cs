using System;
using UnityEngine;

public class GameInput : Utility.SingletonMonoBehaviour<GameInput>
{
    // Events Declaration
    // public event EventHandler<OnPlayerMoveEventArgs> OnPlayerMove;
    // public class OnPlayerMoveEventArgs : EventArgs
    // {
    //     public Vector2 MoveVector { get; set; }
    // }
    
    
    private InputSystem_Actions _inputActions;

    protected override void Awake()
    {
        base.Awake();
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Enable();
    }

    private void Start()
    {
        // _inputActions.Player.Move.performed += PlayerMove_Performed;
    }

    private void OnDestroy()
    {
        // _inputActions.Player.Move.performed -= PlayerMove_Performed;
    }

    // private void PlayerMove_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    // {
    //     // if (IsInputLocked())
    //     // {
    //     //     return;
    //     // }
    //     Debug.Log("MovePerformed");
    //     OnPlayerMove?.Invoke(this, new OnPlayerMoveEventArgs { MoveVector = GetPlayerMoveVector() });
    // }

    public Vector2 GetPlayerMoveVector()
    {
        // if (IsInputLocked())
        // {
        //     return Vector2.zero;
        // }
        return _inputActions.Player.Move.ReadValue<Vector2>();
    }
}
