using UnityEngine;
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
        TransferZombies(_zombie);
    }

    public Zombie TransferZombies(Zombie zombie)
    {
        return zombie;
    }
}