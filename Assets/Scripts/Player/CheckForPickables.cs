using TMPro;
using UnityEngine;

public class CheckForPickables : MonoBehaviour
{
    public GameObject _rayStartPos;
    public float _rayLength = 0.9f;
    public RaycastHit _hitInfo;
    public LayerMask _pickableItemLayer;
    public LayerMask ignoreRaycastLayer;
    public bool _isHoldingObj;
    private OutlineHandler _outlineHandler;
    [SerializeField] private TextMeshPro _textToAppear;
    private Player_Pickup_Controller _pickupController;
    private Camera _camera;
    private Vector3 _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);



    //TODO: add an ignore layer for invisible colliders

    private void Awake()
    {
        _camera = Camera.main;
        _pickupController = gameObject.GetComponent<Player_Pickup_Controller>();
        _outlineHandler = gameObject.GetComponent<OutlineHandler>();
        _textToAppear = gameObject.transform.GetComponentInChildren<TextMeshPro>();
    }
    private void Update()
    {
        CheckingForPickupsWithRay();
        HandleDropPickup();
    }
    public void CheckingForPickupsWithRay()
    {
        if (IsObjectPickable() && !_isHoldingObj)
        {
            _outlineHandler.ShowPickupVisibleHint();
            _outlineHandler._currentOutLine = _hitInfo.collider.GetComponent<Outline>();
            _textToAppear.enabled = true;
            HandlePickup();
        }
        else
        {
            _outlineHandler.ResetHighlight();
            _textToAppear.enabled = false;
        }
    }
    public bool IsObjectPickable()
    {

        Ray ray = _camera.ScreenPointToRay(_screenCenter);
        Debug.DrawRay(ray.origin, ray.direction * _rayLength, Color.red);
        if (Physics.Raycast(ray, out _hitInfo, _rayLength, ~ignoreRaycastLayer))
        {
            if (_hitInfo.transform.GetComponent<LightScript>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }
    public void HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _pickupController.Pickup();
            _outlineHandler.ResetHighlight();
            _isHoldingObj = true;
        }

    }
    private void HandleDropPickup()
    {
        if (_isHoldingObj && Input.GetKeyDown(KeyCode.F))
        {
            _pickupController.DropPickup();
            _outlineHandler.ResetHighlight();
            _isHoldingObj = false;
        }
    }

}