using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Lives = 3;
    public float speed = 4.0f;
    public float jumpForce = 1.0f;
    public Rigidbody2D CharacterRigidbody;
    public Animator characterAnimator;
    public SpriteRenderer sprite;

    bool isGround;

    private void Awake()
    {
        CharacterRigidbody = GetComponentInChildren<Rigidbody2D>();
        characterAnimator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Move()
    {
        Vector3 tempVector = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position+tempVector, speed * Time.deltaTime);
        characterAnimator.SetInteger("State", 1);
        if(tempVector.x<0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    void Jump()
    {
        CharacterRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        isGround = colliders.Length > 1;
        Debug.Log(colliders.Length);
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal"))
        {
            Move();
        }
        if(Input.GetButton("Jump"))
        {
            Jump();
            characterAnimator.SetInteger("State", 2);
        }

        if(!Input.anyKey)
        {
            characterAnimator.SetInteger("State", 0);
        }
    }
}
