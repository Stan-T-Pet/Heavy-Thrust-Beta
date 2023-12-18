using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 50f;
    
    void Start()
    {
        
    }

    void Update()
    {
        //Move bullet forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

}
