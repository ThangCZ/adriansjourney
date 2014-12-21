using UnityEngine;
using System.Collections;

public class PickupControllerWeapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    public GameObject pickupPrefab;

    public Vector3 usedRelativePosition;
    public Vector3 usedRelativeRotation;

    public Vector3 storedRelativePosition;
    public Vector3 storedRelativeRotation;

    public string label;

    public PlayerWeaponsHolder.WeaponType weaponType;
    public PlayerWeaponsHolder.WeaponUsedSlot weaponSlotUsed;

    public int damage;
    public float attackDuration;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerWeaponsHolder playerWeaponsHolder = other.gameObject.GetComponent<PlayerWeaponsHolder>();
            playerWeaponsHolder.OnPickUp(gameObject, this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerWeaponsHolder playerWeaponsHolder = other.gameObject.GetComponent<PlayerWeaponsHolder>();
            playerWeaponsHolder.OnEndOfPickUp(gameObject, this);
        }
    }
}
