using System.Collections;
using UnityEngine;

public class Knight : MonoBehaviour
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
    public int KnightState = 0;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger(nameof(KnightState), KnightState);
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
        if(player.transform.position.x < transform.position.x)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (CanMove && KnightState != 2)
        {
            var diffVector = transform.position - player.transform.position;
            
            Debug.Log(diffVector);
            if((Mathf.Abs(diffVector.x) < 3 && Mathf.Abs(diffVector.y) < 1.2))
            {
                GetComponent<Rigidbody2D>().MovePosition(transform.position - ((diffVector) * Time.deltaTime * (Speed * 4f)));
                KnightState = 1;
                animator.SetInteger(nameof(KnightState), KnightState);
            }
            else
            {
                GetComponent<Rigidbody2D>().MovePosition(transform.position - ((diffVector) * Time.deltaTime * Speed));
                KnightState = 0;
                animator.SetInteger(nameof(KnightState), KnightState);
            }

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
            if (Health > 1 && CanDamage)
            {
                Health--;
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
            else
            {
                CanMove = false;
                CanDamage = false;
                transform.SetParent(Camera.main.transform);
                Destroy(GetComponent<Rigidbody2D>());
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponentInChildren<BoxCollider2D>().enabled = false;
                KnightState = 2;
                animator.SetInteger(nameof(KnightState), KnightState);
                
            
                yield return new WaitForSeconds(1.15f);
                Destroy(gameObject);
            }
          
        }
    }
}
