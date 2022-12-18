using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChunkGen : MonoBehaviour
{
    [SerializeField] ChunkGenerator CG;

    private void Awake() {
        CG = transform.parent.parent.gameObject.GetComponent<ChunkGenerator>();
        GetComponent<Collider>().enabled = true;    
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            CG.CreateNewChunk();    
            GetComponent<Collider>().enabled = false;
            CG.currentChunk++;
        } 
    }
}
