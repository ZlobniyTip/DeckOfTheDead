using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace UI
{
    public class Bestiary : MonoBehaviour
    {
        private readonly List<BestiaryView> Content = new();

        [SerializeField] private List<ZombieData> _zombies;
        [SerializeField] private GameObject _container;
        [SerializeField] private BestiaryView _prefabView;

        private void OnEnable()
        {
            FillBestiary();
        }

        private void OnDisable()
        {
            for (int i = 0; i < Content.Count; i++)
            {
                Destroy(Content[i].gameObject);
            }

            Content.Clear();
        }

        private void FillBestiary()
        {
            for (int i = 0; i < _zombies.Count; i++)
            {
                var view = Instantiate(_prefabView, _container.transform);
                view.Initialize(_zombies[i]);
                Content.Add(view);
            }
        }
    }
}