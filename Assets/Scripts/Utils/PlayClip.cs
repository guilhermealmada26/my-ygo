using UnityEngine;

public class PlayClip : StateMachineBehaviour
{
    [SerializeField] AudioClip clip;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.PlaySfx(clip);
    }
}
