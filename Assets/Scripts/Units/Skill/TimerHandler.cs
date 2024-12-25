using System.Collections;
using TMPro;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    [SerializeField] private Enemy _prefabEnemy;

    private int _timerEmoSrill = 15;
    private TMP_Text _timer;
    private Enemy _enemy;

    public void StartTimerEmoSkill(Unit enemyFriend, Character character)
    {
        _timer = enemyFriend.FXUnit.StartTimer();
        StartCoroutine(TimerEmoSkill(enemyFriend, character));
    }

    private IEnumerator TimerEmoSkill(Unit enemyFriend, Character character)
    {
        while (_timerEmoSrill > 0)
        {
            _timer.text = _timerEmoSrill.ToString();
            yield return new WaitForSeconds(1);
            _timerEmoSrill--;
        }

        Enemy enemy = Instantiate(_prefabEnemy, enemyFriend.transform.position, enemyFriend.transform.rotation);
        enemy.SetValue(enemyFriend.Value, enemyFriend.MaxValue);
        enemy.ZombieSearch.InitializeStartTarget(character);

        Destroy(enemyFriend.gameObject);
        Destroy(gameObject);
    }
}