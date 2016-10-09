using UnityEngine;
using System.Collections;

public class AntiBioControllerStar1 : MonoBehaviour
{
    float speed = 0.1f;
    private Transform target;
    private float delay = 0f;


    private void setDirection()
    {
        transform.LookAt(target.position);
    }

    private void DeathCountDown()
    {
        delay -= Time.deltaTime;
        
        
    }

    private void setMove()
    {
        transform.Translate(transform.forward * speed);
    }

    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update ()
    {
        setDirection();

        if (delay == 0) setMove();
        else DeathCountDown();
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.tag == "Player") delay = 1.0f;
        }
    }

}
