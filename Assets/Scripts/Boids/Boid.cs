using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TestTools;

public class Boid : MonoBehaviour
{
    [Header("Neighbor")]
    List<GameObject> nearNeighbors = new List<GameObject>();

    [Header("MoveInform")]
    [SerializeField] private Vector3 velocity; 

    BoidManager spawner;

    [Header("TEST")]
    [SerializeField] private int neighborCount = 0;

    void Start()
    {
        Init();
    }


    public void Init()
    {
        spawner = BoidManager.Instance;
        velocity = transform.forward * spawner.maxSpeed;


        //start coroutine
    }


    void Update()
    {
        FindNeighbors();

        velocity += CalculateCohesion() * spawner.cohesionWeight;
        velocity += CalculateAlignment() * spawner.alignmentWeight;
        velocity += CalculateSeparation() * spawner.separationWeight;
        LimitMoveRadius();

        if (velocity.magnitude > spawner.maxSpeed) // prevent infinite accelation
            velocity = velocity.normalized * spawner.maxSpeed;

        this.transform.position += velocity * Time.deltaTime; // 가속도
        this.transform.rotation = Quaternion.LookRotation(velocity);
    }

    private void FindNeighbors()
    {
        nearNeighbors.Clear();

        foreach (GameObject neighbor in spawner.Boids) // 전체 이웃 탐색
        {
            if (nearNeighbors.Count >= spawner.maxNeighbors)
                return;
            
            if (neighbor == this.gameObject)
            {
                Debug.Log("Pass, because neighbor is me");
                continue;
            }

            Vector3 diff = neighbor.transform.position - this.transform.position;

            if (diff.sqrMagnitude < spawner.neighborDistance * spawner.neighborDistance) // 범위 내 이웃만 남기기
            {
                nearNeighbors.Add(neighbor);
            }
        }

        neighborCount = nearNeighbors.Count;
        Debug.Log(neighborCount);
    }


    #region Cohesion 계산 메서드
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
    #endregion


    #region Alignment 계산 메서드
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
    #endregion


    #region Separation 계산 메서드
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
    #endregion


    private void LimitMoveRadius()
    {
        if (spawner.moveRadiusRange < this.transform.position.magnitude)
        {
            velocity +=
                (this.transform.position - Vector3.zero).normalized *
                (spawner.moveRadiusRange - (this.transform.position - Vector3.zero).magnitude) *
                spawner.boundaryForce *
                Time.deltaTime;
            // 원점->boid 방향 x -(boid가 벗어난 정도) x 힘 x 델타타임
        }
    }
}
