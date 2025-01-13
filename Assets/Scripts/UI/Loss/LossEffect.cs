using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LossEffect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StopAnimator());
    }

    private IEnumerator StopAnimator()
    {
        var delay = new WaitForSeconds(1);

        yield return delay;

        Destroy(gameObject);
    }
}