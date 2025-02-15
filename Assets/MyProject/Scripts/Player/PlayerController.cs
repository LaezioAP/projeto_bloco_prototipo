using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class PlayerController : MonoBehaviour
{
    private Vector3 UP = Vector3.up;
    private Vector3 LEFT = Vector3.left;
    private Vector3 RIGHT = Vector3.right;
    private Vector3 DOWN = Vector3.down;

    public Animator animator;

    public bool moving = false;
    public bool isRotationEnabled = true;
    float speed = 6.0f;

    Vector3 mousePos;
    Camera cam;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        GameEvents.Instance.OnStartDialogue += HandleStartDialogue;
        GameEvents.Instance.OnFinishDialogue += HandleFinishtDialogue;
    }

    private void HandleStartDialogue(DialogueDataSO sO)
    {
        moving = false;
        UP = Vector3.zero;
        DOWN = Vector3.zero;
        LEFT = Vector3.zero;
        RIGHT = Vector3.zero;
        StopCamera();
    }

    private void HandleFinishtDialogue()
    {
        moving = true;
        UP = Vector3.up;
        DOWN = Vector3.down;
        LEFT = Vector3.left;
        RIGHT = Vector3.right;
        RotateToCamera();
    }


    private void OnDestroy()
    {
        GameEvents.Instance.OnStartDialogue -= HandleStartDialogue;
        GameEvents.Instance.OnFinishDialogue -= HandleFinishtDialogue;
    }

    private void Update()
    {

        if (isRotationEnabled)
        {
            RotateToCamera();
        }
        else 
        {
            StopCamera();
        }
        Animations();
    }

    private void FixedUpdate()
    {
        Movement();
    }


    void StopCamera() 
    {
        rb.transform.eulerAngles = Vector3.zero;
        isRotationEnabled = false;
    }
    void RotateToCamera() 
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        rb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg);
        isRotationEnabled = true;
    }

    void Movement()
    {
        //Adicionei aqui o moving inicalmente como false, como o movent � validado no update, sempre que a cada frame passar se o usuario n�o cliclou em nenhum tecla, o moving torna-se false, caso contr�rio vira true.
        moving = false;

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

    }

    void ApplyMovement(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        moving = true;
    }

    void Animations()
    {
        animator.SetBool("walking", moving);
    }
}
