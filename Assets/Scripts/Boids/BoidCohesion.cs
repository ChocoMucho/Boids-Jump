using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Cohesion : 응집력
// 이웃들의 중간 위치를 찾고 그곳을 향해 이동하는 규칙
// => 이웃은 범위 안에 있는 것들이라 갑자기 중간지점 변할 수 있다.

[RequireComponent(typeof(Boid))]
public class BoidCohesion : MonoBehaviour
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

        foreach(GameObject neighbor in neighbors) //TODO : Where 개선
        {
            if (neighbor == boid) { continue; }

            Vector3 diff = neighbor.transform.position - this.transform.position; // 찾은 새와 나 사이의 벡터
            // TODO : magnitude 개선
            if(diff.sqrMagnitude < radius * radius) // 범위 안에 들어왔다면
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
