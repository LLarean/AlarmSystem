using UnityEngine;
using System.Collections;

public class Signalization : MonoBehaviour
{
    [SerializeField] private AudioSource _loudspeaker;
    [SerializeField] private ForbiddenArea _forbiddenArea;
    
    [Space]
    [SerializeField] private float _maximumVolume = 1f;
    [SerializeField] private float _minimumVolume = 0f;
    [SerializeField] private float _fadeSpeed = .1f;
    
    private IEnumerator _coroutine;

    private void Awake()
    {
        _forbiddenArea.Disturbed.AddListener(delegate { FadeVolume(_maximumVolume); });
        _forbiddenArea.Abandoned.AddListener(delegate { FadeVolume(_minimumVolume); });
    }

    private void OnDestroy()
    {
        _forbiddenArea.Disturbed.RemoveListener(delegate { FadeVolume(_maximumVolume); });
        _forbiddenArea.Abandoned.RemoveListener(delegate { FadeVolume(_minimumVolume); });
    }

    private void FadeVolume(float targetVolume)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
     
        _coroutine = ChangeVolume(targetVolume);
        StartCoroutine(_coroutine);
    }
    
    private IEnumerator ChangeVolume(float targetVolume)
    {
        while(_loudspeaker.volume != targetVolume)
        {
            _loudspeaker.volume = Mathf.MoveTowards(_loudspeaker.volume, targetVolume, _fadeSpeed * Time.deltaTime);
            yield return Time.deltaTime;
        }

        _coroutine = null;
    }
}