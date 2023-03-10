using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelsMenuScript : MonoBehaviour
{
    public TMP_Text txt;
    public GameObject ErrorLevel;
    int posi=1;
    private int levelCant = 3;


    public void backLevel()
    {
        if (posi != 1)
            posi--;
        else
            posi = levelCant;
        txt.text = "Nivel " + posi;
    }
    public void nextLevel()
    {
        if (posi != levelCant)
            posi++;
        else
            posi = 1;
        txt.text = "Nivel " + posi;
        
    }
    public void PlayLevel()
    {
        try
        {
            SceneManager.LoadScene(posi);
        }
        catch (UnityException e)
        {
            Debug.LogError("Failed to load scene: " + e.Message);
            ErrorLevel.SetActive(false);

        }
    }
    public void okError()
    {
        ErrorLevel.SetActive(true);
    }

}
