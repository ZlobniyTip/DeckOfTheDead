using UnityEngine;

public class SkillEmo : MonoBehaviour
{
    [SerializeField] private Unit _enemyFriend;
    [SerializeField] private TimerHandler _timerHandler;

    public void ConvertEnemyToAlly(Zombie enemy, Character character)
    {
        if (enemy != null)
        {
            Unit enemyFriend = Instantiate(_enemyFriend, enemy.transform.position, enemy.transform.rotation);
            TimerHandler timerHandler = Instantiate(_timerHandler);

            enemyFriend.SetValue(enemy.Value, enemy.MaxValue);
            enemyFriend.SetCharacter(character);
            timerHandler.StartTimerEmoSkill(enemyFriend, character); 

            Destroy(enemy.gameObject);
        }
    }
}
