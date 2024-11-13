using System.Collections.Generic;
using UnityEngine;

public class Plug : Base
{    
    [SerializeField] private float _distance;

    [Space(10)]
    [SerializeField] private Base _target;
    [SerializeField] protected Soket _soket;
    [SerializeField] protected LayerMask _soketLayer;

    [Space(10)]
    [SerializeField] private Vector3[] _blockPoints;
    [SerializeField] protected float _castRadius;

    [Space(10)]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] protected Vector3 _connectedRotation;


    private readonly List<Soket> Used = new();
    protected Collider[] _result;
    public Base Target { get=> _target; set => _target = value; }
    public Vector3 Offset => _offset;
    public Vector3 Rotation => _rotation;
    public Rigidbody Rigidbody { get; private set; }
    public Transform TAnchor => _target.Anchor;

    public float Distance => _distance;

    public override bool IsCharging 
    { 
        get => _isCharging;
        set 
        { 
            _isCharging = value;
            _target.IsCharging = _isCharging;
        }
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void OnMouseDown() 
    {
        IsCharging = false;
        Rigidbody.isKinematic = false;
        if(_soket != null)
        {
            for (int i = 0; i < _blockPoints.Length; i++)
            {
                Vector3 position = _soket.transform.position + _blockPoints[i];

                if (Physics.Linecast(position + Vector3.up * 2, position, out RaycastHit hit, _soketLayer))               
                    if (hit.collider.TryGetComponent(out Soket s))                   
                        s.IsUse = false;

                Used.Clear();
                Debug.DrawLine(position + Vector3.up * 2, position, Color.green, 10f);
            }

            _soket.Disconnect();
            _soket = null;
        }
        Game.Action.SendPlagDown(this);
    } 

    private void OnMouseUp() => Game.Action.SendPlagUp();

    public virtual void Connect()
    {
        Collider[] sokets = Physics.OverlapSphere(transform.position, _castRadius, _soketLayer);

        foreach(Collider obj in sokets)
        {
            if (obj.TryGetComponent(out Soket soket))
            {
                if(soket.IsUse) continue;

                for (int i = 0; i < _blockPoints.Length; i++)
                {
                    Vector3 position = soket.transform.position + _blockPoints[i];

                    if(Physics.Linecast(position + Vector3.up * 2, position, out RaycastHit hit, _soketLayer))                    
                        if (hit.collider.TryGetComponent(out Soket s))                        
                            Used.Add(s);
                        
                    
                    Debug.DrawLine(position + Vector3.up * 2, position, Color.red, 10f);
                }

                if (IsFreeSoket())
                {
                    Rigidbody.isKinematic = true;
                    transform.SetPositionAndRotation(soket.transform.position, Quaternion.Euler(_connectedRotation));

                    transform.SetParent(soket.transform);

                    foreach (Soket freeobj in Used)
                        freeobj.IsUse = true;

                    _soket = soket;
                    _soket.Connect(this);

                    break;
                }
            }
        }
    }

    protected bool IsFreeSoket()
    {
        foreach(Soket obj in Used)
            if (obj.IsUse) return false;

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _castRadius);

        if(_target != null)
        {
            Gizmos.color = new Color(1, 1, 0, 0.2f);
            Gizmos.DrawSphere(TAnchor.position, _distance);
        }
    }
}