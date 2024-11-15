using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Projectile : MonoBehaviour
{

    // Controls projectile speed
    public float projectileSpeed = 1f;
    public int damage = 1;

    // target game object for when player is aiming at a specific enemy
    private GameObject trackingTarget;

    // Checks every frame if our receiver gets set to anything other than null
    // Player class calls setTarget when it fires while an enemy is in range
    void Update() 
    {
        if (trackingTarget != null)
        {
            trackingProj();
        }
    }

    //Method called after projectile instantiated in player class,
    //fires proj straight 
    public void straightShot()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
    }

    // Sets the target to the reciever
    public void setTarget(GameObject receiver)
    {
            trackingTarget = receiver; 
    }

    //Tells projectile to track to its target and move to that pos
    public void trackingProj()
    {
        transform.position = Vector3.MoveTowards(transform.position, trackingTarget.transform.position, projectileSpeed * Time.deltaTime);
    }

    // If the projectile collides with an enemy, make the enemy take damage according to the spell's health
    // then, destroy the projectile
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().loseHealth(damage);
        } 
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().loseHealth(damage);
        }
 

        Destroy(gameObject);

    }

}
