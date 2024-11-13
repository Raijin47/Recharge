using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _offsetY = 2.5f;

    [SerializeField] private LayerMask _layerMask;

    private const float _rayDistance = 100;

    private Plug _plug;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        Game.Action.OnPlagDown += StartDrag;
        Game.Action.OnPlagUp += EndDrag;
    }

    private void StartDrag(Plug plug)
    {
        _plug = plug;

        _plug.transform.rotation = Quaternion.Euler(_plug.Rotation);
        _plug.transform.position = new Vector3(_plug.transform.position.x, _offsetY, _plug.transform.position.z);

        _plug.Rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        _plug.Rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if(_plug != null)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _rayDistance, _layerMask))
            {
                Vector3 point = Vector3.Lerp(_plug.TAnchor.position, hit.point, _plug.Distance / Vector3.Distance(_plug.TAnchor.position, hit.point));
                Vector3 direction = point + _plug.Offset - _plug.transform.position;

                float multipy = Vector3.Distance(point + _plug.Offset, _plug.transform.position);

                _plug.Rigidbody.velocity = multipy * _movementSpeed * Time.fixedDeltaTime * direction;
            }
        }
    }

    private void EndDrag()
    {
        _plug.Rigidbody.constraints = RigidbodyConstraints.None;
        _plug.Connect();
        _plug = null;
    }
}