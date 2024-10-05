using System;
using UnityEngine;
using YG;

[Serializable]
public class PinData
{
    public event Action OnSwapPin;

    [SerializeField] private Mesh[] _holeMeshes;
    [SerializeField] private Mesh[] _pinMeshes;
    [SerializeField] private Vector3[] _pinOffset;

    public Vector3 PinOffset => _pinOffset[Pin];
    public Mesh CurrentHole => _holeMeshes[Pin];
    public Mesh CurrentPin => _pinMeshes[Pin];

    private int Pin
    {
        get => YandexGame.savesData.Pin;
        set
        {
            YandexGame.savesData.Pin = value;
            YandexGame.SaveProgress();
            OnSwapPin?.Invoke();
        }
    }

    public void SetPin(int id) => Pin = id;
}