using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _loudspeaker;
    [SerializeField] private ProtectionCircuit _protectionCircuit;
    
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
        _loudspeaker.Play();
    }
    
    private void StopSiren()
    {
        _loudspeaker.Stop();
    }
}
