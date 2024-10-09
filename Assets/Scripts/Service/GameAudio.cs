using UnityEngine;
using System;

[Serializable]
public class GameAudio
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _onPhoneIsCharged;
    [SerializeField] private AudioClip _onPlugInSocket;

    public void OnPhoneIsCharged() => _audioSource.PlayOneShot(_onPhoneIsCharged);
    public void OnPlugInSocket() => _audioSource.PlayOneShot(_onPlugInSocket);
}