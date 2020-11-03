using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
   // bool isInvisible = false;
    private Vector3 vectorToUse;
    bool isWrappingX = false;
    bool isWrappingY = false;
    bool isVisible = true;
    int timesWrapped = 0;
    public Sprite damageSprite;
    // Start is called before the first frame update
    void Start()
    {
      //  Destroy(gameObject, 4.0f);
        vectorToUse = transform.up;
        GetComponent<Rigidbody2D>().AddForce(vectorToUse,ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(DamageCoroutine());
        isVisible = CameraEx.IsObjectVisible(Camera.main, GetComponent<SpriteRenderer>());
        ScreenWrap();

    }

    void ScreenWrap()
    {
       // var isVisible = CheckRenderers();
        if(timesWrapped > 0)
        {
            GetComponent<TrailRenderer>().enabled = true;
            return;
        }
        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }
        //else
        //{
      
        //    timesWrapped++;
        //}

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            viewportPosition.x -= 1;
            newPosition.x = cam.ViewportToWorldPoint(-viewportPosition).x;

            isWrappingX = true;
        }

        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = newPosition.y;

            isWrappingY = true;
        }

        if (timesWrapped >= 1)
        {
            Destroy(gameObject);
        }
        vectorToUse = new Vector3(vectorToUse.x, vectorToUse.y, 0);// -vectorToUse;
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().Clear();
        transform.position = newPosition;
        Destroy(gameObject,2.0f);
        timesWrapped++;
    }
    IEnumerator DamageCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = 10;
        GetComponent<SpriteRenderer>().sprite = damageSprite;
        GetComponent<TrailRenderer>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var collisionObject = collision.contacts[0].collider.gameObject;
        Debug.Log(collisionObject.layer + " collision total " + collision.contactCount);
        if (collisionObject.gameObject.layer == 18)
        {
            var audio = collisionObject.gameObject.GetComponent<AudioSource>();
            if(!audio.isPlaying)
            {
                audio.Play();
            }
            timesWrapped++;
            if(timesWrapped > 1)
            {
                Destroy(gameObject);
            }
        }
        if (collisionObject.gameObject.layer == 9)
        {
            collisionObject.gameObject.GetComponent<PlayerMovement>().DamageTimer(transform.position);
            Destroy(gameObject);
        }
        if (gameObject.layer != 16 && (collisionObject.gameObject.layer == 11 || collisionObject.gameObject.layer == 16))
        {
            if (collisionObject.gameObject.GetComponent<Walker>() != null && collisionObject.gameObject.GetComponent<Walker>().CanDamage)
            {
                collisionObject.gameObject.GetComponent<Walker>().DamageTimer();
            }
            Destroy(gameObject);    
        }
        if (gameObject.layer != 16 && (collisionObject.gameObject.layer == 14))
        {
            if (collisionObject.gameObject.GetComponent<Knight>() != null && collisionObject.gameObject.GetComponent<Knight>().CanDamage)
            {
                collisionObject.gameObject.GetComponent<Knight>().DamageTimer();
            }
            if (collisionObject.gameObject.GetComponent<MoveAttackScript>() != null && collisionObject.gameObject.GetComponent<MoveAttackScript>().CanDamage)
            {
                collisionObject.gameObject.GetComponent<MoveAttackScript>().DamageTimer();
            }
            Destroy(gameObject);
        }
    }
}
