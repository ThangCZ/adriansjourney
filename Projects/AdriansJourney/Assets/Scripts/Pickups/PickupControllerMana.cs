using UnityEngine;
using System.Collections;

public class PickupControllerMana : MonoBehaviour
{

    public int containedMana;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerStatsHolder playerStatsHolder = other.gameObject.GetComponent<PlayerStatsHolder>();
            if (playerStatsHolder.modifyMana(containedMana))
            {
                Destroy(gameObject);
            }
        }
    }
}
