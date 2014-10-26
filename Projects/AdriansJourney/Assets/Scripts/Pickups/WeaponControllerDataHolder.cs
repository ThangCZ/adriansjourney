using UnityEngine;
using System.Collections;

public class WeaponControllerDataHolder {

    public Vector3 usedRelativePosition;
    public Vector3 usedRelativeRotation;

    public Vector3 storedRelativePosition;
    public Vector3 storedRelativeRotation;

    public string label;

    public PlayerWeaponsHolder.WeaponUsedSlot weaponSlotUsed;

    public WeaponControllerDataHolder(PickupControllerWeapon pickupController) {
        usedRelativePosition = pickupController.usedRelativePosition;
        usedRelativeRotation = pickupController.usedRelativeRotation;
        storedRelativePosition = pickupController.storedRelativePosition;
        storedRelativeRotation = pickupController.storedRelativeRotation;
        label = pickupController.label;
        weaponSlotUsed = pickupController.weaponSlotUsed;
    }
}
