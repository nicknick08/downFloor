using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{   
    public void LoadScene(string sceneName){
        Time.timeScale = 1f;
        SceneManager.LoadScene (sceneName);
    }
    public void EndGame(){
        Application.Quit();
    }
}
