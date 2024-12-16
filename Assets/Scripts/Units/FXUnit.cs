using UnityEngine;

public class FXUnit : MonoBehaviour
{
    [SerializeField] private ParticleSystem _recoveryHealth;

    private void Awake()
    {
        StopRecoveryHealth();
    }

    public void PlayRecoveryHealth()
    {
        if (!_recoveryHealth.isPlaying)
            _recoveryHealth.Play();
    } 

    public void StopRecoveryHealth()
    {
        if (_recoveryHealth.isPlaying)
            _recoveryHealth.Stop();
    } 
}
