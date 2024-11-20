using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponStatus _weaponStatus;

    [SerializeField] protected float _attackDistance;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _delayBetweenShots;

    protected AudioSource _audio;

    public float DelayBetweenShots => _delayBetweenShots;
    public WeaponStatus WeaponStatus => _weaponStatus;
    public float AttackDistance => _attackDistance;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public virtual int Shooting()
    {
        _audio.Play();
        return _damage;
    }
}