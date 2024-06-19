using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStrings : MonoBehaviour
{
    public static int MoveBool = Animator.StringToHash("Move");
    public static int JumpTrigger = Animator.StringToHash("Jump");
    public static int IsGround = Animator.StringToHash("IsGround");
    public static int IsDown = Animator.StringToHash("IsDown");
}
