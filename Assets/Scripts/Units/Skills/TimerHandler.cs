using Character;
using Enemy;
using System.Collections;
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
            while (_timerEmoSrill > 0)
            {
                _timer.text = _timerEmoSrill.ToString();
                yield return new WaitForSeconds(1);
                _timerEmoSrill--;
            }

            Zombie enemy = Instantiate(_prefabEnemy, enemyFriend.transform.position, enemyFriend.transform.rotation);
            enemy.SetValue(enemyFriend.Value, enemyFriend.MaxValue);
            enemy.ZombieSearch.InitializeStartTarget(character);

            Destroy(enemyFriend.gameObject);
            Destroy(gameObject);
        }
    }
}