using Shirotetsu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ShockWave>().IncreaseGaugePowerUp();
            Destroy(gameObject);
        }
    }
}