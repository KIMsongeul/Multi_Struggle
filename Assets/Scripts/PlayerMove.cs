using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float speed = 8;
    public float jumpPower = 6;

    private Vector2 inputVec;
    private int jumpCnt = 0;
    private bool isGround;
    private bool isDown = false;
    private int gravityPower = 2;
    

    private Animator anim;
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    

    void Move()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        Vector2 nextVec = inputVec.normalized * speed;
        rigid.velocity = new Vector2(nextVec.x, rigid.velocity.y);
        
        
        //캐릭터 좌우변환
        if (inputVec.x > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        if (inputVec.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        anim.SetBool(AnimStrings.MoveBool, inputVec.x !=0);

        if (Input.GetKey(KeyCode.S))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y - gravityPower * Time.deltaTime);
            isDown = true;
            anim.SetBool(AnimStrings.IsDown,isDown);
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < 2)
        {
            isGround = false;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpPower,ForceMode2D.Impulse);
            jumpCnt++;
            anim.SetTrigger(AnimStrings.JumpTrigger);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            jumpCnt = 0;
            isDown = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        anim.SetBool(AnimStrings.IsGround,isGround);
    }
}
