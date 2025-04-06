using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _defaultPlayerColliderCenter;
    private float _defaultColliderRadius;
    [SerializeField] private float _colliderRadius;
    [SerializeField] private float _xValue;
    [SerializeField] private float _yValue;
    [SerializeField] private float _zValue;


    //TODO: check what is the bug when i pick up a lamp

    private void Awake()
    {
        _characterController = GetComponentInParent<CharacterController>();
        _defaultPlayerColliderCenter = _characterController.center;
        _defaultColliderRadius = _characterController.radius;
    }
    public void GrowPlayerCollider()
    {
        _characterController.center = new Vector3(_xValue, _yValue, _zValue);
        _characterController.radius = _colliderRadius;
    }
    public void ResetPlayerCollider()
    {
        _characterController.center = _defaultPlayerColliderCenter;
        _characterController.radius = _defaultColliderRadius;
    }

    

}
