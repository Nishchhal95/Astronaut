using Astronaut;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private InputActionReference movementControl;
    [SerializeField] private InputActionReference jumpControl;
    [SerializeField] private InputActionReference gravityControl;
    [SerializeField] private Transform cameraMainTransform;
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private Animator myAnimator;

    private List<IPlayerController> playerControllers = new List<IPlayerController>();

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
        playerControllers.Add(new PlayerMovementController(this.transform, cameraMainTransform, 
            controller, movementControl, jumpControl, gravityControl, new PlayerSettings(2.0f, 1.0f, -9.81f, 4f)));
        //playerControllers.Add(new PlayerInteractionController(this.transform, cameraMainTransform, 
        //    1.5f, 90f, whatIsInteractable));
        //playerControllers.Add(new PlayerAnimationController(movementControl, jumpControl, myAnimator, controller));

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
        gravityControl.action.Enable();
        for (int i = 0; i < playerControllers.Count; i++)
        {
            playerControllers[i].OnEnable();
        }
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
        gravityControl.action.Disable();
        for (int i = 0; i < playerControllers.Count; i++)
        {
            playerControllers[i].OnDisable();
        }
    }

    void Update()
    {
        for (int i = 0; i < playerControllers.Count; i++)
        {
            playerControllers[i].Update();
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < playerControllers.Count; i++)
        {
            playerControllers[i].OnDrawGizmos();
        }
    }
}
