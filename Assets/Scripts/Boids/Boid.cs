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
    [SerializeField] private Vector3 targetVector;

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
        //velocity = transform.forward * 1;

        //start coroutine
    }


    void Update()
    {
        FindNeighbors();

        velocity += CalculateCohesion() * spawner.cohesionWeight;
        velocity += CalculateAlignment() * spawner.alignmentWeight;
        velocity += CalculateSeparation() * spawner.separationWeight;
        velocity += LimitMoveRadius();


        if (velocity.magnitude > spawner.maxSpeed) // prevent infinite accelation
            velocity = velocity.normalized * spawner.maxSpeed;

        this.transform.position += velocity * Time.deltaTime; // ���ӵ�
        this.transform.rotation = Quaternion.LookRotation(velocity);
    }


    private void FindNeighbors() // TODO : �ڷ�ƾ���� ���� ���� �ʿ�
    {
        nearNeighbors.Clear();

        foreach (GameObject neighbor in spawner.Boids) // ��ü �̿� Ž��
        {
            if (nearNeighbors.Count >= spawner.maxNeighbors)
                return;
            
            if (neighbor == this.gameObject)
            {
                Debug.Log("Pass, because neighbor is me");
                continue;
            }

            Vector3 diff = neighbor.transform.position - this.transform.position;

            if (diff.sqrMagnitude < spawner.neighborDistance * spawner.neighborDistance) // ���� �� �̿��� �����
            {
                nearNeighbors.Add(neighbor);
            }
        }

        neighborCount = nearNeighbors.Count;
        Debug.Log(neighborCount);
    }


    #region Cohesion ��� �޼���
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


    #region Alignment ��� �޼���
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


    #region Separation ��� �޼���
    private Vector3 CalculateSeparation()
    {
        Vector3 separationDirection = Vector3.zero;

        if(nearNeighbors.Count > 0)
        {
            for(int i = 0; i < nearNeighbors.Count; ++i)
            {
                separationDirection += (this.transform.position - nearNeighbors[i].transform.position); 
            }

            separationDirection.Normalize();
        }

        return separationDirection;
    }
    #endregion


    private Vector3 LimitMoveRadius() // TODO : ������ ���� ���͸� ��ȯ�ϴ� ������??
    {
        Vector3 returnVector = Vector3.zero;
        if (spawner.moveRadiusRange < this.transform.position.magnitude)
        {
            returnVector +=
                (this.transform.position - spawner.moveCenter).normalized *
                (spawner.moveRadiusRange - (this.transform.position - spawner.moveCenter).magnitude) *
                spawner.boundaryForce * Time.deltaTime;
            // ����->boid ���� x -(boid�� ��� ����) x �� x ��ŸŸ��

            returnVector.Normalize();
        }

        return returnVector;
    }
}

