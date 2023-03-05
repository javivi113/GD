using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChangeSpeed : MonoBehaviour
{
    public Speeds Speed;
    void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();

            movement.ChangeSpeed(Speed);
        }
        catch { }
    }
}
