using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Animator animator;

    public void End()
    {
        StartCoroutine(Destroy());
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
