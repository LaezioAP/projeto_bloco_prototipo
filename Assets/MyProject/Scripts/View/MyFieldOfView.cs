
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MyFieldOfView : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform player; // Referência ao Player
    [SerializeField] private LayerMask visibilityLayerMask; // Camada para itens/NPCs visíveis apenas no FoV
    private Mesh mesh;
    private float fov;
    private float viewDistance;
    private float startingAngle;
    private List<GameObject> visibleObjects; // Lista de objetos a verificar

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 160f;
        viewDistance = 8f;
        visibleObjects = new List<GameObject>();

        if (player == null)
        {
            Debug.LogError("Player não foi atribuído ao Field of View!");
        }

        // Encontra todos os objetos com a tag "VisibleInFoV" (ou use outro critério)
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ItenFov"))
        {
            visibleObjects.Add(obj);
            Debug.Log("Objeto adicionado à lista: " + obj.name);
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position; // FOV segue o Player
        }

        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = transform.position + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = transform.InverseTransformPoint(vertex);

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100f);

        // Atualiza a visibilidade dos objetos
        UpdateObjectVisibility();
    }

    private void UpdateObjectVisibility()
    {
        foreach (GameObject obj in visibleObjects)
        {
            if (obj == null) continue;

            Vector3 directionToObject = (obj.transform.position - transform.position).normalized;
            float distanceToObject = Vector3.Distance(transform.position, obj.transform.position);
            float angleToObject = Vector3.Angle(directionToObject, UtilsClass.GetVectorFromAngle(startingAngle - fov / 2f));

            // Verifica se o objeto está dentro da distância e do ângulo do FoV
            bool isInFoV = distanceToObject <= viewDistance && Mathf.Abs(angleToObject) <= fov / 2f;

            if (isInFoV)
            {
                // Verifica se há obstáculos entre o jogador e o objeto
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToObject, distanceToObject, layerMask);
                if (hit.collider == null || hit.collider.gameObject == obj) // Sem obstáculos ou o objeto é o próprio alvo
                {
                    obj.SetActive(true); // Torna visível
                }
                else
                {
                    obj.SetActive(false); // Esconde se houver obstáculo
                }
            }
            else
            {
                obj.SetActive(false); // Esconde se estiver fora do FoV
            }
        }
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFoV(float fov)
    {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

}
