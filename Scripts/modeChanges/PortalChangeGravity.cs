using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChangeGravity : MonoBehaviour
{
    public bool Gravity;
    void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();

            movement.ChangeModePortal(0, 0, Gravity ? 1 : -1, 2);
        }
        catch { }
    }
}
