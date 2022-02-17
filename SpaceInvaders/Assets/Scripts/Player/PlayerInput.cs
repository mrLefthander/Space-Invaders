using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput: MonoBehaviour, IInputProvider
{
  public float Horizontal => _horizontal;
  public event Action FireEvent;

  private PlayerInputActions _inputActions;
  private float _horizontal = 0f;

  private void Awake()
  {
    _inputActions = new PlayerInputActions();
    _inputActions.Player.Enable();
  }

  private void OnEnable()
  {
    _inputActions.Player.Move.performed += OnMoveInput;
    _inputActions.Player.Move.canceled += OnMoveInput;
    _inputActions.Player.Fire.performed += OnFireInput;
  }

  private void OnDisable()
  {
    _inputActions.Player.Move.performed -= OnMoveInput;
    _inputActions.Player.Move.canceled -= OnMoveInput;
    _inputActions.Player.Fire.performed -= OnFireInput;
  }

  private void OnMoveInput(InputAction.CallbackContext context) => 
    _horizontal = context.ReadValue<float>();

  private void OnFireInput(InputAction.CallbackContext context) => 
    FireEvent?.Invoke();
}
