using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpColision : MonoBehaviour
{
    public enum JumpType{ JumpImpulse, GravityChange }
    public JumpType jumpType;

    void OnCollisionEnter2D(Collision2D collision)
    {

        try
        {
            //Movement movement = collision.gameObject.GetComponent<Movement>();
            //switch (jumpType)
            //{
            //    case JumpType.JumpImpulse:                    
            //        movement.Jump();
            //        break; 
            //    case JumpType.GravityChange:
            //        movement.Gravity = movement.Gravity * (-1);
            //        movement.ChangeModePortal(0, 0, movement.Gravity, 2);
            //        break;
            //}
        }
        catch { }
    }
}
