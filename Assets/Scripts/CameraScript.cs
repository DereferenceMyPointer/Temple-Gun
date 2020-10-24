using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float followSpeed = .3f;
    public float trailDistance = 3f;
    public float growSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), followSpeed);
        }
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 2.8f + PController.Instance.explosionRadius, growSpeed);
    }
}
