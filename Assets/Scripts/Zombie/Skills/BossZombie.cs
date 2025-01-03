using UnityEngine;

[RequireComponent(typeof(Zombie))]
public class BossZombie : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _attackSpeed;

    private Zombie _zombie;

    private void Start()
    {
        _zombie = GetComponent<Zombie>();

        _zombie.SetValue(_health, _health);
        _zombie.ZombieAttack.BuffAttack(_attackSpeed, _damage);
    }
}