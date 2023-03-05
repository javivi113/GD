using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingPlatform : MonoBehaviour
{
    public GameObject Square_Left_Die_Top_Walk_FlashingOk;
    public Material platformOkey_Show;
    public Material platformOkey_Hide;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Renderer renderer = Square_Left_Die_Top_Walk_FlashingOk.GetComponent<Renderer>();
        
        renderer.material = platformOkey_Show;
        StartCoroutine(Timeout());
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(1f);
        // Your code to execute after the timeout
        //renderer.material = platformOkey_Hide;
    }




}
