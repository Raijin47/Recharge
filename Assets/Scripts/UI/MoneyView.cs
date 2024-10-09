using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Animator _animator;
    private float _current;

    private readonly float Speed = 5;
    private Coroutine _changeProcess;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Game.Wallet.OnAddMoney += PlayAnim;
        Game.Wallet.OnSpendMoney += Spend;
        Set(Game.Wallet.Money);
    }

    private void Set(float value)
    {
        _current = value;
        _text.text = $"{Mathf.RoundToInt(_current)}";
    }

    private void PlayAnim() => _animator.Play("Enter");

    public void Add()
    {
        ReleaseCoroutine();
        _changeProcess = StartCoroutine(AddMoneyProcess());
    }

    private void Spend()
    {
        ReleaseCoroutine();
        _changeProcess = StartCoroutine(SpendMoneyProcess());
    }

    private IEnumerator AddMoneyProcess()
    {
        while (_current < Game.Wallet.Money)
        {
            Set(Mathf.Lerp(_current, Game.Wallet.Money, Time.deltaTime * Speed));
            yield return null;
        }

        Set(Game.Wallet.Money);
    }

    private IEnumerator SpendMoneyProcess()
    {
        while (_current > Game.Wallet.Money)
        {
            Set(Mathf.Lerp(_current, Game.Wallet.Money, Time.deltaTime * Speed));
            yield return null;
        }

        Set(Game.Wallet.Money);
    }

    private void ReleaseCoroutine()
    {
        if(_changeProcess != null)
        {
            StopCoroutine(_changeProcess);
            _changeProcess = null;
        }
    }
}