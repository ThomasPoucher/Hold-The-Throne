using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 3.0f;
    public int Health = 3;
    public GameObject tmp;
    public bool CanDamage = true;
    public Material damageMaterial;
    // Update is called once per frame
    public Material startMaterial;

    // Start is called before the first frame update
    void Start()
    {
        tmp.GetComponent<TextMeshProUGUI>().text = "Health: " + Mathf.Max(Health, 0);
    }
    private void Update()
    {
        if (Health <= 0)
        {
            var score = GameObject.FindGameObjectWithTag("ScoreManager");
            score.GetComponent<Timer>().CamPosition = Mathf.RoundToInt(Camera.main.transform.position.x);
            score.GetComponent<Timer>().CalculateScore();
            SceneChanger.LoadScene("GameOver");
        }
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Backspace))
        {
            SceneChanger.LoadScene("MainMenu");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");
        var moveAmount = new Vector2(hor, ver) * Time.deltaTime * Speed;
        GetComponent<Rigidbody2D>().velocity = Camera.main.GetComponent<Rigidbody2D>().velocity + moveAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 14)
        {
            
            DamageTimer(collision.gameObject.transform.position);
        }
    }
    public void DamageTimer(Vector3 otherPosition)
    {
        if (CanDamage)
        {
            GetComponent<Rigidbody2D>().AddForce((transform.position - otherPosition) * 2000f);
            StartCoroutine(Damage());
            
        }
    }
    IEnumerator Damage()
    {
        if (CanDamage)
        {
            if (Health >= 0)
            {
                Health--;
                tmp.GetComponent<TextMeshProUGUI>().text = "Health: " + Mathf.Max(Health, 0);
            }
   
            CanDamage = false;
            GetComponent<SpriteRenderer>().material = damageMaterial;
            Color current = GetComponent<SpriteRenderer>().color;
            current.a = 100;
            GetComponent<SpriteRenderer>().color = current;
            yield return new WaitForSeconds(1.0f);
            GetComponent<SpriteRenderer>().material = startMaterial;
            current.a = 255;
            GetComponent<SpriteRenderer>().color = current;
            CanDamage = true;
        }
    }
}
