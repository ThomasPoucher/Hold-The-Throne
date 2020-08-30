using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<PlayerMovement>().DamageTimer(transform.position);
            Destroy(gameObject);
        }
    }
}
