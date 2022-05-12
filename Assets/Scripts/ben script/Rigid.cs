using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rigid : MonoBehaviour
{
    public UnityEvent interact;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        interact.Invoke();
    }
}