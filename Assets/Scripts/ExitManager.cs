using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    public GameObject canvasPause;

    [HideInInspector] public bool pause;
    
    // Start is called before the first frame update
    void Start()
    {
        canvasPause.SetActive(false);
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause){
            canvasPause.SetActive(true);
            pause = true;
            Time.timeScale = 0f;


        }

        else if (Input.GetKeyDown(KeyCode.Escape) && pause){
            canvasPause.SetActive(false);
            pause = false;
            Time.timeScale = 1f;


        }
    }

    public void Back(){
        canvasPause.SetActive(false);
        pause = false;
        Time.timeScale = 1f;

    }

    public void Exit(){
        pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Initial");

    }
}
