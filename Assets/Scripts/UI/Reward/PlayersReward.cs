using UnityEngine;

namespace UI
{
    public class PlayersReward : MonoBehaviour
    {
        [SerializeField] private RewardView _rewardView;
        [SerializeField] private Buyer _buyer;

        private void OnEnable()
        {
            _rewardView.RewardCounted += RewardPlayer;
        }

        private void OnDisable()
        {
            _rewardView.RewardCounted -= RewardPlayer;
        }

        private void RewardPlayer(int money)
        {
            _buyer.GetMoney(money);
        }
    }
}