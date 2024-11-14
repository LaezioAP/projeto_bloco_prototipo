using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 UP = Vector3.up;
    private Vector3 LEFT = Vector3.left;
    private Vector3 RIGHT = Vector3.right;
    private Vector3 DOWN = Vector3.down;

    public Animator animator;

    public bool moving = false;
    float speed = 7.0f;

    Vector3 mousePos;
    Camera cam;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        
    }

    private void Update()
    {
        RotateToCamera();
        Animations();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void RotateToCamera() 
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        rb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg);
    }

    void Movement() 
    {
        if (Input.GetKey(KeyCode.W))
        {
            ApplyMovement(UP);
        }

        if (Input.GetKey(KeyCode.A))
        {
            ApplyMovement(LEFT);
        }

        if (Input.GetKey(KeyCode.S))
        {
            ApplyMovement(DOWN);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ApplyMovement(RIGHT);
        }

        // Esse validação, é necessária? (validar com o Kauan)
        if (checkInputPressed) 
        {
            moving = false;
        }
    }

    void ApplyMovement(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        moving = true;
    }

    void Animations()
    {
        if (!moving)
        {
            animator.SetBool("walking", false);
        } else
        {
            animator.SetBool("walking", true);
        }
    }

    bool CheckInputPressed()
    {
        return Input.GetKey(KeyCode.D) != true && Input.GetKey(KeyCode.A) != true && Input.GetKey(KeyCode.S) != true && Input.GetKey(KeyCode.W) != true;
    }
}
