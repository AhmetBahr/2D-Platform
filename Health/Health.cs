using playerCont;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth {  get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Efect")]
    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOffFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private GameManager GM;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim= GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            //Player hurt
            anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
           if(!dead)
            {
                // player dead

               
                foreach(Behaviour component in components)
                    component.enabled= false;

                anim.SetBool("Grounded" , true);
                anim.SetTrigger("Die");

                dead = true;
            }


        }


    }
    
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }
    */

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);

    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOffFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable= false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void Respawn()
    {
           AddHealth(startingHealth);
           anim.ResetTrigger("Die");
           anim.Play("Idle");
           StartCoroutine(Invunerability());

           //Activate all attached component classes
           foreach (Behaviour component in components)
               component.enabled = true;
        

        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
      //  GM.Respawn();
        Destroy(gameObject);

    }


}
