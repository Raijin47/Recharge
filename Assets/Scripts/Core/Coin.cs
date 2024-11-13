using UnityEngine;

public class Coin : Device
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Collider _collider;

    private const float _speedRotate = 100f;

    protected override void Charge(bool value)
    {
        if (value)
        {
            IsComplated = value;
            _object.SetActive(false);
            _collider.enabled = false;
        }
    }

    private void Update()
    {
        transform.Rotate(_speedRotate * Time.deltaTime * Vector3.down);
    }
}