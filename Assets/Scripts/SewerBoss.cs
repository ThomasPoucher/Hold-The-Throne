using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerBoss : MonoBehaviour
{
    public int BossState = 0;
    public bool ready = true;
    public float Speed = 10;
    public GameObject Rat;
    public GameObject WhooshSound;
    private bool hasActivated = false;
    private Animator animator;
    [SerializeField]
    private bool attacking = false;
    private GameObject player;
    private Vector3 spawnPoint;
    private Vector3 attackPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player");
        spawnPoint = transform.position;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ResetState(2f));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking)
        {
            var diffVector = transform.position - new Vector3(attackPosition.x, attackPosition.y, attackPosition.z);
            if(diffVector.x <= 0)
            {
                diffVector.x = 1;
            }
            GetComponent<Rigidbody2D>().MovePosition(transform.position - ((diffVector.normalized) * Time.deltaTime * (Speed * 4f)));
        }
    }

    void PickNewState()
    {
        if(attacking)
        {
            return;
        }
       // GetComponent<Walker>().enabled = false;
   
        ready = true;
        StopCoroutine(SpawnRats());
        StopCoroutine(AttackPlayer());
        BossState = UnityEngine.Random.Range(0, 3);
        Debug.Log("State is " + BossState);
        animator.SetInteger(nameof(BossState), BossState);
        ready = false;
        switch (BossState)
        {
            case (0):
                {
                    Idle();
                    break;
                }
            case (1):
                {
                    Spawn();
                    break;
                }
            case (2):
                {
                    Attack();
                    break;
                }
            default:
                break;
        }
      

    }

    private void Idle()
    {
    }

    IEnumerator ResetState(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (CameraEx.IsObjectVisible(Camera.main, GetComponent<SpriteRenderer>()))
        {
            hasActivated = true;
            PickNewState();
        }
        else if (hasActivated && attacking)
        {
            ready = false;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.position = new Vector3(Camera.main.transform.position.x + 10f, spawnPoint.y, spawnPoint.z);
            attacking = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
            StopCoroutine(AttackPlayer());
            animator.SetInteger(nameof(BossState), 0);
        }
        StartCoroutine(ResetState(seconds));
    }
    private void Attack()
    {
        if(attacking)
        {
            return;
        }
    
        StartCoroutine(AttackPlayer());
  
    }

    private void Spawn()
    {
        StartCoroutine(SpawnRats());
    }

    private IEnumerator AttackPlayer()
    {
        attackPosition = player.transform.position;
  
        yield return new WaitForSeconds(1.0f);
        Instantiate(WhooshSound);
        attacking = true;
        GetComponent<CircleCollider2D>().isTrigger = true;
        //  GetComponent<Walker>().enabled = false;
        yield return new WaitForSeconds(3.5f);

    }

    private IEnumerator SpawnRats()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(Rat, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))).GetComponent<Rigidbody2D>().AddForce(-transform.up, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        Instantiate(Rat, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))).GetComponent<Rigidbody2D>().AddForce(-transform.up, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        Instantiate(Rat, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))).GetComponent<Rigidbody2D>().AddForce(-transform.up, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().DamageTimer(gameObject.transform.position);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile") || collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            gameObject.GetComponent<Walker>().DamageTimer();
        }
    }
}
