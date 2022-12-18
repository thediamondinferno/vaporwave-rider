using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using KartGame.KartSystems;

public class CameraFOV : MonoBehaviour
{
    
    [SerializeField]
    CinemachineVirtualCamera CVC;

    [SerializeField]
    MotorcycleController playerController;

    [SerializeField]
    float FOV_multiplier;
    void Start()
    {
        CVC = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(CVC.m_Lens.FieldOfView < 110) 
        CVC.m_Lens.FieldOfView = Mathf.Lerp(CVC.m_Lens.FieldOfView, 60 + playerController.acceleration*FOV_multiplier, Time.deltaTime);
        //Debug.Log(playerController.LocalSpeed());
    }
}
