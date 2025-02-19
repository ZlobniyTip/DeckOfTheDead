using Character;
using UnityEngine;

namespace UI.Reward
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

        private void RewardPlayer(float money)
        {
            _buyer.GetMoney((int)money);
        }
    }
}