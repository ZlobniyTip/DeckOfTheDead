public interface ISkill
{
    public string Name { get; }
    public int Damage { get; }
    public float Cooldown { get; }

    public int UseSkill();
}