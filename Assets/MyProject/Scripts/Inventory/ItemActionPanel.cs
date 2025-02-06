using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        // Prefab do botão que será instanciado no painel
        [SerializeField]
        private GameObject buttonPrefab;

        // Adiciona um novo botão ao painel com um nome e uma ação ao clicar
        public void AddButton(string name, Action onClickAction)
        {
            // Cria um novo botão a partir do prefab
            GameObject button = Instantiate(buttonPrefab, transform);

            // Adiciona a ação de clique ao botão
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());

            // Define o texto do botão com o nome passado como parâmetro
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }

        // Alterna a visibilidade do painel
        internal void Toggle(bool val)
        {
            // Se estiver ativando o painel, remove botões antigos para evitar acúmulo
            if (val == true)
                RemoveOldButtons();

            // Ativa ou desativa o painel
            gameObject.SetActive(val);
        }

        // Remove todos os botões antigos antes de adicionar novos
        public void RemoveOldButtons()
        {
            // Percorre todos os elementos filhos (botões) e os destrói
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }
    }
}