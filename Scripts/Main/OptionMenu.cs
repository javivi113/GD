using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OptionMenu : MonoBehaviour
{
    public TMP_Text txt;
    public static bool godMode = false;
    public void switchValueGodMode()
    {
        if(!godMode)
        {
            txt.text = "God mode on";
            godMode = true;
        }
        else
        {
            txt.text = "God mode off";
            godMode = false;
        }
    }
}
