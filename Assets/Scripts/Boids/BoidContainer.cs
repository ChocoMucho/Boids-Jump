using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidContainer : MonoBehaviour
{
    private Boid boid;

    public float radius;

    public float boundaryForce;

    void Start()
    {
        boid = GetComponent<Boid>();
    }

    
    void Update()
    {
        if(radius < boid.transform.position.magnitude)
        {
            boid.velocity += 
                this.transform.position.normalized * 
                (radius - boid.transform.position.magnitude) * 
                boundaryForce * 
                Time.deltaTime;
            // 원점->boid 방향 x -(boid가 벗어난 정도) x 힘 x 델타타임
        }
    }
}
