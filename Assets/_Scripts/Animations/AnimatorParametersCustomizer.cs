using UnityEngine;

namespace Project.Animations
{

    public sealed class AnimatorParametersCustomizer
    {
        public static int GetHash(string parameter)
        {
            return Animator.StringToHash(parameter);
        }
    }

}
