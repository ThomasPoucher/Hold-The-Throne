using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject Bone;
    public float TimeToSpawn = 2.0f;
    private bool startedCoroutine = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator ThrowBone(float spawnTime)
    {
        startedCoroutine = true;
        yield return new WaitForSeconds(spawnTime);
        var item1 = Instantiate(Bone, transform.position, Quaternion.Euler(new Vector3(0,0,45)));
       // var vectorToUse = transform.up;
        item1.GetComponent<Rigidbody2D>().AddForce(item1.gameObject.transform.up, ForceMode2D.Impulse);
        var item2 = Instantiate(Bone, transform.position, Quaternion.Euler(new Vector3(0, 0, 135)));
        item2.GetComponent<Rigidbody2D>().AddForce(item2.gameObject.transform.up, ForceMode2D.Impulse);
        StartCoroutine(ThrowBone(TimeToSpawn));


    }
    void OnWillRenderObject()
    {
        if (CameraEx.IsObjectVisible(Camera.main, GetComponent<SpriteRenderer>()) && !startedCoroutine)
            StartCoroutine(ThrowBone(TimeToSpawn));
    }
}
