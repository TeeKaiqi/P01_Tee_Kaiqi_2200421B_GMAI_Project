using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public GameObject player; //reference to the player gameobject
    //public GameObject enemy;

    public float healingDistance = 2f; //minimum distace the player has to be from the creature to be healed
    public float tauntingDistance = 3f; //minimum distance the enemy has to be from the creature for the creature to taunt

    public float speed { get; private set; } = 2f; //I want the creature to be slower than the player

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
