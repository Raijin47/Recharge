using System;
using UnityEngine;

[Serializable]
public class GameParticle
{
    [SerializeField] private ParticleSystem _onPlugInSocket;
    [SerializeField] private ParticleSystem _onWinStageL, _onWinStageR;

    public void OnPlugInSocket(Vector3 pos)
    {
        _onPlugInSocket.transform.position = pos;
        _onPlugInSocket.Play();
    }

    public void OnWinStage()
    {
        _onWinStageL.Play();
        _onWinStageR.Play();
    }
}