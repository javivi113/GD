using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChangeGameMode : MonoBehaviour
{
    public GameModes gameModes;
    void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();
            movement.ChangeModePortal(gameModes, 0, 0, 1);
        }
        catch { }
    }
}
