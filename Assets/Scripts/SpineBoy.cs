using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineBoy : MonoBehaviour
{
  public SkeletonAnimation skeletonAnimation;

  // Start is called before the first frame update
  void Start()
  {
    skeletonAnimation.AnimationState.SetAnimation(0, "run", true);
    Invoke("SwitchWalk", 3);
  }

  void SwitchWalk()
  {
    skeletonAnimation.AnimationState.SetAnimation(0, "walk", true);
  }
}
