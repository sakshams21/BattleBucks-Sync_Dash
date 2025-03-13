using System;
using UnityEngine;
 
 public class PlatformSection : MonoBehaviour
 {
     [SerializeField] private Vector3[] Obstacle_Positions;

     private float _moveSpeed;
     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             //Spawn the next one
         }

         if (other.CompareTag("BackToPool"))
         {
             //back to pool
         }
     }

     private void Update()
     {
         transform.position += Vector3.left * (_moveSpeed * Time.deltaTime);
     }

     public void SpeedChange(float speed)
     {
         _moveSpeed = speed;
     }

     public void OnSectionSpawn()
     {
         //Put traps and pickups randomly
         
     }

     public void OnSectionDestroy()
     {
         //put it back to pool
     }
 
 }