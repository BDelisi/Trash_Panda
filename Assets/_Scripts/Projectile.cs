using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Projectile : MonoBehaviour
{

    // Controls projectile speed
    public float projectileSpeed = 1f;
    public int damage = 1;
    public GameObject particles;

    // target game object for when player is aiming at a specific enemy
    private GameObject trackingTarget;
    private Rigidbody rb;
    private Vector3 lastPos;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }
    // Checks every frame if our receiver gets set to anything other than null
    // Player class calls setTarget when it fires while an enemy is in range
    void Update() 
    {
        if (trackingTarget != null)
        {
            trackingProj();
        }
        if (rb.velocity.normalized.magnitude == 0)
        {
            Vector3 newVector = lastPos - transform.position;
            GetComponent<Rigidbody>().velocity = newVector * projectileSpeed;
        }
    }

    //Method called after projectile instantiated in attacking class,
    //fires proj straight 
    public void straightShot()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
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
        lastPos = transform.position;
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

        GameObject temp = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(temp, .5f);
    }

}
