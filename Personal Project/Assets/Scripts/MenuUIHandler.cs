using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{


    private void Start()
    {


    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);

    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {

    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
                Application.Quit(); // original code to quit Unity player
    #endif
    }




}

