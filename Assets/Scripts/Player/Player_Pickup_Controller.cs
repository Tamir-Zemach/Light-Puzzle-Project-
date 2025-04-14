
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;

public class Player_Pickup_Controller : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody _objectRigidbody;
    private GameObject _objectThatGotPickedUp; 

    [SerializeField] private CinemachineCamera _pickupCam;
    private float _defaultCameraTopClamp;
    private float _defaultCameraBottomClamp;
    [SerializeField] private GameObject _playerCameraRoot;

    private CheckForPickables _checkForPickables;
    private ThirdPersonController _playerContoller;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerContoller = gameObject.GetComponentInParent<ThirdPersonController>();
        _checkForPickables = gameObject.GetComponentInParent<CheckForPickables>();
        _defaultCameraTopClamp = _playerContoller.TopClamp;
        _defaultCameraBottomClamp = _playerContoller.BottomClamp;

    }


    public void Pickup()
    {
        AttachPickedUpObject();

        SetPickupCameraMode();

    }
    private void AttachPickedUpObject()
    {
        _objectThatGotPickedUp = _checkForPickables._hitInfo.transform.gameObject;
        _objectRigidbody = _objectThatGotPickedUp.GetComponent<Rigidbody>();
        Transform objectTransform = _objectThatGotPickedUp.transform;
        objectTransform.parent = transform;
        objectTransform.SetPositionAndRotation(transform.position, transform.rotation);

        _objectRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void SetPickupCameraMode()
    {
        _pickupCam.enabled = true;
        _playerContoller.TopClamp = 0;
        _playerContoller.BottomClamp = 0;
        _playerContoller.CinemachineCameraTarget = _player;
    }

    public void DropPickup() // should be seperated to inputs and logic i think
    {

            DetachPickedUpObject(_objectRigidbody);

            ResetCameraDropMode();

            _objectThatGotPickedUp = null;
    }

    private void DetachPickedUpObject(Rigidbody objectRigidbody)
    {
        objectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _objectThatGotPickedUp.transform.SetParent(null);
        objectRigidbody.linearVelocity = Vector3.zero;
    }

    private void ResetCameraDropMode() //should probably change name to reflect changes to camera
    {
        _pickupCam.enabled = false;
        _playerContoller.TopClamp = _defaultCameraTopClamp;
        _playerContoller.BottomClamp = _defaultCameraBottomClamp;
        _playerContoller.CinemachineCameraTarget = _playerCameraRoot;
    }

    public void DropPickupInFrontOfWall(Vector3 dropPoint) 
    {

            DetachPickedUpObject(_objectRigidbody);
            _objectThatGotPickedUp.transform.position = dropPoint;
            _objectThatGotPickedUp = null;
            ResetCameraDropMode();

    }


}
