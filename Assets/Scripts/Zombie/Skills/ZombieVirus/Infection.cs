using UnityEngine;

public class Infection : MonoBehaviour
{
    [SerializeField] private ZombieVirus _zombieVirus;

    private void OnDestroy()
    {
        Instantiate(_zombieVirus, transform.position, Quaternion.identity);
    }
}