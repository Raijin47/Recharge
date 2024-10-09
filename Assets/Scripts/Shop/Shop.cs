using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private Image[] _buttonImages;
    [SerializeField] private Animator _animator;
    [SerializeField] private Material _material;
    [SerializeField] private GameObject _buttonBuyPin;
    [SerializeField] private GameObject _buttonBuyBackground;

    [Space(10)]
    [SerializeField] private Image[] _pinFrame;
    [SerializeField] private ButtonBase[] _pinButtons;
    [SerializeField] private GameObject[] _enablePin;
    [SerializeField] private GameObject[] _disablePin;

    [Space(10)]
    [SerializeField] private Image[] _backgroundFrame;
    [SerializeField] private ButtonBase[] _backgroundButtons;
    [SerializeField] private Image[] _enableBackground;
    [SerializeField] private GameObject[] _disableBackground;

    [SerializeField] private ButtonBase[] _deviceButtons;

    [Space(10)]
    [SerializeField] private Color _disablePanelColor;
    [SerializeField] private Color _enableFrameColor;
    [SerializeField] private Color _disableFrameColor;


    private readonly List<int> Num = new();
    private Coroutine _coroutine;
    public void Init()
    {
        for(int i = 0; i < _pinButtons.Length; i++)
        {
            _pinButtons[i].Interactable = YandexGame.savesData.IsPinPurchased[i];
            _enablePin[i].SetActive(YandexGame.savesData.IsPinPurchased[i]);
            _disablePin[i].SetActive(!YandexGame.savesData.IsPinPurchased[i]);
            _pinFrame[i].color = YandexGame.savesData.Pin == i ? _enableFrameColor : _disableFrameColor;
        }

        for(int i = 0; i < _backgroundButtons.Length; i++)
        {
            _backgroundButtons[i].Interactable = YandexGame.savesData.IsBackgroundPurchased[i];
            _enableBackground[i].gameObject.SetActive(YandexGame.savesData.IsBackgroundPurchased[i]);
            _disableBackground[i].SetActive(!YandexGame.savesData.IsBackgroundPurchased[i]);
            _backgroundFrame[i].color = YandexGame.savesData.Background == i ? _enableFrameColor : _disableFrameColor;
        }

        _buttonBuyPin.SetActive(IsBuyPin());
        _buttonBuyBackground.SetActive(IsBuyBackground());
        _material.mainTexture = _enableBackground[YandexGame.savesData.Background].mainTexture;
    }

    private void OnEnable() => EnterPanel(0);

    private bool IsBuyPin()
    {
        for (int i = 0; i < _pinButtons.Length; i++)
            if (!YandexGame.savesData.IsPinPurchased[i])
                return true;

        return false;
    }

    private bool IsBuyBackground()
    {
        for (int i = 0; i < _backgroundButtons.Length; i++)
            if (!YandexGame.savesData.IsBackgroundPurchased[i])
                return true;

        return false;
    }

    public void EnterPanel(int id)
    {
        for(int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetActive(i == id);
            _buttonImages[i].color = i == id ? Color.white : _disablePanelColor;
        }
    }

    public void SetPin(int id)
    {
        if (YandexGame.savesData.Pin == id) return;

        _pinFrame[YandexGame.savesData.Pin].color = _disableFrameColor;
        _pinFrame[id].color = _enableFrameColor;
        Game.Pin.SetPin(id);
        _animator.Play("Enter");
    }

    public void SetBackground(int id)
    {
        if (YandexGame.savesData.Background == id) return;

        _backgroundFrame[YandexGame.savesData.Background].color = _disableFrameColor;
        _backgroundFrame[id].color = _enableFrameColor;
        _material.mainTexture = _enableBackground[id].mainTexture;

        YandexGame.savesData.Background = id;
        YandexGame.SaveProgress();
    }

    public void BuyPin()
    {
        ReleaseCoroutine();
        _coroutine = StartCoroutine(RandomizePinCoroutine());       
    }

    public void BuyBackground()
    {
        ReleaseCoroutine();
        _coroutine = StartCoroutine(RandomizeBackgroundCoroutine());
    }

    private void ReleaseCoroutine()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator RandomizePinCoroutine()
    {
        _pinFrame[YandexGame.savesData.Pin].color = _disableFrameColor;

        for (int i = 0; i < _pinFrame.Length; i++)
            if (!YandexGame.savesData.IsPinPurchased[i])
                Num.Add(i);

        int r = Random.Range(Num.Count * 3, Num.Count * 5);
        int current = 0;

        for (int i = 0; i < r; i++)
        {
            current = i;
            while(current >= Num.Count)
                current -= Num.Count;

            _pinFrame[Num[current]].color = _enableFrameColor;
            yield return new WaitForSeconds(0.05f);
            _pinFrame[Num[current]].color = _disableFrameColor;
        }

        current = Num[current];

        YandexGame.savesData.IsPinPurchased[current] = true;
        SetPin(current);

        _pinButtons[current].Interactable = true;
        _disablePin[current].SetActive(false);
        _enablePin[current].SetActive(true);

        _buttonBuyPin.SetActive(IsBuyPin());
        Num.Clear();
    }

    private IEnumerator RandomizeBackgroundCoroutine()
    {
        _backgroundFrame[YandexGame.savesData.Pin].color = _disableFrameColor;

        for (int i = 0; i < _backgroundFrame.Length; i++)
            if (!YandexGame.savesData.IsBackgroundPurchased[i])
                Num.Add(i);

        int r = Random.Range(Num.Count * 3, Num.Count * 5);
        int current = 0;

        for (int i = 0; i < r; i++)
        {
            current = i;
            while (current >= Num.Count)
                current -= Num.Count;

            _backgroundFrame[Num[current]].color = _enableFrameColor;
            yield return new WaitForSeconds(0.05f);
            _backgroundFrame[Num[current]].color = _disableFrameColor;
        }

        current = Num[current];

        YandexGame.savesData.IsBackgroundPurchased[current] = true;
        SetBackground(current);

        _backgroundButtons[current].Interactable = true;
        _disableBackground[current].SetActive(false);
        _enableBackground[current].gameObject.SetActive(true);

        _buttonBuyBackground.SetActive(IsBuyBackground());
        Num.Clear();
    }
}