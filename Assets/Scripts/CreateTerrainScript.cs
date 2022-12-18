using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;


[RequireComponent(typeof(MeshFilter))]
public class CreateTerrainScript : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    Color[] colors;

    float minTerrainHeight;
    float maxTerrainHeight;

    public int xSize;
    public int zSize;

    [SerializeField] Gradient gradient;

    [SerializeField] MeshFilter meshFilter;

    [SerializeField] ChunkGenerator CG;

    void Start(){
        CG = transform.parent.parent.GetComponent<ChunkGenerator>();
        GenerateChunkTerrain();
    }


    [Button]
    public void GenerateChunkTerrain(){
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        // POSMATRA PRETHODNU ILI TI PRVU JA MISLIM STAZU

        for(int i = 0, z = 0; z <= zSize; z++){
            for(int x = 0; x <= xSize; x++){
                float y = CG.chunks.Count*Mathf.PerlinNoise(x * .05f, z * .05f) - CG.chunks.Count/2;
                //Debug.Log(x + " " + y + " " + z);
                
                Ray ray = new Ray(transform.position + new Vector3(x, y+1, z), new Vector3(0, -1, 0));
                if(Physics.Raycast(ray, 20)){
                    //Debug.DrawLine(new Vector3(x, y+1, z), new Vector3(x, y, z) + new Vector3(0, -20, 0), Color.yellow, 1000, false);
                    y = 0;
                    //Debug.Log(y);
                }
                
                
                vertices[i] = new Vector3(x, y, z);

                if(y > maxTerrainHeight) maxTerrainHeight = y;
                if(y < minTerrainHeight) minTerrainHeight = y;

                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for(int z = 0; z < zSize; z++){
            for(int x = 0; x < xSize; x++){
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                
                vert++; 
                tris += 6;
            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];
        colors = new Color[vertices.Length];

        for(int i = 0, z = 0; z <= zSize; z++){
            for(int x = 0; x <= xSize; x++){

                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }

        UpdateMesh();

    }

    void UpdateMesh(){
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.uv = uvs;
        mesh.colors = colors;

        mesh.RecalculateNormals();

        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i=0; i<vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}

