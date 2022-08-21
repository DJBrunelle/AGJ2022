using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour
{
    public delegate void ThrowOut(string binTag);
    public event ThrowOut OnThrowOut;

    private void OnCollisionEnter2D(Collision2D col)
    {
        OnThrowOut?.Invoke(tag);
    }
}
