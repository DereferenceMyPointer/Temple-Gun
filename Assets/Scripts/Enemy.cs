using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : Death
{
    [Header("Movement")]
    public float radiusIncrease;
    public float speed;
    public float minMoveTimeout;
    public float maxMoveTimeout;
    public float minIdleTimeout;
    public float maxIdleTimeout;
    public float avgSpawnTime;
    public float spawnChance;
    public float idleChance;
    public float spawnOtherChance = 3f;

    [Header("Components")]
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject prefab;

    public enum States
    {
        Still,
        Moving
    };

    public States state;

    private void Start()
    {
        state = States.Still;
        StartCoroutine(Still(Random.Range(minIdleTimeout, maxIdleTimeout)));
    }

    public override void Kill()
    {
        rb.velocity = Vector2.zero;
        PController.Instance.IncreaseRadius(radiusIncrease);
        StopAllCoroutines();
        StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
      animator.SetTrigger("Death");
      GetComponent<CapsuleCollider2D>().enabled = false;
      yield return new WaitForSeconds(1);
      Destroy(this.gameObject);
    }

    void NewMove()
    {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        StartCoroutine(Move(direction, Random.Range(minMoveTimeout, maxMoveTimeout)));
    }

    public IEnumerator Move(Vector2 direction, float time)
    {
        rb.velocity = direction.normalized * speed;
        if (Random.Range(1f, 10f) < spawnChance)
        {
            if (Random.Range(0f, 10f) < spawnOtherChance)
            {
                Instantiate(gameObject, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(time);
        if (Random.Range(1, 10) < idleChance)
        {
            StartCoroutine(Still(Random.Range(minIdleTimeout, maxIdleTimeout)));
        } else
        {
            NewMove();
        }
    }

    public IEnumerator Still(float time)
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        NewMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Death>(out Death d))
        {
            if (d.gameObject.tag != "Enemy")
            {

                d.Kill();
            }
        }
    }

}
