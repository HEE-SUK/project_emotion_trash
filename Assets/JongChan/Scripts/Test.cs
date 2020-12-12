using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Pathfinding.AIDestinationSetter>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gameObject.GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;
        }
    }
}
