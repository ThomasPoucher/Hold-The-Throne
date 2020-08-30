using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Walker : MonoBehaviour
{
    public float Speed = 50f;
    private bool canMove = false;
    Vector3 moveTowards = Vector3.zero;
    public Material damageMaterial;
    // Update is called once per frame
    public Material startMaterial;
    public bool CanDamage = true;
    public int Health = 5;
    public bool IsOnScreen = false;
    public float MoveTime = 1.5f;
    public AudioClip DamageNoise;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject score;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        score = GameObject.FindGameObjectWithTag("ScoreManager");
    }
    void Start()
    {
        if(DamageNoise != null)
        GetComponent<AudioSource>().clip = DamageNoise;
        // StartCoroutine(Damage());
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (score == null)
        {
            score = GameObject.FindGameObjectWithTag("ScoreManager");
        }
        if (canMove && IsOnScreen)
        {
            moveTowards = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), 0);
            canMove = false;
            StartCoroutine(MoveFor());
        }

        GetComponent<Rigidbody2D>().AddForce(moveTowards.normalized * Time.deltaTime * Speed);
    }
    void OnWillRenderObject()
    {
        //Debug.Log("Camera current is " + Camera.current);
        if (CameraEx.IsObjectVisible(Camera.main, GetComponent<SpriteRenderer>()))
        {
           // if (Camera.current.tag == "MainCamera" && !IsOnScreen)
        //    {
                canMove = true;
                IsOnScreen = true;
          //  }
        }
     }
    IEnumerator MoveFor()
    {
        yield return new WaitForSeconds(MoveTime);
        canMove = true;
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
                if (gameObject.name == "Boss")
                {
                    score.GetComponent<Timer>().HealthRemaining = player.GetComponent<PlayerMovement>().Health;
                    score.GetComponent<Timer>().CamPosition = Mathf.RoundToInt(Camera.main.transform.position.x);
                    score.GetComponent<Timer>().CalculateScore();
                    SceneManager.LoadScene("WinScreen");
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            CanDamage = false;
            GetComponent<SpriteRenderer>().material = damageMaterial;
            Color current = GetComponent<SpriteRenderer>().color;
            current.a = 100;
            GetComponent<SpriteRenderer>().color = current;
            //    Debug.Log("Bye");
            yield return new WaitForSeconds(0.3f);
            //   Debug.Log("Hello");
            GetComponent<SpriteRenderer>().material = startMaterial;
            current.a = 255;
            GetComponent<SpriteRenderer>().color = current;
            CanDamage = true;
        }
    }
}
