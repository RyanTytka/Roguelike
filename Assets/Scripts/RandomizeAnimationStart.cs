using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimationStart : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ignore Mecanim transitions with non-zero transition offset and re-triggered calls
        if (stateInfo.normalizedTime > 0.001f)
        {
            return;
        }

        var offset = Random.value * 2.0f;
        if (animator.IsInTransition(layerIndex))
        {
            var transition = animator.GetAnimatorTransitionInfo(layerIndex);

            // Re-triggers OnStateEnter on next update
            animator.CrossFade(stateInfo.fullPathHash, transition.duration, layerIndex, offset, transition.normalizedTime);
            return;
        }

        // Re-triggers OnStateEnter on next update
        animator.Play(stateInfo.fullPathHash, layerIndex, offset);
    }
}
