using System.Collections;
using UnityEngine;

namespace BBG.Animations
{
    [CreateAssetMenu(menuName = "Animations/ParticleAnimation")]
    public class ParticleAnimation : ScriptableObject
    {
        [SerializeField] GameObject prefab;
        [SerializeField] AudioClip clip;
        [SerializeField] float duration;
        [SerializeField] float scale;

        public float Duration => duration;

        public IEnumerator Play (Vector3 origin, Vector3? moveTo = null, float scaleMultiplier = 1f) 
        {
            SoundManager.Instance.PlaySfx(clip);
            if (prefab == null)
                yield break;

            var instantiation = Instantiate(prefab, origin, Quaternion.identity);
            instantiation.transform.localScale = Vector3.one * scale * scaleMultiplier;
            
            if (moveTo.HasValue)
                yield return MoveCoroutine.Play(duration, instantiation.transform, moveTo.Value);
            else
                yield return new WaitForSeconds(duration);

            Destroy(instantiation);
        }
    }
}