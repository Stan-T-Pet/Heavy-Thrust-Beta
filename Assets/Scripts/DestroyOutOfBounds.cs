using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    //game boundaries
    private float topBound = 200;
    private float lowBound = -200;

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }else if (transform.position.z < lowBound)
        {
            Destroy(gameObject);
        }
    }
}
