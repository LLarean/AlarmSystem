using UnityEngine;
using UnityEngine.Events;

public class ProtectionCircuit : MonoBehaviour
{
    private UnityEvent _disturbed = new UnityEvent();
    private UnityEvent _abandoned = new UnityEvent();
    
    public UnityEvent Disturbed
    {
        get => _disturbed;
        set => _disturbed = value;
    }    
    
    public UnityEvent Abandoned
    {
        get => _abandoned;
        set => _abandoned = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        _disturbed?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        _abandoned?.Invoke();
    }
}