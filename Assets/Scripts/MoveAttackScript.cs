using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool CanDamage = false;
    public bool CanMove = false;
    private GameObject player;
    public float Speed = 2.0f;
    public bool IsOnScreen = false;
    public int Health = 1;
    public Material damageMaterial;
    // Update is called once per frame
    public Material startMaterial;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    void Start()
    {
        
    }
    void OnWillRenderObject()
    {
        if (CameraEx.IsObjectVisible(Camera.main, GetComponent<SpriteRenderer>()) && !IsOnScreen)
        {
           // Camera.main.GetComponent<AudioSource>().
            CanMove = true;
            CanDamage = true;
            IsOnScreen = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {
            GetComponent<Rigidbody2D>().MovePosition(transform.position - ((transform.position - player.transform.position) * Time.deltaTime * Speed));
        }
    }
    public void DamageTimer()
    {
        GetComponent<AudioSource>().Play();
        if (CanDamage && IsOnScreen)
        {
            StartCoroutine(Damage());
        }
    }
    IEnumerator Damage()
    {
        if (CanDamage)
        {
            if (Health > 1)
            {
                Health--;
            }
            else
            {
                Destroy(gameObject);
            }
            CanDamage = false;
            GetComponent<SpriteRenderer>().material = damageMaterial;
            Color current = GetComponent<SpriteRenderer>().color;
            current.a = 100;
            GetComponent<SpriteRenderer>().color = current;
            //    Debug.Log("Bye");
            yield return new WaitForSeconds(0.4f);
            //   Debug.Log("Hello");
            GetComponent<SpriteRenderer>().material = startMaterial;
            current.a = 255;
            GetComponent<SpriteRenderer>().color = current;
            CanDamage = true;
        }
    }
}
