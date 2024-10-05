using System;
using UnityEngine;

[CreateAssetMenu(fileName = "stageData", menuName = "data/stage", order = 51)]
public class StageData : ScriptableObject
{
    [SerializeField] private AdapterData[] _adapters;
    [SerializeField] private PlugData[] _plugs;

    public AdapterData[] Adapter => _adapters;
    public PlugData[] Plug => _plugs;
}

[Serializable]
public class PhoneData
{
    public Phone Phone;
    public Vector3 StartPos;
    public Vector3 StartRot;
}

[Serializable]
public class AdapterData
{
    public Adapter Adapter;
    public Vector3 StartPos;
    public Vector3 StartRot;
}

[Serializable]
public class PlugData
{
    public Plug Plug;
    public Vector3 StartPos;
    public Vector3 StartRot;
    public PhoneData Phone;
    public AdapterData Adapter;
}