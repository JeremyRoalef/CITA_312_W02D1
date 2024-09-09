using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class VehicleController : MonoBehaviour
{
    [SerializeField] private float fltMoveSpeed = 10f;
    [SerializeField] private float fltTurnSpeed = 100f;
    [SerializeField] private float fltPlayerHealth = 10f;
    [SerializeField] ParticleSystem deathParticleSystem;


    Rigidbody myRigidBody;
    private int intScore = 0;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetAxis("Jump") > 0) //"Jump" corresponds to the space key
        {
            myRigidBody.velocity = transform.forward * 0;
        }
        else
        {
            MovePlayer();
        }
        RotatePlayer();

        if (fltPlayerHealth <= 0) {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            intScore++;
            Destroy(other.gameObject);
            Debug.Log("Score: " + intScore);
        }
        if (other.tag == "SpeedBoost")
        {
            Destroy(other.gameObject);
            fltMoveSpeed *= 1.3f;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            fltPlayerHealth -= 1;
            Vector3 pushObject = new Vector3(myRigidBody.velocity.x+1, myRigidBody.velocity.y + 1, myRigidBody.velocity.z + 1);
            other.rigidbody.AddForce(pushObject);
        }
    }
    private void MovePlayer()
    {
        myRigidBody.velocity = transform.forward * fltMoveSpeed * Input.GetAxis("Vertical");
    }
    private void RotatePlayer()
    {
        float rotate = Input.GetAxis("Horizontal") * fltTurnSpeed * Time.deltaTime;
        transform.Rotate(0, rotate, 0);
    }

    void Die()
    {
        Instantiate(deathParticleSystem, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
