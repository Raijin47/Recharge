using UnityEngine;

public abstract class Base : MonoBehaviour
{
    [SerializeField] protected Transform _anchror;
    protected bool _isCharging;
    public abstract bool IsCharging { get; set; }
    public Transform Anchor => _anchror;
}