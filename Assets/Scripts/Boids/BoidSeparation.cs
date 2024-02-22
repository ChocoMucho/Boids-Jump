using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Separation : 분리
// 이웃들에게서 벗어나는 규칙
// => (나 - 이웃) 벡터 값을 모두 더하면 벗어나는 방향이 나옴

[RequireComponent(typeof(Boid))]
public class BoidSeparation : MonoBehaviour
{
    private Boid boid;

    public float radius;

    public float seperateForce;

    List<GameObject> neighbors;

    void Start()
    {
        boid = GetComponent<Boid>(); // 이하 명칭 "새"
        neighbors = BoidSpawner.Instance.Boids;
    }

    void Update()
    {
        //TODO : Find => 스포너에서 리스트로 관리하게 해야함
         // 모든 새
        Vector3 average = Vector3.zero;
        float found = 0; // 찾은 새의 수

        foreach (GameObject neighbor in neighbors) //TODO : Where 개선
        {
            if (neighbor == boid) { continue; }

            Vector3 diff = this.transform.position - neighbor.transform.position; 
            // TODO : magnitude 개선
            if (Mathf.Abs(diff.sqrMagnitude) < radius * radius) // 범위 안에 들어왔다면
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
