using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;

public class Boid : MonoBehaviour
{
    [Header("Neighbor")]
    List<GameObject> nearNeighbors = new List<GameObject>();

    private float maxNeighbors = 50;
    //private float nearNeighbors = 0;
    private float neighborDistance = 5;

    public Vector3 velocity;
    public float maxVelocity;

    BoidManager spawner;

    [Header("TEST")]
    [SerializeField] private int neighborCount = 0;

    public void Init()
    {
        ;//What to do in init..
        
        //start coroutine
    }


    void Start()
    {
        velocity = transform.forward * maxVelocity;
        spawner = BoidManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        FindNeighbors();
        velocity += CalculateCohesion() * spawner.cohesionWeight;
        velocity += CalculateAlignment() * spawner.alignmentWeight;
        velocity += CalculateSeparation() * spawner.separationWeight;

        if (velocity.magnitude > maxVelocity) // prevent infinite accelation
            velocity = velocity.normalized * maxVelocity;

        this.transform.position += velocity * Time.deltaTime; // 가속도
        this.transform.rotation = Quaternion.LookRotation(velocity);

    }

    private void FindNeighbors()
    {
        nearNeighbors.Clear();
        foreach (GameObject neighbor in spawner.Boids) // 전체 이웃 탐색
        {
            if (neighbor == this.gameObject)
            {
                Debug.Log("Pass, because neighbor is me");
                continue;
            }

            Vector3 diff = neighbor.transform.position - this.transform.position;

            if(diff.sqrMagnitude < neighborDistance * neighborDistance) // 범위 내 이웃만 남기기
            {
                nearNeighbors.Add(neighbor);
            }
        }

        neighborCount = nearNeighbors.Count;
        Debug.Log(neighborCount);
    }


    private Vector3 CalculateCohesion()
    {
        Vector3 cohesionDirection = Vector3.zero;

        if(nearNeighbors.Count > 0)
        {
            for(int i = 0; i < nearNeighbors.Count; ++i)
            {
                cohesionDirection += nearNeighbors[i].transform.position - this.transform.position;
            }
            cohesionDirection /= nearNeighbors.Count;
            cohesionDirection.Normalize();
        }
        return cohesionDirection;
    }

    private Vector3 CalculateAlignment() 
    {
        Vector3 alignmentDirection = transform.forward;

        if (nearNeighbors.Count > 0)
        {
            for (int i = 0; i < nearNeighbors.Count; ++i)
            {
                alignmentDirection += nearNeighbors[i].transform.forward;
                //alignmentDirection += nearNeighbors[i].GetComponent<Boid>().velocity;
            }
            alignmentDirection /= nearNeighbors.Count;
            alignmentDirection.Normalize();
        }
        return alignmentDirection;
    }

    private Vector3 CalculateSeparation()
    {
        Vector3 separationDirection = Vector3.zero;

        if(nearNeighbors.Count > 0)
        {
            for(int i = 0; i < nearNeighbors.Count; ++i)
            {
                separationDirection += (this.transform.position - nearNeighbors[i].transform.position);
            }

            separationDirection /= nearNeighbors.Count;
            separationDirection.Normalize();
        }

        return separationDirection;
    }
}
