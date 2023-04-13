using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPlayerData" , menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move")]
    public float movementVelocity = 10f;

    [Header("Jump")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("Checks")]
    public float GroundCheckRadius = 0.3f;



    [Header("What is")]
    public LayerMask WhatIsGround;

}
