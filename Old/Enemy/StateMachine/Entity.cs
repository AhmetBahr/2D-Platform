using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Entity : MonoBehaviour
{
    public FinisStateMachine stateMachine;

    public D_Entity entityData;

    public int facingDirection { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    
    public GameObject aliveGo { get; private set; }

    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    

    private Vector2 velocityWorkspace;
    protected bool isStunned;
    protected bool isDead;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;


    public virtual void Start()
    {
        facingDirection= 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        aliveGo = transform.Find("Alive").gameObject;
        rb = aliveGo.GetComponent<Rigidbody2D>();
        anim = aliveGo.GetComponent<Animator>();
        atsm = aliveGo.GetComponent <AnimationToStatemachine>();

        stateMachine = new FinisStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        if(Time.time >= lastDamageTime + entityData.stunRecovertTime)
        {
            ResetStunResistance();
        }

    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velecity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x  * velecity * direction, angle.y * velecity); 
        rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {

        return Physics2D.Raycast(wallCheck.position, aliveGo.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {

        return Physics2D.Raycast(ledgeCheck.position,Vector2.down,entityData.ledgeCheckDistance,entityData.whatIsGround );

    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(ledgeCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);

    }

    public virtual bool ChechPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGo.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool ChechPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGo.transform.right, entityData.maxAgeroDistance, entityData.whatIsPlayer);


    }

    public virtual bool CheckPlayerInCloseAction() 
    {
        return Physics2D.Raycast(playerCheck.position, aliveGo.transform.right, entityData.performanCloseRangeAction, entityData.whatIsPlayer);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGo.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void DamgeHop(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;

    }

    public virtual void ResetStunResistance()
    {
        isStunned= false;
        currentStunResistance = entityData.stunResistance;

    }

    public virtual void Damage(AttacksDeatls attackDetails)
    {
        lastDamageTime = Time.time;
        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.damageAmount;    

        DamgeHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, aliveGo.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if(attackDetails.positon.x > aliveGo.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if(currentStunResistance <= 0)
        {
            isStunned= true;
        }

        if(currentHealth <= 0)
        {
            isDead= true;
        }

    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.performanCloseRangeAction), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgeroDistance), 0.2f);


    }




}
    