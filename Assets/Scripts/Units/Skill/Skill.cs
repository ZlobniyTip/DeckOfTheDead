using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private string _name;

    public virtual void UseSkill()
    {
    }
}