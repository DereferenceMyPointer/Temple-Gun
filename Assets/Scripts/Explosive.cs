using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public Vector3 target;
    public float speed;
    public LayerMask killables;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed);
        if(Vector3.Magnitude(transform.position - target) < 0.1)
        {
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position, PController.Instance.explosionRadius);
    }

    void Explode()
    {
        float explosionSize = PController.Instance.explosionRadius;
        foreach(Collider2D col in Physics2D.OverlapCircleAll(transform.position, explosionSize, killables))
        {
            col.gameObject.GetComponent<Death>().Kill();
        }
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
        expl.transform.localScale = new Vector3(explosionSize, explosionSize, expl.transform.localScale.z);
        AudioSource boom = expl.gameObject.GetComponent<AudioSource>();
        boom.pitch = Mathf.Clamp(1f / explosionSize, 0.9f, 2f);
        boom.Play();
        Destroy(expl.gameObject, 0.4f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Explode();
        }
    }
}
