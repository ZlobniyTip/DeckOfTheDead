using UnityEngine;

namespace Enemy.Skills.Virus
{
    public class Infection : MonoBehaviour
    {
        [SerializeField] private ZombieVirus _zombieVirus;

        Zombie _zombie;

        private void Awake()
        {
            _zombie = GetComponent<Zombie>();
        }
        private void OnDestroy()
        {
            Instantiate(_zombieVirus, transform.position, Quaternion.identity);
        }
    }
}