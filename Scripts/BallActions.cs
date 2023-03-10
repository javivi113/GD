using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActions : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        Movement movement = collision.gameObject.GetComponent<Movement>();
        switch (collision.gameObject.tag)
        {
            case "JumpBall":
                if (Input.GetMouseButton(0))
                    movement.Jump(1f);
                break;
            case "GravityBall":
                //if (Input.GetMouseButton(0))
                movement.ChangeGravity(-1);
                break;
            case "0GravityDash":
                Debug.Log("No Gravity dash");
                movement.ChangeGravity(1);
                //if (Input.GetMouseButton(0))
                //    noGravityDash();

                break;
        }
    }
}