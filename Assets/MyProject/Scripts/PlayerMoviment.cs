using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMoviment : MonoBehaviour
{

    // Não está sendo usado, criei esse pra diminuir nosso código em playerController. (Está funcionando)

    public float moveSpeed = 5f; // Velocidade normal do jogador
    private Rigidbody2D rig; // Referência ao Rigidbody2D
    public Camera cam; // Referência à câmera para detectar posição do mouse

    Vector2 movement; // Guarda o movimento do teclado
    Vector2 mousePos; // Guarda a posição do mouse no mundo

    void Awake()
    {
        InitializeComponents();
    }

    private void Update() 
    {
        ProcessInputs();
        AdjustSpeed();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    // Inicializa componentes necessários
    void InitializeComponents()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Processa inputs do teclado e do mouse
    void ProcessInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    // Ajusta a velocidade ao pressionar Shift
    void AdjustSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10f; // Velocidade de corrida
        }
        else
        {
            moveSpeed = 5f; // Velocidade normal
        }
    }

    // Move o jogador baseado no input e velocidade
    void MovePlayer()
    {
        rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Faz o jogador olhar para o mouse
    void RotatePlayer()
    {
        Vector2 lookDir = mousePos - rig.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rig.rotation = angle;
    }
}