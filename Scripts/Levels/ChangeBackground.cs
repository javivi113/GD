using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public Image imageComponent;
    public Sprite backgroundImage;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        imageComponent.sprite = backgroundImage;
        imageComponent.type = Image.Type.Simple;
        imageComponent.preserveAspect = false;
    }
}
