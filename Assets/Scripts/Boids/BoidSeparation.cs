using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Separation : �и�
// �̿��鿡�Լ� ����� ��Ģ
// => (�� - �̿�) ���� ���� ��� ���ϸ� ����� ������ ����

[RequireComponent(typeof(Boid))]
public class BoidSeparation : MonoBehaviour
{
    private Boid boid;

    public float radius;

    public float seperateForce;

    List<GameObject> neighbors;

    void Start()
    {
        boid = GetComponent<Boid>(); // ���� ��Ī "��"
        neighbors = BoidSpawner.Instance.Boids;
    }

    void Update()
    {
        //TODO : Find => �����ʿ��� ����Ʈ�� �����ϰ� �ؾ���
         // ��� ��
        Vector3 average = Vector3.zero;
        float found = 0; // ã�� ���� ��

        foreach (GameObject neighbor in neighbors) //TODO : Where ����
        {
            if (neighbor == boid) { continue; }

            Vector3 diff = this.transform.position - neighbor.transform.position; 
            // TODO : magnitude ����
            if (Mathf.Abs(diff.sqrMagnitude) < radius * radius) // ���� �ȿ� ���Դٸ�
            {
                average += diff;
                ++found;
            }
        }

        if (found > 0)
        {
            average = average / found;
            boid.velocity += Vector3.Lerp(Vector3.zero, average, average.magnitude / radius) * seperateForce;
        }
    }
}
