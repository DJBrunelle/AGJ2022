using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeController : MonoBehaviour
{
    public delegate void DudeCollision(string objectTag);
    public event DudeCollision OnDudeCollision;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        OnDudeCollision?.Invoke(col.gameObject.tag);
    }
}
