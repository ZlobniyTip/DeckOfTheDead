using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace UI
{
    public class Bestiary : MonoBehaviour
    {
        private readonly List<BestiaryView> _content = new();

        [SerializeField] private List<ZombieData> _zombies;
        [SerializeField] private GameObject _container;
        [SerializeField] private BestiaryView _prefabView;

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