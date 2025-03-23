using System.Collections;
using Character;
using Enemy;
using TMPro;
using UnityEngine;

namespace Units.Skills
{
    public class TimerHandler : MonoBehaviour
    {
        [SerializeField] private Zombie _prefabEnemy;

        private int _timerEmoSrill = 15;
        private TMP_Text _timer;

        public void StartTimerEmoSkill(Unit enemyFriend, Player character)
        {
            _timer = enemyFriend.FXUnit.StartTimer();
            StartCoroutine(TimerEmoSkill(enemyFriend, character));
        }

        private IEnumerator TimerEmoSkill(Unit enemyFriend, Player character)
        {
            var delay = new WaitForSeconds(1);

            while (_timerEmoSrill > 0)
            {
                _timer.text = _timerEmoSrill.ToString();

                yield return delay;

                _timerEmoSrill--;
            }

            Zombie enemy = Instantiate(_prefabEnemy, enemyFriend.transform.position, enemyFriend.transform.rotation);
            enemy.SetValue(enemyFriend.ValueHealth, enemyFriend.MaxValueHealth);
            enemy.ZombieSearch.InitializeStartTarget(character);

            Destroy(enemyFriend.gameObject);
            Destroy(gameObject);
        }
    }
}