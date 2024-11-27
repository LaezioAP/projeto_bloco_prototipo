using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetilScript : MonoBehaviour
{
    [SerializeField] private float tempoDuracaoProjetil;
    [SerializeField] private float velocidadeProjetil;

    void Start()
    {
        //
        Destroy(gameObject, tempoDuracaoProjetil);
    }

    // Update is called once per frame
    void Update()
    {
        // movimentar o projetil
        transform.Translate(Vector2.right * velocidadeProjetil * Time.deltaTime);
    }
}
