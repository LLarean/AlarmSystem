using UnityEngine;
using System.Collections;

public class Signalization : MonoBehaviour
{
    [SerializeField] private AudioSource _loudspeaker;
    [SerializeField] private ForbiddenArea _forbiddenArea;
    
    [SerializeField] private float _maximumVolume = 1f;
    [SerializeField] private float _minimumVolume = 0f;
    [SerializeField] private float _fadeSpeed = 1f;

    private Coroutine _fadeSound;

    private void Awake()
    {
        _forbiddenArea.Disturbed.AddListener(StartSiren);
        _forbiddenArea.Abandoned.AddListener(StopSiren);
    }

    private void OnDestroy()
    {
        _forbiddenArea.Disturbed.RemoveListener(StartSiren);
        _forbiddenArea.Abandoned.RemoveListener(StopSiren);
    }

    private void StartSiren()
    {
        StopFadeSound();
        _fadeSound = StartCoroutine(FadeSound(_maximumVolume, _fadeSpeed));
    }

    private void StopSiren()
    {
        StopFadeSound();
        _fadeSound = StartCoroutine(FadeSound(_minimumVolume, -_fadeSpeed));
    }

    private void StopFadeSound()
    {
        if (_fadeSound != null)
        {
            StopCoroutine(_fadeSound);
        }
    }
    
    private IEnumerator FadeSound(float targetVolume, float fadeSpeed)
    {
        if (targetVolume == _maximumVolume && _loudspeaker.isPlaying == false)
        {
            _loudspeaker.Play();
        }

        while (_loudspeaker.volume != targetVolume)
        {
            _loudspeaker.volume += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        if (targetVolume == _minimumVolume)
        {
            _loudspeaker.Stop();
        }
        
        _fadeSound = null;
    }
}