using UnityEngine;
using System.Collections;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _loudspeaker;
    [SerializeField] private ProtectionCircuit _protectionCircuit;
    [SerializeField] private float maxVolume = 1.0f;
    [SerializeField] private float fadeSpeed = 1.0f;

    private IEnumerator _fadeInSound;
    private IEnumerator _fadeOutSound;

    private void Awake()
    {
        _protectionCircuit.Disturbed.AddListener(StartSiren);
        _protectionCircuit.Abandoned.AddListener(StopSiren);
    }

    private void OnDestroy()
    {
        _protectionCircuit.Disturbed.RemoveListener(StartSiren);
        _protectionCircuit.Abandoned.RemoveListener(StopSiren);
    }

    private void StartSiren()
    {
        if (_fadeOutSound != null)
        {
            StopCoroutine(_fadeOutSound);
        }

        _fadeInSound = FadeInSound();
        StartCoroutine(_fadeInSound);
    }

    private void StopSiren()
    {
        if (_fadeInSound != null)
        {
            StopCoroutine(_fadeInSound);
        }

        _fadeOutSound = FadeOutSound();
        StartCoroutine(_fadeOutSound);
    }

    private IEnumerator FadeInSound()
    {
        _loudspeaker.Play();

        while (_loudspeaker.volume < maxVolume)
        {
            _loudspeaker.volume += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        _loudspeaker.volume = maxVolume;
    }

    private IEnumerator FadeOutSound()
    {
        while (_loudspeaker.volume > 0)
        {
            _loudspeaker.volume -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        _loudspeaker.volume = 0;
        _loudspeaker.Stop();
    }
}