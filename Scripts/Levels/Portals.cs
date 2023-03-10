using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public GameModes GameMode;
    public Speeds Speed;
    public bool Gravity;
    public int State;

    void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();

            //movement.ChangeModePortal(GameMode, Speed, Gravity ? 1:-1, State);
        }
        catch { }
    }
}
