using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State Varibles
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpstate jumpState { get; private set; }
    public PlayerAirState InAirState { get; private set; }    
    public PlayerLandState LandState { get; private set; }  


    [SerializeField] private PlayerData playerData;


    #endregion

    #region Components

    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }

    #endregion

    #region Check Transform

    [SerializeField] private Transform groundCheck;

    #endregion

    #region Others
    public Vector2 CurrentVelocity { get; private set; }
    public int FaceingDirection { get; private set; }

    private Vector2 workspace;



    #endregion

    #region Calls Function
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        jumpState = new PlayerJumpstate(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");


    }

    private void Start()
    {
        Anim = GetComponent<Animator>();

        InputHandler = GetComponent<PlayerInputHandler>();

        RB = GetComponent<Rigidbody2D>();

        FaceingDirection = 1;

        StateMachine.Initialize(IdleState);


    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();

    }

    #endregion

    #region Set Function
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;

    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Check Function


    public void CheckIfShouldFlip(int xinput)
    {
        if (xinput != 0 && xinput != FaceingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGround()
    {

        return Physics2D.OverlapCircle(groundCheck.position, playerData.GroundCheckRadius, playerData.WhatIsGround);

        
    }

    #endregion

    #region Other Function
    private void Flip()
    {
        FaceingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void AnimatonTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimatonFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #endregion
}
