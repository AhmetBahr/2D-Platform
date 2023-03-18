using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Arrowspawner : MonoBehaviour
{
    [Header("Cooldown")]
    [SerializeField] private float attackCooldown;
    private float cooldownTimer;

    [Header("Tranfom")]
    [SerializeField] private Transform firePoint;
    [Header("Arrows")]
    [SerializeField] private GameObject[] arrows;

    private void Attack()
    {
        cooldownTimer = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy) return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
            Attack();


    }
}
