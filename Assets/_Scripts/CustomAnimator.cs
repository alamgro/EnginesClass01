using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void TriggerAnimation(AnimationClip clip) => _animator.Play(clip.name);
}
