using TMPro;
using UnityEngine;

public class CheckForPickables : MonoBehaviour
{
    public GameObject _rayStartPos;
    public float _rayLength = 0.9f;
    public RaycastHit _hitInfo;
    public LayerMask _pickableItemLayer;
    private bool _pickedUpGameObject;
    private OutlineHandler _outlineHandler;
    [SerializeField] private TextMeshPro _textToAppear;
    private Player_Pickup_Controller _pickupController;
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
    }
    public void CheckingForPickupsWithRay()
    {
        if (IsObjectPickable())
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
        ) && !_pickedUpGameObject;
    }   
    public void HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _pickupController.Pickup();
            _outlineHandler.ResetHighlight();
        }
    }


}
