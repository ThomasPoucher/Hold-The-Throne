using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        if(Input.GetMouseButtonUp(0))
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
