using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GetComponent<IStatsModifier>().addToCharacter(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}