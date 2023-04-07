using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    static bool bInitialized = false;

    /*
        This function happens on startup. While it is checked every time the Main Menu scene is loaded,
        for this little I don't think it is necessary to add a background scene (which seems to be the
        generally excepted solution).
    */
    public static void initialize()
    {
        if (bInitialized) { return; }

        // Setting the culture info, ensures things like decimal delimantors are standard accross different applications
        CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

        bInitialized = true;
    }

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
