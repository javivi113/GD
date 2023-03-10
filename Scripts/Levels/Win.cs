using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Win : MonoBehaviour
{
    public TMP_Text textComponent;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Movement movement = collision.gameObject.GetComponent<Movement>();
        movement.End();
        textComponent.text = "WIN!";
    }



}
