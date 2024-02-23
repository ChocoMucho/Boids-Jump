using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Alignment : ����
// �̿����� ��� �������� �̵��ϴ� ��Ģ
// => ��� �ӵ��� ���ϸ� ������ ����


[RequireComponent(typeof(Boid))]
public class BoidAlignment : MonoBehaviour
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

        foreach (GameObject neighbor in neighbors) //TODO : Where ����
        {
            if (neighbor == boid){ continue; }

            Vector3 diff = neighbor.transform.position - this.transform.position; // ã�� ���� �� ������ ����
            // TODO : magnitude ����
            if (diff.sqrMagnitude < radius * radius) // ���� �ȿ� ���Դٸ�
            {
                //average += neighbor.GetComponent<Boid>().velocity;
                ++found;
            }
        }

        if (found > 0)
        {
            average = average / found;
            //boid.velocity += Vector3.Lerp(boid.velocity, average, Time.deltaTime);
        }
    }
}
