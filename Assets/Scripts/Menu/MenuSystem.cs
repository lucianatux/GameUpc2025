using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour

{
    public Animator transitionAnimator;

  
    public void Play()
    
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Jugando");
    }
    


    public void Exit()
    {
        Application.Quit();
        Debug.Log("Saliste del juego");
    }
    
   
}