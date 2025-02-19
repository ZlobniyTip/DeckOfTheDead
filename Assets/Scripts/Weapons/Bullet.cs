using Enemy;
using UnityEngine;

namespace Weapons
{
    public class Bullet : MonoBehaviour
    {
        private float _force = 30;
        private float _timer = 2;

        private void Update()
        {
            _timer -= Time.deltaTime;
            transform.Translate(Vector3.forward * _force * Time.deltaTime);

            if (_timer <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Zombie>())
            {
                Destroy(gameObject);
            }
        }
    }
}