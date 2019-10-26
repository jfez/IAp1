using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalizaTimer : MonoBehaviour
{
    private Image timerImage;

    private Color imageColor;
    private Movement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        timerImage = GetComponent<Image>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        timerImage.fillAmount = playerMovement.timerBaliza/playerMovement.balizaCD;
        imageColor = new Color(1-(playerMovement.timerBaliza/playerMovement.balizaCD), 0, playerMovement.timerBaliza/playerMovement.balizaCD);
        timerImage.color = imageColor;
    }
}
