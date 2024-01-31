using System.Collections;
using UnityEngine;

namespace BBG.Dueling
{
    public abstract class DuelComponent : MonoBehaviour
    {
        protected Duel duel { private set; get; }
        protected Player currentPlayer => duel.CurrentPlayer;

        protected void Execute(IEnumerator coroutine)
        {
            CoroutineQueue.Instance.AddCoroutine(coroutine);
        }

        protected void Wait(float seconds) => Execute(WaitCoroutine(seconds));  

        private IEnumerator WaitCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        protected void PlaySfx(AudioClip clip)
        {
            SoundManager.Instance.PlaySfx(clip);
        }

        internal virtual void Setup(Duel duel)
        {
            this.duel = duel;
        }
    }
}