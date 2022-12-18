using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using PathCreation;

public class CreateTracks : MonoBehaviour
{
    
    public Vector3[] newPath;
    [SerializeField] int chunkSize;
    [SerializeField] int turns;

    [SerializeField] int firstX;

    [SerializeField] PathCreation.Examples.RoadMeshCreator rmc;

    public VertexPath theVertexPath;

    [Button]
    public void GeneratePath(){

        firstX = 5;

        newPath = new Vector3[turns];

        newPath[0] = new Vector3(chunkSize/2, 0.1f, 0);

        for(int i = 1; i < turns-1; i++){
            //Debug.Log(transform.parent.transform.position.z + " < " + firstX*200);
            if(transform.parent.transform.position.z < firstX*200) newPath[i] = new Vector3(chunkSize/2, 0.1f, chunkSize/2);
            else newPath[i] = new Vector3((int)Random.Range(0, chunkSize), 0.1f, (int)Random.Range(0, chunkSize));
        }

        newPath[newPath.Length-1] = new Vector3(chunkSize/2, 0.1f, chunkSize);

        BezierPath bezierPath = new BezierPath(newPath);
        theVertexPath = new VertexPath(bezierPath, transform);

    }

    void Start(){
        GeneratePath();
        rmc.TriggerUpdate();
    }
}
