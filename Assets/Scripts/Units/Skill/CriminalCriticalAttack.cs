using UnityEngine;

public class CriminalCriticalAttack : Skill
{
    [SerializeField] private Unit _unit;
    [SerializeField] private ParticleSystem _particle;

    private int _lethalCount = 1;
    private int _chance = 20;
    private int _multiplyDamage = 100;

    private void Start()
    {
        _unit.Attack.CurrentWeapon.Shooting += TryInflictLethalDamage;
    }

    private void OnDestroy()
    {
        _unit.Attack.CurrentWeapon.Shooting -= TryInflictLethalDamage;
    }

    public override void UseSkill()
    {
        Instantiate(_particle, _unit.Attack.CurrentWeapon.transform);
        _unit.Target.TakeDamage(_unit.Attack.CurrentWeapon.Damage * _multiplyDamage);
    }

    private void TryInflictLethalDamage()
    {
        int random = Random.Range(_lethalCount, _chance);

        if (random == _lethalCount)
            UseSkill();
    }
}
