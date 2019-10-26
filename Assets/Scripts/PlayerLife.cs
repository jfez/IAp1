using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Image lifeBar;
    public GameObject canvasDeath;
    private Color imageColor;
    private float lifeCount;
    private float maxLife;

    private float lifeRecover;
    [HideInInspector] public bool dead;
    private float timerDamage;
    private float maxTimerDamage;
    private Movement movement;
    
    // Start is called before the first frame update
    void Start()
    {
        maxLife = 100f;
        lifeCount = maxLife;
        lifeRecover = 10f;
        dead = false;
        timerDamage = 0f;
        maxTimerDamage = 5f;
        canvasDeath.SetActive(false);
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        timerDamage += Time.deltaTime;
        
        lifeBar.fillAmount = lifeCount/maxLife;
        imageColor = new Color(1-(lifeCount/maxLife), lifeCount/maxLife, 0);
        lifeBar.color = imageColor;

        
        if (lifeCount < 100 && !dead && timerDamage > maxTimerDamage){
            lifeCount += lifeRecover * Time.deltaTime;
        }
        
    }

    

    public void TakeDamage(float damage){
        if (!movement.shieldActive){
            timerDamage = 0f;

            if(movement.growingShield){
                movement.growingShield = false;
                movement.timerShield = 0f;
                movement.shield.SetActive(false);
            }

            if (lifeCount-damage <= 0){
                lifeCount = 0;
                Death();
            }

            else{
                lifeCount -= damage;

            }

        }

        else{
            movement.shieldActive = false;
            movement.timerShield = 0f;
            movement.shield.SetActive(false);
        }
        
    }

    void Death(){
        dead = true;
        Time.timeScale = 0f;
        canvasDeath.SetActive(true);


    }
}
