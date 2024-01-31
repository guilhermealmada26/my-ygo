using System.Collections;
using UnityEngine;

namespace BBG.Animations
{
    public static class RotateCoroutine
    {
        public static IEnumerator Rotate(float duration, Transform transform, Vector3 angle)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, angle, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.eulerAngles = angle;
        }

        public static IEnumerator RotateLocal(float duration, Transform transform, Vector3 angle)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, angle, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localEulerAngles = angle;
        }
    }
}