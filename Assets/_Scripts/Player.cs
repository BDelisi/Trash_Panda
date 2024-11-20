using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 10;
    private float iFrames = 0;

    // Update is called once per frame
    void Update()
    {
        if (iFrames != 0)
        {
            iFrames -= Time.deltaTime;
            if (iFrames < 0)
            {
                iFrames = 0;
            }
        }
    }

    // Method to be called by projectile that makes the player take damage according to the spell
    public void loseHealth(int projDamage)
    {
        if (iFrames <= 0)
        {
            health -= projDamage;
            iFrames = .25f;
        }
    }

}
