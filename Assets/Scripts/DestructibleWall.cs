using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : Death
{
  public Animator animator;
  public int tier;

  public override void Kill()
  {
      if(PController.Instance.gun.GetInteger("GunSize") >= tier)
      {
        StopAllCoroutines();
        StartCoroutine(Die());
      }
  }

  public IEnumerator Die()
  {
    animator.SetTrigger("Death");
    GetComponent<BoxCollider2D>().enabled = false;
    yield return new WaitForSeconds(1);
    Destroy(this.gameObject);
  }
}
