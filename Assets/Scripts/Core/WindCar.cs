using UnityEngine;

public class WindCar : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _targetPoint;

    [SerializeField] private float _movementSpeed;

    public void Push()
    {
        if(Vector3.Distance(_targetPoint.position, transform.position) > 0.2f)
        {
            _rigidbody.velocity = Time.fixedDeltaTime * _movementSpeed * (_targetPoint.position - transform.position);
        }
    }
}