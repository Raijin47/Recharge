using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Base
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private bool _isCoinCollect;

    private readonly List<Vector3> Points = new();
    private readonly float Distance = 100f;
    private Coroutine _updateProcessCoroutine;
    private Vector3 _origin;
    private Vector3 _direction;
    private SolarPanel _panel;

    public override bool IsCharging
    {
        get => _isCharging;
        set
        {
            Debug.Log(value);

            _isCharging = value;

            ReleaseCoroutine();

            if (value)
            {
                _particle.Play();
                _updateProcessCoroutine = _isCoinCollect ? 
                    StartCoroutine(CoinCollect()) : 
                    StartCoroutine(UpdateProcessCoroutine());
            }
            else
            {
                if(_panel != null)
                {
                    _panel.IsCharging = false;
                    _panel = null;
                }

                _particle.Stop();
                _lineRenderer.positionCount = 0;
            }
        }
    }

    private IEnumerator UpdateProcessCoroutine()
    {
        while (true)
        {
            _origin = _startPoint.position;
            _direction = _startPoint.forward;
            Points.Clear();
            Points.Add(_origin);

            bool isHit = false;

            while (Physics.Raycast(_origin, _direction, out RaycastHit hit, Distance, _layer))
            {
                _origin = hit.point;
                _direction = Vector3.Reflect(_direction, hit.normal);
                Points.Add(_origin);
                if(hit.collider.TryGetComponent(out SolarPanel panel))
                {
                    isHit = true;
                    _panel = panel;
                }
            }

            if (isHit)
            {
                if (!_panel.IsCharging)
                    _panel.IsCharging = true;
            }       
            else
            {
                Points.Add(_origin + _direction * Distance);
                if (_panel != null)
                {
                    _panel.IsCharging = false;
                    _panel = null;
                }
            }

            _lineRenderer.positionCount = Points.Count;

            for (int i = 0; i < Points.Count; i++)            
                _lineRenderer.SetPosition(i, Points[i]);
        

            yield return null;
        }
    }

    private IEnumerator CoinCollect()
    {
        while (true)
        {
            _origin = _startPoint.position;
            _direction = _startPoint.forward;
            Points.Clear();
            Points.Add(_origin);

            while (Physics.Raycast(_origin, _direction, out RaycastHit hit, Distance, _layer))
            {
                _origin = hit.point;
                _direction = Vector3.Reflect(_direction, hit.normal);
                Points.Add(_origin);
                if (hit.collider.TryGetComponent(out Coin coin))                
                    coin.IsCharging = true;
            }

            Points.Add(_origin + _direction * Distance);

            _lineRenderer.positionCount = Points.Count;

            for (int i = 0; i < Points.Count; i++)
                _lineRenderer.SetPosition(i, Points[i]);

            yield return null;
        }
    }

    private void ReleaseCoroutine()
    {
        if (_updateProcessCoroutine != null)
        {
            StopCoroutine(_updateProcessCoroutine);
            _updateProcessCoroutine = null;
        }
    }
}