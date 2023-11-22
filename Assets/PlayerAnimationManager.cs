using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void Teleport(Transform position)
    {
        gameObject.transform.position = position.transform.position;
        gameObject.transform.rotation = position.transform.rotation;
    }

    public void isSitting(bool isActive)
    {
        playerAnimator.SetBool("isSitting", isActive);
    }

    public void isCrouching(bool isActive)
    {
        playerAnimator.SetBool("isCrouching", isActive);
    }
    public void isCutting(bool isActive)
    {
        playerAnimator.SetBool("isCutting", isActive);
    }
}
