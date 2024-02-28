using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public static BoidManager Instance;

    public GameObject prefab;

    [Header("Init")]
    public float InstantiateRadius;
    public int number;

    public List<GameObject> Boids = new List<GameObject>();

    [Header("MoveManage")]
    public float cohesionWeight = 1.0f;     // 3규칙 가중치
    public float alignmentWeight = 1.0f;
    public float separationWeight = 1.0f;
    public Vector3 moveCenter = Vector3.zero;
    public float moveRadiusRange = 5.0f;    // 활동 범위 반지름
    public float boundaryForce = 3.5f;      // 범위 내로 돌아가게 하는 힘
    public float maxSpeed = 2.0f;
    public float neighborDistance = 3.0f;   // 이웃 탐색 범위
    public float maxNeighbors = 50;         // 이웃 탐색 수 제한

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < number; ++i)
        {
            Boids.Add(Instantiate(prefab, moveCenter + Random.insideUnitSphere * InstantiateRadius, Random.rotation));
        }
    }
}
