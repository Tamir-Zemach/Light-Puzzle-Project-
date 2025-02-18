
using StarterAssets;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Pickup_Controller : MonoBehaviour
{
    public float _rayLength = 0.5f;
    public GameObject _rayStartPos;
    [SerializeField] private GameObject _playerCameraRoot;
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshPro _textToAppear;
    private RaycastHit _hitInfo;
    public LayerMask _pickableItemLayer;
    private Outline _currentOutLine;
    public bool _pickedUpGameObject;
    private GameObject _objectThatGotPickedUp;
    [SerializeField] private CinemachineCamera _pickupCam;
    private ThirdPersonController _playerContoller;
    private float _defaultCameraTopClamp;
    private float _defaultCameraBottomClamp;


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _textToAppear = gameObject.transform.GetComponentInChildren<TextMeshPro>();
        _playerContoller = gameObject.GetComponentInParent<ThirdPersonController>();
        _defaultCameraTopClamp = _playerContoller.TopClamp;
        _defaultCameraBottomClamp = _playerContoller.BottomClamp;

    }
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_rayStartPos.transform.position, _rayStartPos.transform.forward * _rayLength, Color.red);

        CheckingForPickups();
        DropPickup();
        //MouseChangeRotForPlayer();
    }


    private void CheckingForPickups()
    {
        // check with raycast for layer "pickableObject & checking if he already picked up an object 
        if (Physics.Raycast(_rayStartPos.transform.position, _rayStartPos.transform.forward, out _hitInfo, _rayLength, _pickableItemLayer) && _pickedUpGameObject == false)
        {

            Debug.Log("can pick up");
            _currentOutLine = _hitInfo.collider.GetComponent<Outline>();
            _textToAppear.enabled = true;
            if (_currentOutLine != null)
            {
                // if in the raycast exists a value with a "Outline" Script - than turn it on 
                _currentOutLine.Highlight();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //if you are looking at an object and pressing E - than you clear the highlight and make the parent of the the object to the 
                //game object 
                _currentOutLine.ClearHighlight();
                _textToAppear.enabled = false;
                _objectThatGotPickedUp = _hitInfo.transform.gameObject;
                _objectThatGotPickedUp.transform.parent = gameObject.transform;
                _objectThatGotPickedUp.transform.position = gameObject.transform.position;
                _objectThatGotPickedUp.transform.rotation = gameObject.transform.rotation;
                _objectThatGotPickedUp.GetComponent<Rigidbody>().isKinematic = true;
                _pickupCam.enabled = true;
                _playerContoller.TopClamp = 0;
                _playerContoller.BottomClamp = 0;
                _playerContoller.CinemachineCameraTarget = _player;

                _pickedUpGameObject = true;

            }

        }
        else
        {
            //if you are not looking at any object that can be picked up than turn off the outline
            if (_currentOutLine != null)
            {
                _currentOutLine.ClearHighlight();
                _currentOutLine = null;
                _textToAppear.enabled = false;
            }
        }
    }

    private void DropPickup()
    {
        if (_pickedUpGameObject && Input.GetKeyDown(KeyCode.F))
        {
            _objectThatGotPickedUp.GetComponent<Rigidbody>().isKinematic = false;
            _objectThatGotPickedUp.transform.SetParent(null);
            _objectThatGotPickedUp = null;
            _pickedUpGameObject = false;
            _pickupCam.enabled = false;
            _playerContoller.TopClamp = _defaultCameraTopClamp;
            _playerContoller.BottomClamp = _defaultCameraBottomClamp;
            _playerContoller.CinemachineCameraTarget = _playerCameraRoot;
        }
    }

    public void DropPickupInFrontOfWall(Vector3 dropPoint)
    {

        if (_pickedUpGameObject)
        {
            _objectThatGotPickedUp.GetComponent<Rigidbody>().isKinematic = false;
            _objectThatGotPickedUp.transform.SetParent(null);
            _objectThatGotPickedUp.transform.position = dropPoint;
            _objectThatGotPickedUp = null;
            _pickedUpGameObject = false;
            _pickupCam.enabled = false;
            _playerContoller.TopClamp = _defaultCameraTopClamp;
            _playerContoller.BottomClamp = _defaultCameraBottomClamp;
            _playerContoller.CinemachineCameraTarget = _playerCameraRoot;
        }

    }



}
