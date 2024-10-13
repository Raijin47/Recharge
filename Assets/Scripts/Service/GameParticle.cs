using System;
using UnityEngine;

[Serializable]
public class GameParticle
{
    [SerializeField] private ParticleSystem _onPlugInSocket;

    public void OnPlugInSocket(Vector3 pos)
    {
        _onPlugInSocket.transform.position = pos;
        _onPlugInSocket.Play();
    }
}