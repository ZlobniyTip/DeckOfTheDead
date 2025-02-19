using UnityEngine;

namespace Enemy
{
    public class ZombieData : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private string _skillName;
        [SerializeField] private string _skillDescription;
        [SerializeField] private Sprite _image;

        public string Name => _name;
        public string SkillName => _skillName;
        public string SkillDescription => _skillDescription;
        public Sprite Image => _image;
    }
}