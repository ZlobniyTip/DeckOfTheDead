using Character;
using Enemy;
using UnityEngine;

namespace Units.Skills
{
    public class SkillEmo : MonoBehaviour
    {
        [SerializeField] private Unit _enemyFriend;
        [SerializeField] private TimerHandler _timerHandler;

        public void ConvertEnemyToAlly(Zombie enemy, Player character)
        {
            if (enemy != null)
            {
                Unit enemyFriend = Instantiate(_enemyFriend, enemy.transform.position, enemy.transform.rotation);
                TimerHandler timerHandler = Instantiate(_timerHandler);

                enemyFriend.SetValue(enemy.Value, enemy.MaxValue);
                enemyFriend.SetCharacter(character);
                timerHandler.StartTimerEmoSkill(enemyFriend, character);

                enemy.DeclareDeath();
                Destroy(enemy.gameObject);
            }
        }
    }
}