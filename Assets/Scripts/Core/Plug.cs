using System.Collections.Generic;
using UnityEngine;

public class Plug : Base
{
    [SerializeField] private Pin _pin;
    [SerializeField] private Base _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Vector3 _connectedRotation;

    public Vector3 Offset => _offset;
    public Vector3 Rotation => _rotation;

    public Rigidbody Rigidbody { get; private set; }
    private Soket _soket;

    public override bool IsCharging 
    { 
        get => _isCharging;
        set 
        { 
            _isCharging = value;
            _target.IsCharging = _isCharging;
        }
    }

    private readonly List<Soket> Sokets = new();

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown() 
    {
        _pin.Disconnect();
        Game.Action.SendPlagDown(this);
        IsCharging = false;
        Rigidbody.isKinematic = false;
        if(_soket != null)
        {
            _soket.Disconnect();
            _soket = null;
        }
    } 
    private void OnMouseUp() => Game.Action.SendPlagUp();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Soket soket))       
            if(!soket.IsUse) Sokets.Add(soket);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Soket soket))        
            Sokets.Remove(soket);
    }

    public void Connect()
    {
        if(Sokets.Count != 0)
        {
            Rigidbody.isKinematic = true;
            Rigidbody.position = Sokets[0].transform.position;
            Rigidbody.rotation = Quaternion.Euler(_connectedRotation);

            _soket = Sokets[0];
            _soket.Connect(this);
            _pin.Connect();
        }
    }
}