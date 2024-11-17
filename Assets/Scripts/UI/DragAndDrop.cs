using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _dragObject;
    [SerializeField] private GameObject _cardView;

    private RectTransform _rectTransform;
    private Vector3 _originalPosition;

    private GameObject _gameObject;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPosition = transform.position;

        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            _cardView.gameObject.SetActive(false);
            _gameObject = Instantiate(_dragObject, spawnPosition, Quaternion.identity);
            _gameObject.transform.position = spawnPosition;
        }
        else
        {
            _gameObject = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;

        if (FindSpawnLocation(out Vector3 spawnPosition))
        {
            _gameObject.transform.position = spawnPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_gameObject != null && FindSpawnLocation(out Vector3 spawnPosition))
        {
            _gameObject.transform.position = spawnPosition;
        }
        else
        {
            if (_gameObject != null)
            {
                Destroy(_gameObject);
            }

            transform.position = _originalPosition;
            _cardView.gameObject.SetActive(true);
        }
    }

    private bool FindSpawnLocation(out Vector3 spawnPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            spawnPosition = hit.point;
            return true;
        }

        spawnPosition = _originalPosition;
        return false; 
    }
}
