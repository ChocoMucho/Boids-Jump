using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public static BoidManager Instance;

    public GameObject prefab;

    [Header("Instantiate")]
    public float radius;
    public int number;

    public List<GameObject> Boids = new List<GameObject>();

    [Header("Manage")]
    public float cohesionWeight = 1.0f;
    public float alignmentWeight = 1.0f;
    public float separationWeight = 1.0f;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < number; ++i)
        {
            Boids.Add(Instantiate(prefab, this.transform.position + Random.insideUnitSphere * radius, Random.rotation));
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
