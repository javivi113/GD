using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();
            movement.Die();
        }
        catch { }
    }
}
