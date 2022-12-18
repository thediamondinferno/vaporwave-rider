using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    public List<GameObject> chunks;
    public Vector2 spawnNextChunkAt;

    [SerializeField] int skipEvery;
    bool skip = false;

    public int currentChunk = 0;

    public void CreateNewChunk(){
        
        Debug.Log(chunks.Count + " % " + skipEvery + " = " + chunks.Count % skipEvery);
        if(currentChunk % skipEvery == 0){
            skip = false;
        }

        if(!skip){

            if(currentChunk == 0)
            for(int i = 0; i < skipEvery; i++){
                chunks.Add(Instantiate(chunkPrefab, new Vector3(spawnNextChunkAt.x, 0, spawnNextChunkAt.y), Quaternion.identity, this.transform));
                spawnNextChunkAt += new Vector2(0, 200);
            }

            for(int i = 0; i < skipEvery; i++){
                chunks.Add(Instantiate(chunkPrefab, new Vector3(spawnNextChunkAt.x, 0, spawnNextChunkAt.y), Quaternion.identity, this.transform));
                spawnNextChunkAt += new Vector2(0, 200);
            }
            skip = true;
        }

    }
}
