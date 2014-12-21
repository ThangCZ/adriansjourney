using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWeaponsHolder : MonoBehaviour {
    private const KeyCode SWITCH_WEAPONS_KEY = KeyCode.X;

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject body;

    private Vector3 rightHandDefaultRotation;
    private Vector3 leftHandDefaultRotation;
    private Vector3 bodyDefaultRotation;

    private WeaponControllerDataHolder weaponControllerMelee;
    private GameObject weaponObjectMelee;
    private GameObject weaponPickupPrefabMelee;

    private WeaponControllerDataHolder weaponControllerRanged;
    private GameObject weaponObjectRanged;
    private GameObject weaponPickupPrefabRanged;

    public Text pendingPickupDialog;

    public enum WeaponType { WeaponMelee, WeaponRangedBow, WeaponRangedCrossbow };
    public enum WeaponUsedSlot { RightHand, LeftHand };

    private WeaponType? currentlyEquipped = null;

    private bool pickupPending;
    private GameObject pendingPickupObject;
    private PickupControllerWeapon pendingPickupController;

    void Start() {
        rightHandDefaultRotation = rightHand.transform.rotation.eulerAngles;
        leftHandDefaultRotation = leftHand.transform.rotation.eulerAngles;
        bodyDefaultRotation = body.transform.rotation.eulerAngles;
    }

    private bool isRanged(WeaponType? weaponType) {
        return weaponType == WeaponType.WeaponRangedBow || weaponType == WeaponType.WeaponRangedCrossbow;
    }

    public void OnPickUp(GameObject pickupObject, PickupControllerWeapon pickupController) {
        if ((!isRanged(pickupController.weaponType)
            && weaponControllerMelee != null)
            || (isRanged(pickupController.weaponType)
            && weaponControllerRanged != null)) {
            // weapon pickup is pending
            pendingPickupObject = pickupObject;
            pendingPickupController = pickupController;
            pickupPending = true;

            pendingPickupDialog.text = "Press E to pickup " + pickupController.label;
        } else {
            // pick it right up
            PickUp(pickupObject, pickupController);
        }
    }

    private void setNewWeapon(GameObject weaponObject, GameObject pickupObject, WeaponControllerDataHolder controllerDataHolder,
        ref GameObject weaponObjectVariable, ref GameObject pickupObjectVariable, ref WeaponControllerDataHolder controllerDataHolderVariable) {
        controllerDataHolderVariable = controllerDataHolder;
        if (weaponObjectVariable != null) {
            // destroy old wepon of type currently being equipped
            Destroy(weaponObjectVariable);
            pickupObjectVariable.SetActive(true);
            pickupObjectVariable.transform.position =
                new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        }
        weaponObjectVariable = weaponObject;
        pickupObjectVariable = pickupObject;
    }

    private void PickUp(GameObject pickupObject, PickupControllerWeapon pickupController) {
        GameObject weaponObject = (GameObject)Instantiate(pickupController.weaponPrefab, new Vector3(), Quaternion.identity);
        GameObject pickupObjectClone = (GameObject)Instantiate(pickupController.pickupPrefab, new Vector3(), Quaternion.identity);
        pickupObjectClone.SetActive(false);
        WeaponControllerDataHolder weaponControllerDataHolder = new WeaponControllerDataHolder(pickupController);

        if (isRanged(pickupController.weaponType)) {
            setNewWeapon(weaponObject, pickupObjectClone, weaponControllerDataHolder,
                ref weaponObjectRanged, ref weaponPickupPrefabRanged, ref weaponControllerRanged);
        } else {
            setNewWeapon(weaponObject, pickupObjectClone, weaponControllerDataHolder,
                ref weaponObjectMelee, ref weaponPickupPrefabMelee, ref weaponControllerMelee);
        }


        bool used = currentlyEquipped == null || isRanged(currentlyEquipped) == isRanged(pickupController.weaponType);
        GameObject positionBase;
        Vector3 baseDefaultRotation;
        if (used) {
            if (pickupController.weaponSlotUsed == WeaponUsedSlot.RightHand) {
                positionBase = rightHand;
                baseDefaultRotation = rightHandDefaultRotation;
            } else {
                positionBase = leftHand;
                baseDefaultRotation = leftHandDefaultRotation;
            }
        } else {
            positionBase = body;
            baseDefaultRotation = bodyDefaultRotation;
        }
        MoveObjectToLocationAndParent(used, positionBase, baseDefaultRotation, weaponObject, weaponControllerDataHolder);

        if (currentlyEquipped == null) {
            currentlyEquipped = pickupController.weaponType;
        }

        Destroy(pickupObject);
    }

    private void MoveObjectToLocationAndParent(bool used, GameObject positionBase, Vector3 positionBaseDefaultRotation, GameObject o, WeaponControllerDataHolder controller) {
        Vector3 relativePosition;
        Vector3 relativeRotation;
        if (used) {
            relativePosition = controller.usedRelativePosition;
            relativeRotation = controller.usedRelativeRotation;
        } else {
            relativePosition = controller.storedRelativePosition;
            relativeRotation = controller.storedRelativeRotation;
        }

        Quaternion baseRotationDelta = Quaternion.Euler(positionBase.transform.rotation.eulerAngles - positionBaseDefaultRotation);

        o.transform.position =
            positionBase.transform.position
            + (baseRotationDelta * relativePosition);
        o.transform.rotation =
            positionBase.transform.rotation
            * Quaternion.Euler(relativeRotation.x, relativeRotation.y, relativeRotation.z);

        o.transform.parent = positionBase.transform;
    }

    public void OnEndOfPickUp(GameObject pickupObject, PickupControllerWeapon pickupController) {
        pickupPending = false;
        pendingPickupDialog.text = "";
    }

    void Update() {
        if (pickupPending && Input.GetKeyDown(KeyCode.E)) {
            PickUp(pendingPickupObject, pendingPickupController);
            pickupPending = false;
            pendingPickupObject = null;
            pendingPickupController = null;
            pendingPickupDialog.text = "";
        } else if (Input.GetKeyDown(SWITCH_WEAPONS_KEY)
            && weaponControllerMelee != null
            && weaponControllerRanged != null) {
            GameObject usedObject;
            WeaponControllerDataHolder usedControllerDataHolder;
            GameObject storedObject;
            WeaponControllerDataHolder storedControllerDataHolder;
            if (!isRanged(currentlyEquipped)) {
                currentlyEquipped = weaponControllerRanged.weaponType;
                usedObject = weaponObjectRanged;
                usedControllerDataHolder = weaponControllerRanged;
                storedObject = weaponObjectMelee;
                storedControllerDataHolder = weaponControllerMelee;
            } else {
                currentlyEquipped = weaponControllerMelee.weaponType;
                usedObject = weaponObjectMelee;
                usedControllerDataHolder = weaponControllerMelee;
                storedObject = weaponObjectRanged;
                storedControllerDataHolder = weaponControllerRanged;
            }

            GameObject usedPositionBase = usedControllerDataHolder.weaponSlotUsed == WeaponUsedSlot.RightHand ? rightHand : leftHand;
            Vector3 usedBaseRotationDelta = usedControllerDataHolder.weaponSlotUsed == WeaponUsedSlot.RightHand
                ? rightHandDefaultRotation : leftHandDefaultRotation;

            switchWeapons(usedObject, usedPositionBase,
                usedBaseRotationDelta, usedControllerDataHolder,
                storedObject, body,
                bodyDefaultRotation, storedControllerDataHolder);
        }
    }

    private void switchWeapons(GameObject usedObject, GameObject usedPositionBase, Vector3 usedBaseRotationDelta, WeaponControllerDataHolder usedControllerDataHolder,
        GameObject storedObject, GameObject storedPositionBase, Vector3 storedBaseRotationDelta, WeaponControllerDataHolder storedControllerDataHolder) {
        MoveObjectToLocationAndParent(true, usedPositionBase, usedBaseRotationDelta, usedObject, usedControllerDataHolder);
        MoveObjectToLocationAndParent(false, storedPositionBase, storedBaseRotationDelta, storedObject, storedControllerDataHolder);
    }

    public WeaponType? getCurrentlyEquippedWeaponType() {
        return currentlyEquipped;
    }

    public int getCurrentlyEquippedWeaponDamage() {
        WeaponControllerDataHolder weaponController = getCurrentWeaponController();
        return weaponController == null ? 0 : weaponController.damage;
    }

    public float getCurrentlyEquippedWeaponAttackDuration() {
        WeaponControllerDataHolder weaponController = getCurrentWeaponController();
        return weaponController == null ? 0 : weaponController.attackDuration;
    }

    private WeaponControllerDataHolder getCurrentWeaponController() {
        if (isRanged(getCurrentlyEquippedWeaponType())) {
            return weaponControllerRanged;
        } else {
            return weaponControllerMelee;
        }
    }
}
