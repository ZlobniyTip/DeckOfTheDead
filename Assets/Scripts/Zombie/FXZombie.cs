using UnityEngine;

public class FXZombie : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effectCamp;

    private void Awake()
    {
        _effectCamp.Stop();
    }

    public void EnterCamp()
    {
        _effectCamp.Play();
    }

    public void ExitCamp()
    {
        _effectCamp.Stop();
    }
}
