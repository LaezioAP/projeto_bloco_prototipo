using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    
    [SerializeField] private Transform barrel; // Posicao de onde o tiro vai sair
    [SerializeField] private float cadenciaTiro; // cadencia do tipo
    [SerializeField] private GameObject projetil;

    private float tempoDoTiro; // Controle de cadencia


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleShooting();
    }

    private void HandleShooting() 
    {
        if (Input.GetMouseButtonDown(0) && CanShoot())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        tempoDoTiro = Time.time + cadenciaTiro;
        

        Instantiate(projetil, barrel.position, barrel.rotation);
    }

    private bool CanShoot()
    {
        return Time.time > tempoDoTiro;
    }

}
