using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Image lifeBar;
    private Color imageColor;
    private float lifeCount;
    private float maxLife;

    private float lifeRecover;
    
    // Start is called before the first frame update
    void Start()
    {
        maxLife = 100f;
        //lifeCount = maxLife;
        lifeCount = 20f;
        lifeRecover = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
        lifeBar.fillAmount = lifeCount/maxLife;
        imageColor = new Color(1-(lifeCount/maxLife), lifeCount/maxLife, 0);
        lifeBar.color = imageColor;

        //if han pasado 5 segundos desde que recibió daño
        if (lifeCount < 100){
            lifeCount += lifeRecover * Time.deltaTime;
        }
        
    }
}
