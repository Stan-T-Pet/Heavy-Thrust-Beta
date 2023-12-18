/***
 * 1) find * with tag: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
 * 1.5) https://forum.unity.com/threads/how-to-find-a-gameobject-by-tag.172188/
 * 2) https://youtu.be/--u20SaCCow?t=440
 ***/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private GameObject player;//player obj reference to point bullet at
    private Rigidbody rigBod;
    public float bulletSpeed = 100.0f;//Speed of the bullet

    void Start()
    {
        //
        rigBod = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player"); //using "Player" tag to find position of player

        //giving bullet direction and location to go in
        Vector3 direction = (player.transform.position - transform.position).normalized; 
       
       //Set Bullet Speed
       rigBod.velocity = direction * bulletSpeed;

       transform.rotation = Quaternion.LookRotation(direction);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
    }
}
