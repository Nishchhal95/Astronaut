using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private ThirdPersonController thirdPersonController;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private bool jump = false;
    [SerializeField] private float currentTimeout = 0.2f;
    [SerializeField] private float timeOut = 0.2f;
    [SerializeField] private Shield shield;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private int _animIDIsMoving;
    private int _animIDCastSpell1;

    private void OnEnable()
    {
        StarterAssetsInputs.onCastSpell1Click += OnMakeShield;
    }

    private void OnDisable()
    {
        StarterAssetsInputs.onCastSpell1Click -= OnMakeShield;
    }

    private void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        m_Animator = GetComponentInChildren<Animator>();
        shield = GetComponentInChildren<Shield>();

        SetupAnimationIds();
    }

    private void SetupAnimationIds()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDIsMoving = Animator.StringToHash("IsMoving");
        _animIDCastSpell1 = Animator.StringToHash("CastSpell1");
    }

    private void Update()
    {
        if(currentTimeout > 0)
        {
            currentTimeout -= Time.deltaTime;
        }

        HandleInputAnimations();
    }

    private void HandleInputAnimations()
    {
        bool sprint = starterAssetsInputs.sprint;
        Vector2 velocity = starterAssetsInputs.move;
        bool isMoving = velocity.magnitude > 0;
        float speed = Mathf.Max(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y));

        m_Animator.SetBool(_animIDIsMoving, isMoving);

        if (isMoving)
        {
            if (sprint)
            {
                speed = speed.Remap(0f, 1f, 0.5f, 1f);
            }

            else
            {
                speed = Mathf.Clamp(speed, 0, 0.5f);
            }
        }

        else
        {
            speed = 0;
        }

        m_Animator.SetFloat(_animIDSpeed, speed);

        if (starterAssetsInputs.jump && !jump)
        {
            OnPlayerJumped();
        }

        if (thirdPersonController.Grounded && jump && currentTimeout <= 0)
        {
            Debug.Log("Back to ground");
            jump = false;
            m_Animator.SetBool(_animIDJump, jump);
        }
    }

    private void OnPlayerJumped()
    {
        currentTimeout = timeOut;
        Debug.Log("JUMP");
        jump = true;
        m_Animator.SetBool(_animIDJump, jump);
    }

    private void OnMakeShield()
    {
        if (shield.shieldOn)
        {
            return;
        }
        m_Animator.SetTrigger(_animIDCastSpell1);
    }
}
