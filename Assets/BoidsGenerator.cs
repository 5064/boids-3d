using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BoidsGenerator : MonoBehaviour
{
    public GameObject prefab;
    private int COUNT = 1;
    List<GameObject> boids = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.COUNT; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, Commons.Const.CANVAS[0]), Random.Range(0, Commons.Const.CANVAS[1]), Random.Range(0, Commons.Const.CANVAS[2]));
            boids.Add((GameObject)Instantiate(prefab, pos, Random.rotation));
        }

        Octree<GameObject> octree = new Octree<GameObject>(Commons.Const.CANVAS[0], Commons.Const.CANVAS[1], Commons.Const.CANVAS[2]);
        foreach (GameObject boid in boids)
        {

            octree.Insert(boid.transform.position.x, boid.transform.position.y, boid.transform.position.z, boid);
        }
        foreach (var list in octree.linearOctree)
        {
            if (list.Count > 0)
            {
                Debug.Log(list);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
