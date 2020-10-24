using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] private float shootForgiveness;
    //distance extended per second
    public float chargeRate;
    [SerializeField] private float inputHeldTime;
    [SerializeField] private string shootAxis;
    public float explosionRadius;


    [Header("Movement")]
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection = new Vector2(1, 0);
    private float xDirection = 1;
    [SerializeField] private int speed;
    [SerializeField] private int shootingSpeed;

    [Header("Components")]
    public Animator animator;
    [SerializeField] private Rigidbody2D rb;
    public Transform target;
    public GameObject projectilePrefab;
    public Transform graphicsHolder;
    public Animator gun;

    public static PController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float vertMov = Input.GetAxisRaw("Vertical");
        float horMov = Input.GetAxisRaw("Horizontal");
        Move(horMov, vertMov);
        HandleShoot();
        if(explosionRadius > 8)
        {
          gun.SetInteger("GunSize", 2);
        } else if(explosionRadius > 2)
        {
          gun.SetInteger("GunSize", 1);
        }

    }

    private void Move(float horizontal, float vertical)
    {
        moveDirection = new Vector2(horizontal, vertical);
        if (((moveDirection.x  > 0 && xDirection < 0) ||
            (moveDirection.x < 0 && xDirection > 0)))
        {
            Flip();
        }
        moveDirection = moveDirection.normalized * speed;
        if (!(moveDirection.x == 0 && moveDirection.y == 0))
        {
            lastMoveDirection = moveDirection;
            animator.SetBool("Walking", true);
            if (moveDirection.x != 0)
            {
                xDirection = moveDirection.x;
            }
        } else
        {
            animator.SetBool("Walking", false);
        }
        if (inputHeldTime == 0)
        {
            moveDirection = moveDirection.normalized * speed;
        } else
        {
            moveDirection = moveDirection.normalized * shootingSpeed;
        }
        rb.velocity = moveDirection;
    }

    void HandleShoot()
    {
        if (!Input.GetButton(shootAxis))
        {
            if (inputHeldTime > shootForgiveness)
            {
                Shoot();
            }
            inputHeldTime = 0;
            target.gameObject.SetActive(false);
        } else
        {
            inputHeldTime += chargeRate * Time.deltaTime;
            if (inputHeldTime > shootForgiveness)
            {
                Vector3 targetDirection = new Vector3(lastMoveDirection.x, lastMoveDirection.y, target.position.z);
                target.position = transform.position + targetDirection.normalized * ((inputHeldTime - shootForgiveness) *(1 + explosionRadius * 2));
                target.localScale = new Vector3(1 + explosionRadius / 3, 1 + explosionRadius / 3);
                target.gameObject.SetActive(true);
            }
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Explosive>().target = target.position;
    }

    public void IncreaseRadius(float amount)
    {
        explosionRadius += amount;
    }

    void Flip()
    {
        Vector3 scale = graphicsHolder.localScale;
        scale.x *= -1;
        graphicsHolder.localScale = scale;
    }

    public void SetDisabled()
    {
        rb.velocity = Vector2.zero;
        animator.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

}
