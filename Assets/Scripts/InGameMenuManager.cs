using UnityEngine;
using UnityEngine.EventSystems;

public class InGameMenuManager : MonoBehaviour
{
    [Tooltip("Root GameObject of the menu used to toggle its activation")]
    public GameObject menuRoot;

    [Tooltip("Master volume when menu is open")] [Range(0.001f, 1f)]
    public float volumeWhenMenuOpen = 0.5f;

    [Tooltip("GameObject for the controls")]
    public GameObject controlImage;

    private void Start()
    {
        menuRoot.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown(GameConstants.k_ButtonNamePauseMenu)
            || (menuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
        {
            if (controlImage.activeSelf)
            {
                controlImage.SetActive(false);
                return;
            }

            SetPauseMenuActivation(!menuRoot.activeSelf);
        }

        if (Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
            if (EventSystem.current.currentSelectedGameObject == null)
                EventSystem.current.SetSelectedGameObject(null);
    }

    public void ClosePauseMenu()
    {
        SetPauseMenuActivation(false);
    }


    public void TogglePauseMenu()
    {
        SetPauseMenuActivation(!menuRoot.activeSelf);
    }

    private void SetPauseMenuActivation(bool active)
    {
        menuRoot.SetActive(active);

        if (menuRoot.activeSelf)
        {
            //     Cursor.lockState = CursorLockMode.None;
            //  Cursor.visible = true;
            Time.timeScale = 0f;
            AudioUtility.SetMasterVolume(volumeWhenMenuOpen);

            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            //   Cursor.lockState = CursorLockMode.Locked;
            //   Cursor.visible = false;
            Time.timeScale = 1f;
            AudioUtility.SetMasterVolume(1);
        }
    }

    public void OnShowControlButtonClicked(bool show)
    {
        controlImage.SetActive(show);
    }
}