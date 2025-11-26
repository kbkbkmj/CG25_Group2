using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void UpdateAnimation(Vector3 inputVector)
    {
        anim.SetBool("isWalking", inputVector.sqrMagnitude > 0);
    }

    public void Dead()
    {
        anim.SetTrigger("Dead");
    }

    public void Win()
    {
        anim.SetTrigger("Win");
    }
}
