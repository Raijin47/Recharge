using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial1 : MonoBehaviour
{
    [SerializeField] private Transform _target1, _target2;
    [SerializeField] private RectTransform _hand;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;
    [SerializeField] private RectTransform _helper;
    [SerializeField] private float _speed;
    [SerializeField] private CanvasGroup _canvas;

    private readonly WaitForSeconds Interval = new(0.3f);
    private readonly WaitForSeconds Dalay = new(1f);
    private Camera _camera;
    private Coroutine _coroutine;

    private void Awake() => _camera = Camera.main;
    private void OnEnable() 
    {
        _coroutine = StartCoroutine(TutorialUpdate());

        Game.Level.OnStageComplated.AddListener(() => {

            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            _canvas.alpha = 0;
        });
    }

    private IEnumerator TutorialUpdate()
    {
        while(true)
        {
            _image.sprite = _spriteOff;
            _hand.position = _camera.WorldToScreenPoint(_target1.position);
            _canvas.alpha = 1;

            yield return Interval;
            _image.sprite = _spriteOn;
            yield return Interval;

            _helper.position = _camera.WorldToScreenPoint(_target2.position);

            while (_hand.anchoredPosition != _helper.anchoredPosition)
            {
                yield return null;
                _hand.anchoredPosition = Vector2.MoveTowards(_hand.anchoredPosition, _helper.anchoredPosition, _speed * Time.deltaTime);
            }

            yield return Interval;
            _image.sprite = _spriteOff;
            yield return Interval;

            while (_canvas.alpha != 0)
            {
                _canvas.alpha -= Time.deltaTime * 0.2f;
            }

            yield return Dalay;
        }
    }
}