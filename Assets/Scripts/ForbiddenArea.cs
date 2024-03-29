using UnityEngine;
using UnityEngine.Events;

public class ForbiddenArea : MonoBehaviour
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
    
    private void OnTriggerEnter(Collider collision)
    {
        var thief = collision.TryGetComponent<FirstPersonController>(out FirstPersonController firstPersonController);

        if (thief == true)
        {
            _disturbed?.Invoke();
        }
    }
    
    private void OnTriggerExit(Collider collision)
    {
        var thief = collision.TryGetComponent<FirstPersonController>(out FirstPersonController firstPersonController);

        if (thief == true)
        {
            _abandoned?.Invoke();
        }
    }
}