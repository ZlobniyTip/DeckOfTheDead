using Card;
using Deck;
using Other;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class DragAndDropCardWeapon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private GameObject _cardObject;
        [SerializeField] private AudioSource _soundCard;
        [SerializeField] private Image _indikator;
        [SerializeField] private AudioSource _spawnSound;

        private RectTransform _rectTransform;
        private Vector3 _originalPosition;
        private bool _isSpawnPossible;
        private CardView _cardView;
        private PlayerDeck _deck;

        private void Awake()
        {
            _deck = GetComponentInParent<PlayerDeck>();
            _rectTransform = GetComponent<RectTransform>();
            _cardView = GetComponent<CardView>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _soundCard.Play();

            _originalPosition = transform.position;
            TryUpdateSpawnVisuals();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
            TryUpdateSpawnVisuals();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isSpawnPossible)
                PerformSpawn();
            else
                ResetPosition();
        }

        private void TryUpdateSpawnVisuals()
        {
            if (FindSpawnLocation(out Vector3 spawnPosition))
            {
                if (!_isSpawnPossible)
                {
                    _indikator.gameObject.SetActive(true);
                    _isSpawnPossible = true;
                }
            }
            else
            {
                if (_isSpawnPossible)
                {
                    _indikator.gameObject.SetActive(false);
                    _isSpawnPossible = false;
                }
            }
        }

        private void PerformSpawn()
        {
            _deck.Character.CharacterShooting.UseTemporaryWeapons((_cardView.Card as CardDataWeapon).PrefabWeapon, _cardView);
            _deck.Character.CharacterShooting.StartWeaponTimer((_cardView.Card as CardDataWeapon).TimeAction);
            _deck.RemoveCard(_cardView);
            _deck.TakeAwayPlayerEnergy(_cardView.Card.Energy);
            _deck.Character.CharacterShooting.PlayWeaponSpawnEffect();
            _deck.Character.CharacterShooting.PlaySoundEffect();

            _spawnSound.Play();
            Destroy(gameObject);
        }

        private void ResetPosition()
        {
            transform.position = _originalPosition;
            _indikator.gameObject.SetActive(false);
        }

        private bool FindSpawnLocation(out Vector3 spawnPosition)
        {
            if (IsPointerOverUI())
            {
                spawnPosition = _originalPosition;
                return false;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
            {
                if (hit.collider.GetComponent<Arm>() != null)
                {
                    spawnPosition = _originalPosition;
                    return false;
                }
            }

            foreach (var hit in hits)
            {
                if (hit.collider.GetComponent<Road>() != null)
                {
                    spawnPosition = hit.point;
                    return true;
                }
            }

            spawnPosition = _originalPosition;
            return false;
        }

        private bool IsPointerOverUI()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                if (result.gameObject.GetComponent<Arm>() != null)
                    return true;
            }

            return false;
        }
    }
}