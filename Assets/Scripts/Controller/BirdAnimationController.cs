using UnityEngine;

public class BirdAnimationController : MonoBehaviour
{
    private Animator birdAnimator;
    private string birdFlap = "bird_flap";

    private void Start()
    {
        birdAnimator = GetComponent<Animator>();
    }

    public void makeFlap()
    {
        birdAnimator.SetTrigger(birdFlap);
    }
}