using Astronaut;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerMovementController : IPlayerController
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField] private InputActionReference movementControl;
    [SerializeField] private InputActionReference jumpControl;
    [SerializeField] private InputActionReference gravityToggle;

    [SerializeField] private PlayerSettings playerSettings;

    [SerializeField] private Transform cameraMainTransform;
    [SerializeField] private Transform m_transform;

    private float originalGravity;
    private float originalJumpHeight;

    private int gravityToggleCount;

    public static Action onPlayerJumped;

    public PlayerMovementController(Transform transform, Transform _cameraMainTransform, 
        CharacterController characterController, InputActionReference _movementControl, 
        InputActionReference _jumpControl, InputActionReference _gravityToggle, PlayerSettings _playerSettings)
    {
        m_transform = transform;
        controller = characterController;
        movementControl = _movementControl;
        jumpControl = _jumpControl;
        gravityToggle = _gravityToggle;
        cameraMainTransform = _cameraMainTransform;
        playerSettings = _playerSettings;
        originalGravity = playerSettings.GravityValue;
        originalJumpHeight = playerSettings.JumpHeight;
    }

    public void OnDrawGizmos()
    {
        
    }

    public void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSettings.PlayerSpeed);

        if (gravityToggle.action.triggered)
        {
            gravityToggleCount++;
            if(gravityToggleCount % 2 == 0)
            {
                playerSettings = new PlayerSettings(playerSettings.PlayerSpeed, originalJumpHeight, originalGravity, playerSettings.RotationSpeed);
            }
            else
            {
                playerSettings = new PlayerSettings(playerSettings.PlayerSpeed, 3f, originalGravity / 3, playerSettings.RotationSpeed);
            }
        }

        // Changes the height position of the player..
        if (jumpControl.action.triggered && groundedPlayer)
        {
            onPlayerJumped?.Invoke();
            playerVelocity.y += Mathf.Sqrt(playerSettings.JumpHeight * -3.0f * playerSettings.GravityValue);
        }

        playerVelocity.y += playerSettings.GravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            m_transform.rotation = Quaternion.Lerp(m_transform.rotation, rotation, Time.deltaTime * playerSettings.RotationSpeed);
        }
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }
}

namespace Astronaut
{
    [System.Serializable]
    public class PlayerSettings
    {
        private float playerSpeed = 2.0f;
        private float jumpHeight = 1.0f;
        private float gravityValue = -9.81f;
        private float rotationSpeed = 4f;

        public float PlayerSpeed { get => playerSpeed; }
        public float JumpHeight { get => jumpHeight; }
        public float GravityValue { get => gravityValue; }
        public float RotationSpeed { get => rotationSpeed; }

        public PlayerSettings(float playerSpeed, float jumpHeight, float gravityValue, float rotationSpeed)
        {
            this.playerSpeed = playerSpeed;
            this.jumpHeight = jumpHeight;
            this.gravityValue = gravityValue;
            this.rotationSpeed = rotationSpeed;
        }
    }
}

