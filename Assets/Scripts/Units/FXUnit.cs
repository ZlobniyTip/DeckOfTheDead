using TMPro;
using UnityEngine;

public class FXUnit : MonoBehaviour
{
    [SerializeField] private TMP_Text _timer;

 
    public TMP_Text StartTimer()
    {
        return _timer;
    }
}
