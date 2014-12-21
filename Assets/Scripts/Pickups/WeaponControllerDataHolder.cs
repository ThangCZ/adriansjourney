using UnityEngine;
using System.Collections;

public class WeaponControllerDataHolder {

    public Vector3 usedRelativePosition;
    public Vector3 usedRelativeRotation;

    public Vector3 storedRelativePosition;
    public Vector3 storedRelativeRotation;

    public string label;

    public PlayerWeaponsHolder.WeaponType weaponType;
    public PlayerWeaponsHolder.WeaponUsedSlot weaponSlotUsed;

    public int damage;
    public float attackDuration;

    public WeaponControllerDataHolder(PickupControllerWeapon pickupController) {
        usedRelativePosition = pickupController.usedRelativePosition;
        usedRelativeRotation = pickupController.usedRelativeRotation;
        storedRelativePosition = pickupController.storedRelativePosition;
        storedRelativeRotation = pickupController.storedRelativeRotation;
        label = pickupController.label;
        weaponSlotUsed = pickupController.weaponSlotUsed;
        weaponType = pickupController.weaponType;
        damage = pickupController.damage;
        attackDuration = pickupController.attackDuration;
    }
}
