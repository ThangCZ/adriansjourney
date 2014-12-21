using UnityEngine;
using System.Collections;

public class PickupControllerHealth : MonoBehaviour {

    public int containedHealth;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player")) {
            PlayerStatsHolder playerStatsHolder = other.gameObject.GetComponent<PlayerStatsHolder>();
            if (playerStatsHolder.modifyHealth(containedHealth))
            {
                Destroy(gameObject);
            }
        }
    }
}
