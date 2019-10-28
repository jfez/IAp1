﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashTimer : MonoBehaviour
{
    private Image timerImage;

    private Color imageColor;
    private Movement playerMovement;

    private void Awake()
    {
        timerImage = GetComponent<Image>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        timerImage.fillAmount = playerMovement.timerDash/playerMovement.dashCD;
        imageColor = new Color(1-(playerMovement.timerDash/playerMovement.dashCD), 0, playerMovement.timerDash/playerMovement.dashCD);
        timerImage.color = imageColor;
    }
}
