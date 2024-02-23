using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Alignment : 정렬
// 이웃들의 평균 방향으로 이동하는 규칙
// => 평균 속도를 구하면 방향이 나옴


[RequireComponent(typeof(Boid))]
public class BoidAlignment : MonoBehaviour
{
    private Boid boid;

    public float radius;

    List<GameObject> neighbors;

    void Start()
    {
        boid = GetComponent<Boid>(); // 이하 명칭 "새"
        neighbors = BoidManager.Instance.Boids;
    }

    void Update()
    { 
        Vector3 average = Vector3.zero;
        float found = 0; // 찾은 새의 수

        foreach (GameObject neighbor in neighbors) //TODO : Where 개선
        {
            if (neighbor == boid){ continue; }

            Vector3 diff = neighbor.transform.position - this.transform.position; // 찾은 새와 나 사이의 벡터
            // TODO : magnitude 개선
            if (diff.sqrMagnitude < radius * radius) // 범위 안에 들어왔다면
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
