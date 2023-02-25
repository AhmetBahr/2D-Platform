using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrapt : MonoBehaviour
{
    [SerializeField] private float damage;


    [Header ("Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activationTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; // Ne zaman tetiklenecek
    private bool active; //Ne zaman sald�r�ya haz�r hale gelicek    

    private void Awake()
    {
        anim = GetComponent<Animator> ();
        spriteRend= GetComponent<SpriteRenderer> ();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if(active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }

    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRend.color= Color.red; //Aktif  
        
        
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color= Color.white; //Normal
        active= true;
        anim.SetBool("Activated", true);

       
        yield return new WaitForSeconds( activationTime);
        active= false;
        triggered= false;
        anim.SetBool("Activated", false);

    }

}
