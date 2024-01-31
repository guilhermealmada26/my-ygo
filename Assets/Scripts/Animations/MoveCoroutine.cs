using System.Collections;
using UnityEngine;

namespace BBG.Animations
{
    public static class MoveCoroutine
    {
        public static IEnumerator Play(float duration, Transform toMove, Vector3 destination)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                toMove.position = Vector3.Lerp(toMove.position, destination, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            toMove.position = destination;
        }
    }
}