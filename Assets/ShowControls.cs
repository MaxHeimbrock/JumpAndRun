using UnityEngine;

public class ShowControls : MonoBehaviour
{
    public GameObject controlsImage;
    public bool controlsImageActive = false;

    public void Update()
    {
        if (controlsImageActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Backspace))
            {
                HideTheControls();
            }
        }
    }
    
    public void ShowTheControls()
    {
        controlsImageActive = true;
        controlsImage.SetActive(true);
    }

    public void HideTheControls()
    {
        controlsImageActive = false;
        controlsImage.SetActive(false);
    }
}
