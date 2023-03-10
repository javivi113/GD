using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttemptsTextRemover : MonoBehaviour
{
    public TMP_Text textComponent;

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        textComponent.text = "";


    }
    

    
}
