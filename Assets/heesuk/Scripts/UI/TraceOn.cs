using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent.GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            this.transform.parent.GetComponent<Pathfinding.AIDestinationSetter>().enabled = true;
        }

        else
        {
            this.transform.parent.GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;
        }
    }
}
