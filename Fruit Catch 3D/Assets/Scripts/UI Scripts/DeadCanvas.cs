using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCanvas : MonoBehaviour
{
    public void ButtonAnimations()
    {
        AnimationsManager.Instance.SkipButtonAnim();

        StartCoroutine(NoThanksButton());
    }

    public IEnumerator NoThanksButton()
    {
        yield return new WaitForSeconds(2.5f);
        AnimationsManager.Instance.NoThansButtonAnim();
    }
}
