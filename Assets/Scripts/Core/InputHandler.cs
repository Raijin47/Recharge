using System.Collections;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _movementSpeed;

    [SerializeField] private LayerMask _layerMask;

    private const float _rayDistance = 100;

    private Plug _plug;
    private Camera _camera;
    private Coroutine _dragProcessCoroutine;

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
        ReleaseCoroutine();
        _dragProcessCoroutine = StartCoroutine(DragProcessCoroutine());
    }

    private IEnumerator DragProcessCoroutine()
    {
        while (true)
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, _rayDistance, _layerMask))
            {
                _plug.Rigidbody.Move((hit.point + _plug.Offset), Quaternion.Euler(_plug.Rotation));
            }
            yield return null;
        }
    }

    private void EndDrag()
    {
        ReleaseCoroutine();

        _plug.Connect();
        _plug = null;
    }

    private void ReleaseCoroutine()
    {
        if (_dragProcessCoroutine == null) return;
        StopCoroutine(_dragProcessCoroutine);
        _dragProcessCoroutine = null;
    }
}