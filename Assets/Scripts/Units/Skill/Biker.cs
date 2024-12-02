using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : Skill
{
    [SerializeField] private Unit _unit;

    private int _impactCounter;
    private int _criticalAttackCounter = 5;
    private int _criticalAttack = 2;

    private void Start()
    {
        //_unit.CurrentWeapon.Shooting += CountStrokes;
    }

    private void OnDestroy()
    {
        //_unit.CurrentWeapon.Shooting -= CountStrokes;
    }

    public override void UseSkill()
    {
        //_unit.CurrentWeapon.BuffMultiplyDamage(_criticalAttack);
    }

    private void CountStrokes()
    {
        _impactCounter++;

        if (_impactCounter == _criticalAttackCounter)
        {
            UseSkill();
        }
    }
}