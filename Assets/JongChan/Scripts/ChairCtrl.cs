﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairCtrl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }
}
