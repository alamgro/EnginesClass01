using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    //[SerializeField]
    //private AnimationClip _clip;

    public void TriggerAnimation(AnimationClip clip) => _animator.Play(clip.name);
}
