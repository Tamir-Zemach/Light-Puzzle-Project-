using TMPro;
using UnityEngine;

public class CheckForPickables : MonoBehaviour
{
    public GameObject _rayStartPos;
    public float _rayLength = 0.9f;
    public RaycastHit _hitInfo;
    public LayerMask _pickableItemLayer;
    public bool _isHoldingObj;
    private OutlineHandler _outlineHandler;
    [SerializeField] private TextMeshPro _textToAppear;
    private Player_Pickup_Controller _pickupController;

    //TODO: add an ignore layer for invisible colliders

    private void Awake()
    {
        _pickupController = gameObject.GetComponent<Player_Pickup_Controller>();
        _outlineHandler = gameObject.GetComponent<OutlineHandler>();
        _textToAppear = gameObject.transform.GetComponentInChildren<TextMeshPro>();
    }
    private void Update()
    {
        Debug.DrawRay(_rayStartPos.transform.position, _rayStartPos.transform.forward * _rayLength, Color.red);
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
        return Physics.Raycast(
            _rayStartPos.transform.position,
            _rayStartPos.transform.forward,
            out _hitInfo,
            _rayLength,
            _pickableItemLayer
        );
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
            _isHoldingObj = false;
        }
    }

}
