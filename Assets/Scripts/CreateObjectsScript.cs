using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;


public class CreateObjectsScript : MonoBehaviour
{
    
    [SerializeField] GameObject[] Prefabs;
    [SerializeField] int[] howMany;
    [SerializeField] int minPosX,maxPosX,minPosY,maxPosY,minPosZ,maxPosZ;

    [SerializeField] int minScale, maxScale;
    [SerializeField] int cloudOffset;

    [SerializeField] GameObject[] skyObjects;
    [SerializeField] GameObject[] groundObjects;
    [SerializeField] GameObject[] other;

    void Start(){
        CreateAndScatterObjects();
    }

    [Button]
    public void CreateAndScatterObjects(){
        int skySoFar = 0, groundSoFar = 0, otherSoFar = 0;
        countWantedObjects();
        for(int i = 0; i < Prefabs.Length; i++){
            if(Prefabs[i].name.Contains("Sky")){
                for(int j = 0; j < howMany[i]; j++){
                    Quaternion skyAngle = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                    skyObjects[skySoFar] = Instantiate(Prefabs[i], new Vector3(Random.Range(transform.parent.transform.position.x + minPosX, transform.parent.transform.position.x + maxPosX), Random.Range(minPosY+cloudOffset+Random.Range(0, 50), maxPosY+cloudOffset+Random.Range(0, 50)), Random.Range(transform.parent.transform.position.z + minPosZ, transform.parent.transform.position.z + maxPosZ)), skyAngle, this.transform);
                    float localScale = skyObjects[skySoFar].transform.localScale.x;
                    float newScale = Random.Range(localScale+minScale, localScale+maxScale);
                    skyObjects[skySoFar].transform.localScale = new Vector3(newScale, newScale, newScale);
                    skySoFar++;
                }
            }
            else if(Prefabs[i].name.Contains("Ground")){

                //int offsetY = 0;
                int scaleOffset = 0;
                if(Prefabs[i].name.Contains("GroundObject3")) {
                    //offsetY = 80;
                    scaleOffset = 10;
                }
                //else {
                    //offsetY = 0;
                    //scaleOffset = 0;
                //}

                for(int j = 0; j < howMany[i]; j++){
                    Quaternion groundAngle = Quaternion.Euler(Random.Range(-90, 90), Random.Range(0, 360), Random.Range(-90, 90));
                    groundObjects[groundSoFar] = Instantiate(Prefabs[i], new Vector3(Random.Range(transform.parent.transform.position.x + minPosX, transform.parent.transform.position.x + maxPosX), Random.Range(minPosY, maxPosY), Random.Range(transform.parent.transform.position.z + minPosZ, transform.parent.transform.position.z + maxPosZ)), groundAngle, this.transform);
                    float localScale = groundObjects[groundSoFar].transform.localScale.x;
                    float newScale = Random.Range(localScale*scaleOffset+minScale, localScale*scaleOffset+maxScale);
                    groundObjects[groundSoFar].transform.localScale = new Vector3(newScale, newScale, newScale);
                    groundSoFar++;
                }
            }
            else{
                for(int j = 0; j < howMany[i]; j++){
                    Quaternion angle = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                    other[otherSoFar] = Instantiate(Prefabs[i], new Vector3(Random.Range(transform.parent.transform.position.x + minPosX, transform.parent.transform.position.x + maxPosX), Random.Range(minPosY, maxPosY), Random.Range(transform.parent.transform.position.z + minPosZ, transform.parent.transform.position.z + maxPosZ)), angle, this.transform);
                    float localScale = other[otherSoFar].transform.localScale.x;
                    float newScale = Random.Range(localScale+minScale, localScale+maxScale);
                    other[otherSoFar].transform.localScale = new Vector3(newScale, newScale, newScale);
                    otherSoFar++;
                }
            }   
        }         
    }

    [Button]
    private void DestroyCreatedObjects(){
        for(int i = 0; i < skyObjects.Length; i++) DestroyImmediate(skyObjects[i]);
        for(int i = 0; i < groundObjects.Length; i++) DestroyImmediate(groundObjects[i]);
        for(int i = 0; i < other.Length; i++) DestroyImmediate(other[i]);
        countWantedObjects();
    }
    
    private void countWantedObjects(){
        int numOfSkyObjects = 0;
        int numOfGroundObjects = 0;
        int numOfOther = 0;
        for(int i = 0; i < howMany.Length; i++){
            if(Prefabs[i].name.Contains("Sky")){
                numOfSkyObjects+=howMany[i];
            }
            else if(Prefabs[i].name.Contains("Ground")){
                numOfGroundObjects+=howMany[i];
            }
            else{
                numOfOther+=howMany[i];
            }   
        }  
        skyObjects = new GameObject[numOfSkyObjects];
        groundObjects = new GameObject[numOfGroundObjects];
        other = new GameObject[numOfOther];
    }
}
