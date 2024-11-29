using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;

    public abstract void UseSkill();
}