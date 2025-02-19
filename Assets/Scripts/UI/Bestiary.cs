using Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Bestiary : MonoBehaviour
    {
        [SerializeField] private List<ZombieData> _zombies;
        [SerializeField] private GameObject _container;
        [SerializeField] private BestiaryView _prefabView;

        private List<BestiaryView> _content = new();

        private void OnEnable()
        {
            FillBestiary();
        }

        private void OnDisable()
        {
            for (int i = 0; i < _content.Count; i++)
            {
                Destroy(_content[i].gameObject);
            }

            _content.Clear();
        }

        private void FillBestiary()
        {
            for (int i = 0; i < _zombies.Count; i++)
            {
                var view = Instantiate(_prefabView, _container.transform);
                view.Initialize(_zombies[i]);
                _content.Add(view);
            }
        }
    }
}