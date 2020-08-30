using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int BossState = 0;
    private Animator animator;
    public bool ready = true;
    public GameObject projectile;
    public GameObject laserProjectile;
    public GameObject bats;
    public GameObject player;
    public bool finishedShooting = true;
    bool hasSetMusic = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ResetState(2f));
    }

    // Update is called once per frame
    void Update()
    {
    }
    void PickNewState()
    {
        ready = true;
        StopCoroutine(ThrowProjectile());
        StopCoroutine(LaserProjectile());
        StopCoroutine(SpawnBats());
        BossState = UnityEngine.Random.Range(0, 4);
        Debug.Log("State is " + BossState);
        animator.SetInteger(nameof(BossState), BossState);
        ready = false;
      //  finishedShooting = true;
        switch (BossState)
        {
            case (0):
                {
                    break;
                }
            case (1):
                {
                    Laser();
                    break;
                }
            case (2):
                {
                    Shoot();
                    break;
                }
            case (3):
                {
                    Spawn();
                    break;
                }
            default:
                break;
        }
    }
    IEnumerator WaitFor(Action act, float seconds)
    {
        Debug.Log("Started");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Ended");
        act.Invoke();
    }
    IEnumerator ResetState(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (CameraEx.IsObjectVisible(Camera.main, GetComponent<SpriteRenderer>()))
            {
                if(!hasSetMusic)
            {
              //  Camera.main.GetComponent<AudioSource>().pitch = -1f;
                hasSetMusic = true;
            }
                PickNewState();
            }
        StartCoroutine(ResetState(seconds));
    }
  
    void Laser()
    {
        Debug.Log("Laser");
        if (finishedShooting)
        {
            StopCoroutine(ThrowProjectile());
            StopCoroutine(LaserProjectile());
            StopCoroutine(SpawnBats());
            StartCoroutine(LaserProjectile());
        }
        // StartCoroutine(WaitFor(2f));
    }
    void Shoot()
    {
        if (finishedShooting)
        {
            StopCoroutine(ThrowProjectile());
            StopCoroutine(LaserProjectile());
            StopCoroutine(SpawnBats());
            StartCoroutine(ThrowProjectile());
        }
        //StartCoroutine(WaitFor(2f));
    }
    void Spawn()
    {
        Debug.Log("Spawn");
        if (finishedShooting)
        {
            StopCoroutine(ThrowProjectile());
            StopCoroutine(LaserProjectile());
            StopCoroutine(SpawnBats());
            StartCoroutine(SpawnBats());
        }
    }
    void Idle()
    {
        Debug.Log("Idle");

    }
    IEnumerator SpawnBats()
    {
        yield return new WaitForSeconds(0.3f);
        var item1 = Instantiate(bats, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        // var vectorToUse = transform.up;
        item1.GetComponent<Rigidbody2D>().AddForce(-item1.gameObject.transform.up, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        var item2 = Instantiate(bats, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        item2.GetComponent<Rigidbody2D>().AddForce(-item2.gameObject.transform.up, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        var item3 = Instantiate(bats, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        item3.GetComponent<Rigidbody2D>().AddForce(-item2.gameObject.transform.up, ForceMode2D.Impulse);
        //   finishedShooting = true;

    }
    IEnumerator ThrowProjectile()
    {
        yield return new WaitForSeconds(1.0f);
        //var item1 = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //// var vectorToUse = transform.up;
        //item1.GetComponent<Rigidbody2D>().AddForce(-item1.gameObject.transform.right * 0.85f, ForceMode2D.Impulse);
        var item2 = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 25)));
        item2.GetComponent<Rigidbody2D>().AddForce(-item2.gameObject.transform.right * 0.75f, ForceMode2D.Impulse);
        var item3 = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, -25)));
        item3.GetComponent<Rigidbody2D>().AddForce(-item3.gameObject.transform.right * 0.75f, ForceMode2D.Impulse);
        //yield return new WaitForSeconds(1.0f);
        //var item1 = Instantiate(projectile, transform.position , Quaternion.Euler(new Vector3(0, 0, 0)));
        //// var vectorToUse = transform.up;
        //item1.GetComponent<Rigidbody2D>().AddForce(-item1.gameObject.transform.up, ForceMode2D.Impulse);
        //var item2 = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //item2.GetComponent<Rigidbody2D>().AddForce(-item2.gameObject.transform.up, ForceMode2D.Impulse);
        //var item3 = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //item3.GetComponent<Rigidbody2D>().AddForce(-item2.gameObject.transform.up, ForceMode2D.Impulse);
        ////   finishedShooting = true;

    }
    IEnumerator LaserProjectile()
    {
        //if(!finishedShooting)
        //{
        //    yield return null;
        //}
        //else
        //{
          //  finishedShooting = false;
            yield return new WaitForSeconds(0.3f);
            var dir = player.GetComponent<Rigidbody2D>().position - (GetComponent<Rigidbody2D>().position + new Vector2(0, 1));
            Debug.Log("Dir " + dir);
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);
            var item1 = Instantiate(laserProjectile, transform.position + new Vector3(0, 1, 0), rotation);
            item1.GetComponent<Rigidbody2D>().AddForce(item1.transform.up, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
         dir = player.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position;
       
         rotation = Quaternion.LookRotation(Vector3.forward, dir);
         item1 = Instantiate(laserProjectile, transform.position + new Vector3(0, 1, 0), rotation);
        item1.GetComponent<Rigidbody2D>().AddForce(item1.transform.up, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
         dir = player.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position;
       
         rotation = Quaternion.LookRotation(Vector3.forward, dir);
         item1 = Instantiate(laserProjectile, transform.position + new Vector3(0, 1, 0), rotation);
        item1.GetComponent<Rigidbody2D>().AddForce(item1.transform.up, ForceMode2D.Impulse);
        // finishedShooting = true;
        // }


    }
}
