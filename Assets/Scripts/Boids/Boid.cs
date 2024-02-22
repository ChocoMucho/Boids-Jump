using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity;
    public float maxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.forward * maxVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (velocity.magnitude > maxVelocity) // 무한 가속 방지
            velocity = velocity.normalized * maxVelocity;

        this.transform.position += velocity * Time.deltaTime; // 가속도
        this.transform.rotation = Quaternion.LookRotation(velocity);

    }
}
