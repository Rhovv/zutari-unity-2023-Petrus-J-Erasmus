using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public static bool bLevelOneInit = false;
    public static bool bWeatherInit = false;

    // Function handling the scene loading
    public static void loadScene(string s)
    {
        try
        {
            SceneManager.LoadScene(s);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    /*
        Function that handles exiting the application. If this is done while in the editor, it
        simply stops play, but in the live application it will quit the application.
    */
    public static void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
