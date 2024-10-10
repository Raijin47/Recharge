using UnityEngine;

public class Plug : Base
{
    [SerializeField] private Pin _pin;
    [SerializeField] private Base _target;
    [SerializeField] private LayerMask _soketLayer;

    [Space(10)]
    [SerializeField] private Vector3 _offsetPin;
    [SerializeField] private Vector3 _sizePin;
    [SerializeField] private float _castRadius;

    [Space(10)]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Vector3 _connectedRotation;

    private Collider[] _result;
    public Base Target { get=> _target; set => _target = value; }

    public Vector3 Offset => _offset;
    public Vector3 Rotation => _rotation;

    public Rigidbody Rigidbody { get; private set; }
    [SerializeField]private Soket _soket;

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

    private void OnMouseDown() 
    {
        Game.Action.SendPlagDown(this);
        IsCharging = false;
        Rigidbody.isKinematic = false;
        if(_soket != null)
        {
            _result = new Collider[4];
            Physics.OverlapBoxNonAlloc(_soket.transform.position + _offsetPin, _sizePin, _result, Quaternion.Euler(Vector3.zero), _soketLayer);
            foreach(Collider obj in _result)
            {
                if (obj == null) continue;
                if (obj.TryGetComponent(out Soket soket))
                {
                    soket.IsUse = false;
                }
            }

            _soket.Disconnect();
            _soket = null;
        }
    } 

    private void OnMouseUp() => Game.Action.SendPlagUp();

    public void Connect()
    {
        Collider[] sokets = Physics.OverlapSphere(transform.position, _castRadius, _soketLayer);

        foreach(Collider obj in sokets)
        {
            if (obj.TryGetComponent(out Soket soket))
            {
                if(soket.IsUse) continue;

                _result = new Collider[4];
                Physics.OverlapBoxNonAlloc(soket.transform.position + _offsetPin, _sizePin, _result, Quaternion.Euler(Vector3.zero), _soketLayer);

                if(IsFreeSoket())
                {
                    Rigidbody.isKinematic = true;
                    Rigidbody.position = soket.transform.position;
                    Rigidbody.rotation = Quaternion.Euler(_connectedRotation);

                    foreach (Collider freeobj in _result)
                    {
                        if (freeobj == null) continue;
                        if (freeobj.TryGetComponent(out Soket freesoket))
                        {
                            freesoket.IsUse = true;
                        }
                    }

                    _soket = soket;
                    _soket.Connect(this);

                    break;
                }
            }
        }
    }

    private bool IsFreeSoket()
    {
        foreach(Collider obj in _result)
        {
            if (obj == null) continue;
            if(obj.TryGetComponent(out Soket soket))
            {
                if (soket.IsUse) return false;
            }
        }

        return true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _castRadius);
    }
}