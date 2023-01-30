using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public float speed = 10f;
    public float jumpforce;
    public LayerMask ground;
    public Collider2D coll;
    public int carrot;
    public TMP_Text textMeshPro;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
    }
    
    void Movement()
    {   
        //player movement
        float horizontalmove =Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);
            anim.SetFloat("skip", Math.Abs(facedirection));
        }

        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection * 5.99f, 5.99f, 5.99f);
        }
        //player jump
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jump", true);
        }
    }

    void SwitchAnim()
    {   
        anim.SetBool("idle",false);
        if (anim.GetBool(("jump")))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jump",false);
                anim.SetBool("fall", true);
            }
            else if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("fall", false);
                anim.SetBool("idle",true);
            }
        }
    }

    //collections
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collections")
        {
            Destroy(collision.gameObject);
            carrot += 1;
            textMeshPro.text = carrot.ToString();
        }
    }
    //kill enermy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
    