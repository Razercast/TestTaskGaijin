using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerObj : MonoBehaviour
{
    public List<Item> spawnObjects;
    public int maxCount;
    public Queue<Item> spawnedObjects { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        spawnedObjects = new Queue<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
