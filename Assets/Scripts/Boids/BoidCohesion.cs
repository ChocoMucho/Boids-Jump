using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Cohesion : ������
// �̿����� �߰� ��ġ�� ã�� �װ��� ���� �̵��ϴ� ��Ģ
// => �̿��� ���� �ȿ� �ִ� �͵��̶� ���ڱ� �߰����� ���� �� �ִ�.

[RequireComponent(typeof(Boid))]
public class BoidCohesion : MonoBehaviour
{
    private Boid boid;

    public float radius;

    List<GameObject> neighbors;

    void Start()
    {
        boid = GetComponent<Boid>(); // ���� ��Ī "��"
        neighbors = BoidManager.Instance.Boids;
    }

    void Update()
    {
        Vector3 average = Vector3.zero;
        float found = 0; // ã�� ���� ��

        foreach(GameObject neighbor in neighbors) //TODO : Where ����
        {
            if (neighbor == boid) { continue; }

            Vector3 diff = neighbor.transform.position - this.transform.position; // ã�� ���� �� ������ ����
            // TODO : magnitude ����
            if(diff.sqrMagnitude < radius * radius) // ���� �ȿ� ���Դٸ�
            {
                average += diff;
                ++found;
            }
        }

        if(found > 0)
        {
            average = average / found;
            boid.velocity += Vector3.Lerp(Vector3.zero, average, average.magnitude / radius);
        }
    }
}
