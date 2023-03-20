﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIArena : MonoBehaviour
{
    [SerializeField]private Transform healthCollectiblesGroup;
    [SerializeField]private Transform ground;
    private Movement movementComponent;
    private Health healthComponent;
    private Vector2 movementData;
    private GameObject player;
    private GameObject currentTarget;
    private Rigidbody rb;

    private void Awake()
    {
        this.movementComponent = GetComponent<Movement>();
        this.healthComponent = GetComponent<Health>();
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.rb = GetComponent<Rigidbody>();
    }

   private void FixedUpdate()
   {
       moveToTarget(doDecision());
   }

    private Vector3 doDecision()
    {
        this.currentTarget = target();
        if(!isAvailable(currentTarget))
        {
            return this.player.transform.position;
        }
        else
        {
            return this.currentTarget.transform.position; 
        }
    }

    private void moveToTarget(Vector3 locationToGo)
    {
        this.movementData.x = locationToGo.x > transform.position.x ? 1f : locationToGo.x < transform.position.x ? -1f : 0f;
        this.movementData.y = locationToGo.z > transform.position.z ? 1f : locationToGo.z < transform.position.z ? -1f : 0f;  

        this.movementComponent.changeMovementData(movementData);
        this.movementComponent.moveCharacter(rb);
    }

   private GameObject target()
   {
        if (healthComponent.getCurrentHealth() == 100)
        {
           return player;
        }
        else
        {
           return returnHealthCollectiblesAvailable();
        }
   }

   private bool isAvailable(GameObject gameObjectToCheck)
   {
       if(gameObjectToCheck != null && gameObjectToCheck.activeInHierarchy)
       {
           return true;
       }
        return false;
   }

   private GameObject returnHealthCollectiblesAvailable()
   {
       for (int i = 0 ; i < healthCollectiblesGroup.childCount ; i++)
       {
           if(isAvailable(healthCollectiblesGroup.GetChild(i).gameObject))
           {
               return healthCollectiblesGroup.GetChild(i).gameObject;
           }
       }
       return null;
   }
}
