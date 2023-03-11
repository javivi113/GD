using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public TMP_Text txt;
    // bool godMode = false;
    public Movement mov;
    void Start()
    {
        if (mov.checkpointMode)
            txt.text = "God mode on";
        else
            txt.text = "God mode off";
    }
    public void reanudar()
    {
        mov.Pause();
    }

    public void switchValueGodMode()
    {
        if(!mov.checkpointMode)
        {
            txt.text = "God mode on";
            mov.checkpointMode = true;
        }
        else
        {
            txt.text = "God mode off";
            mov.checkpointMode = false;
        }
    }
    public void VolverMenu()
    {
        SceneManager.LoadScene(0);
    }
}
