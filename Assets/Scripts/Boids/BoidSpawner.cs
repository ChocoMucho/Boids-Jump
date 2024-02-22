using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public static BoidSpawner Instance;

    public GameObject prefab;

    public float radius;

    public int number;

    public List<GameObject> Boids = new List<GameObject>();

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
