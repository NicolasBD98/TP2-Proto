using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonWithHealth : MonoBehaviour
{
    private int minLife = 0;
    public int maxLife;
    public int life;
    private bool healthHasBeenModified;
    GameObject healthBar;

    //Doit être appelé par l'enfant dans start.
    public void SetupHealth(int newMaxLife)
    {
        healthHasBeenModified = false;
        healthBar = transform.Find("HealthBar").gameObject;
        maxLife = newMaxLife;
        life = maxLife;
    }

    //Doit être appelé par l'enfant dans update.
    public void UpdateLife()
    {
        if (healthHasBeenModified)
        {
            healthBar.GetComponent<HealthBarFiller>().fillBar(life, maxLife);
            healthHasBeenModified = false;
        }
    }

    public void takeDamage(int damage)
    {
        if (life > minLife)
        {
            life -= damage;
            healthHasBeenModified = true;
        }
    }

    public void Heal(int lifePoints)
    {
        if (life < maxLife)
        {
            life += lifePoints;
            healthHasBeenModified = true;
        }
    }

    public bool NoLifeLeft()
    {
        return (life == minLife);
    }
}
