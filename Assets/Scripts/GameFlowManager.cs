using KartGame.KartSystems;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public bool autoFindKarts = true;
    public ArcadeKart playerKart;
    private float elapsedTimeBeforeEndScene;

    private ArcadeKart[] karts;
    private string m_SceneToLoad;
    private float m_TimeLoadEndGameScene;

    private void Start()
    {
        if (autoFindKarts)
        {
            karts = FindObjectsOfType<ArcadeKart>();
            if (karts.Length > 0)
                if (!playerKart)
                    playerKart = karts[0];
            DebugUtility.HandleErrorIfNullFindObject<ArcadeKart, GameFlowManager>(playerKart, this);
        }

        AudioUtility.SetMasterVolume(1);

        foreach (var k in karts) k.SetCanMove(false);
        StartRace();
    }

    private void StartRace()
    {
        foreach (var k in karts) k.SetCanMove(true);
    }
}