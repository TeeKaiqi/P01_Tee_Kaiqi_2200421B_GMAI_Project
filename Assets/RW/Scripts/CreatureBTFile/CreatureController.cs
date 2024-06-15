using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public GameObject player; //reference to the player gameobject
    //public GameObject enemy;

    public float healingDistance = 3f; //minimum distace the player has to be from the creature to be healed
    public float tauntingDistance = 4f; //minimum distance the enemy has to be from the creature for the creature to taunt

    public float speed { get; private set; } = 3f; //I want the creature to be slower than the player

}
