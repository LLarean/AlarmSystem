using UnityEngine;

public class Signalization : MonoBehaviour
{
    [SerializeField] private AudioSource _loudspeaker;
    [SerializeField] private ForbiddenArea _forbiddenArea;
    
    [Space]
    [SerializeField] private float _maximumVolume = 1f;
    [SerializeField] private float _minimumVolume = 0f;
    [SerializeField] private float _fadeSpeed = .1f;
    
    private float _targetVolume = 0f;

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

    private void Update()
    {
        if (_loudspeaker.volume == _targetVolume)
        {
            return;
        }

        bool isPlaying = true;
        
        if (_loudspeaker.isPlaying != isPlaying && _targetVolume == _maximumVolume)
        {
            _loudspeaker.Play();
        }
        
        _loudspeaker.volume = Mathf.MoveTowards(_loudspeaker.volume, _targetVolume, _fadeSpeed * Time.deltaTime);

        if (_loudspeaker.isPlaying == isPlaying && _loudspeaker.volume == _minimumVolume)
        {
            _loudspeaker.Stop();
        }
    }

    private void StartSiren()
    {
        _targetVolume = _maximumVolume;
    }

    private void StopSiren()
    {
        _targetVolume = _minimumVolume;
    }
}