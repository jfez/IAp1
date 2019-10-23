using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
        SceneManager.LoadScene("Level1");

    }

    public void Levels(){
        //SceneManager.LoadScene("Level1");

    }

    public void Credits(){
        //SceneManager.LoadScene("Level1");

    }

    public void Exit(){
        Application.Quit();

    }
}
