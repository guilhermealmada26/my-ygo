using System.Collections;
using UnityEngine;

namespace BBG.Animations
{
    public class ScaleCoroutine
    {
        public static IEnumerator Play(float duration, Transform toScale, Vector3 targetScale)
        {
            var timeElapsed = 0.0f;
            var initialScale = toScale.localScale;
            while (timeElapsed < duration)
            {
                float t = timeElapsed / duration;
                toScale.localScale = Vector3.Lerp(initialScale, targetScale, t);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}