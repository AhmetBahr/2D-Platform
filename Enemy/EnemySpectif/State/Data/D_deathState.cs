using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/Entity Data/Dead State")]
public class D_deathState : ScriptableObject
{
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;

}
