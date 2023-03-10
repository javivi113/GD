using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpColision : MonoBehaviour
{
    public enum JumpType{ JumpImpulse, CubeGravityChange }
    public JumpType jumpType;

    void OnCollisionEnter2D(Collision2D collision)
    {

        try
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();
            switch (jumpType)
            {
                case JumpType.JumpImpulse:
                    movement.Jump(1.4f);
                    break;
                case JumpType.CubeGravityChange:
                    movement.changeCubeCubeInvert();
                    break;
            }
        }
        catch { }
    }
}
