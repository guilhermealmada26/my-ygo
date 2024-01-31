using System.Collections;
using UnityEngine;

namespace BBG.Animations
{
    public class LineAnimation : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] float duration;

        public IEnumerator Play(Transform initialPosition, Vector3 targetPosition)
        {
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.SetPositions(new Vector3[] { initialPosition.position, targetPosition });
            yield return new WaitForSeconds(duration);
        }

        public void Disable()
        {
            lineRenderer.gameObject.SetActive(false);
        }
    }
}