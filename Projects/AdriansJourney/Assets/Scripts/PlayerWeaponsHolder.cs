using UnityEngine;
using System.Collections;

public class PlayerWeaponsHolder : MonoBehaviour
{

    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject body;

    private Vector3 rightHandDefaultRotation;
    private Vector3 leftHandDefaultRotation;
    private Vector3 bodyDefaultRotation;

    private WeaponControllerDataHolder weaponControllerMelee;
    private GameObject weaponObjectMelee;
    private WeaponControllerDataHolder weaponControllerRanged;
    private GameObject weaponObjectRanged;

    public GUIText pendingPickupDialog;

    public enum WeaponType { WeaponMelee, WeaponRanged };
    public enum WeaponUsedSlot { RightHand, LeftHand };

    private WeaponType? currentlyEquiped = null;

    private bool pickupPending;
    private GameObject pendingPickupObject;
    private PickupControllerWeapon pendingPickupController;

    void Start() {
        rightHandDefaultRotation = rightHand.transform.rotation.eulerAngles;
        leftHandDefaultRotation = leftHand.transform.rotation.eulerAngles;
        bodyDefaultRotation = body.transform.rotation.eulerAngles;
    }

    public void OnPickUp(GameObject pickupObject, PickupControllerWeapon pickupController)
    {
        if ((pickupController.weaponType == WeaponType.WeaponMelee
            && weaponControllerMelee != null)
            || (pickupController.weaponType == WeaponType.WeaponRanged
            && weaponControllerRanged != null))
        {
            // weapon pickup is pending
            pendingPickupObject = pickupObject;
            pendingPickupController = pickupController;
            pickupPending = true;

            pendingPickupDialog.text = "Press E to pickup " + pickupController.label;
        }
        else
        {
            // pick it right up
            PickUp(pickupObject, pickupController);
        }
    }

    private void PickUp(GameObject pickupObject, PickupControllerWeapon pickupController)
    {
        GameObject weaponObject = (GameObject)Instantiate(pickupController.weaponPrefab, new Vector3(), Quaternion.identity);
        WeaponControllerDataHolder weaponControllerDataHolder = new WeaponControllerDataHolder(pickupController);
        if (pickupController.weaponType == WeaponType.WeaponMelee)
        {
            weaponControllerMelee = weaponControllerDataHolder;
            if (weaponObjectMelee != null)
            {
                // destroy old wepon of type currently being equipped
                Destroy(weaponObjectMelee);
            }
            weaponObjectMelee = weaponObject;
        }
        else if (pickupController.weaponType == WeaponType.WeaponRanged)
        {
            weaponControllerRanged = weaponControllerDataHolder;
            if (weaponObjectRanged != null)
            {
                // destroy old wepon of type currently being equipped
                Destroy(weaponObjectRanged);
            }
            weaponObjectRanged = weaponObject;
        }
        bool used = currentlyEquiped == pickupController.weaponType || currentlyEquiped == null;
        GameObject positionBase;
        Vector3 baseDefaultRotation;
        if (used)
        {
            if (pickupController.weaponSlotUsed == WeaponUsedSlot.RightHand)
            {
                positionBase = rightHand;
                baseDefaultRotation = rightHandDefaultRotation;
            }
            else
            {
                positionBase = leftHand;
                baseDefaultRotation = leftHandDefaultRotation;
            }
        }
        else
        {
            positionBase = body;
            baseDefaultRotation = bodyDefaultRotation;
        }
        MoveObjectToLocationAndParent(used, positionBase, baseDefaultRotation, weaponObject, weaponControllerDataHolder);

        if (currentlyEquiped == null)
        {
            currentlyEquiped = pickupController.weaponType;
        }

        Destroy(pickupObject);
    }

    private void MoveObjectToLocationAndParent(bool used, GameObject positionBase, Vector3 positionBaseDefaultRotation, GameObject o, WeaponControllerDataHolder controller)
    {
        Vector3 relativePosition;
        Vector3 relativeRotation;
        if (used)
        {
            relativePosition = controller.usedRelativePosition;
            relativeRotation = controller.usedRelativeRotation;
        }
        else
        {
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

    public void OnEndOfPickUp(GameObject pickupObject, PickupControllerWeapon pickupController)
    {
        pickupPending = false;
        pendingPickupDialog.text = "";
    }

    void Update()
    {
        if (pickupPending && Input.GetKeyDown(KeyCode.E))
        {
            PickUp(pendingPickupObject, pendingPickupController);
            pickupPending = false;
            pendingPickupObject = null;
            pendingPickupController = null;
            pendingPickupDialog.text = "";
        }
        else if (Input.GetKeyDown(KeyCode.X)
          && weaponControllerMelee != null
          && weaponControllerRanged != null)
        {
            GameObject usedObject;
            WeaponControllerDataHolder usedControllerDataHolder;
            GameObject storedObject;
            WeaponControllerDataHolder storedControllerDataHolder;
            if (currentlyEquiped == WeaponType.WeaponMelee)
            {
                currentlyEquiped = WeaponType.WeaponRanged;
                usedObject = weaponObjectRanged;
                usedControllerDataHolder = weaponControllerRanged;
                storedObject = weaponObjectMelee;
                storedControllerDataHolder = weaponControllerMelee;
            }
            else
            {
                currentlyEquiped = WeaponType.WeaponMelee;
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
        GameObject storedObject, GameObject storedPositionBase, Vector3 storedBaseRotationDelta, WeaponControllerDataHolder storedControllerDataHolder)
    {
        MoveObjectToLocationAndParent(true, usedPositionBase, usedBaseRotationDelta, usedObject, usedControllerDataHolder);
        MoveObjectToLocationAndParent(false, storedPositionBase, storedBaseRotationDelta, storedObject, storedControllerDataHolder);
    }
}
