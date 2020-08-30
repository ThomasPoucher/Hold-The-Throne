using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool enemiesInCam = EnemiesInCamera();
   //     Debug.Log("Enemies In Camera? " + enemiesInCam);
        if (!enemiesInCam)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);//.AddForce((Vector3.right * Time.deltaTime * 0.3f));
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    bool EnemiesInCamera()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Any(x => CameraEx.IsObjectVisible(Camera.main, x.GetComponent<Renderer>(),new Vector3(5,0,0)));
        
    }
}
