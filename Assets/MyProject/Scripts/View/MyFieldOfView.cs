/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MyFieldOfView : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform player; // Referência ao Player
    private Mesh mesh;
    private float fov;
    private float viewDistance;
    private float startingAngle;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 160f;
        viewDistance = 8f;

        if (player == null)
        {
            Debug.LogError("Player não foi atribuído ao Field of View!");
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
                // No hit
                vertex = transform.position + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = transform.InverseTransformPoint(vertex); // Corrigido para manter local ao Mesh


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
