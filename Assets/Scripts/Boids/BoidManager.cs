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
    public float cohesionWeight = 1.0f;     // 3��Ģ ����ġ
    public float alignmentWeight = 1.0f;
    public float separationWeight = 1.0f;
    public Vector3 moveCenter = Vector3.zero;
    public float moveRadiusRange = 5.0f;    // Ȱ�� ���� ������
    public float boundaryForce = 3.5f;      // ���� ���� ���ư��� �ϴ� ��
    public float maxSpeed = 2.0f;
    public float neighborDistance = 3.0f;   // �̿� Ž�� ����
    public float maxNeighbors = 50;         // �̿� Ž�� �� ����

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < number; ++i)
        {
            Boids.Add(Instantiate(prefab, moveCenter + Random.insideUnitSphere * InstantiateRadius, Random.rotation));
        }
    }
}
