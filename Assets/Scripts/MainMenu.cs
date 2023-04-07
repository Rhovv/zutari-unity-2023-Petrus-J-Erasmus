using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void btnLevelOne_click(string s)
    {
        Controller.loadScene(s);
    }

    public void btnWeather_click(string s)
    {
        Controller.loadScene(s);
    }

    public void btnExit_click()
    {
        Controller.exit();
    }
}
