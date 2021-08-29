using UnityEngine;

public class ShowControls : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape))
        {
            HideTheControls();
        }
    }
    
    public void ShowTheControls()
    {
        gameObject.SetActive(true);
    }

    public void HideTheControls()
    {
        gameObject.SetActive(false);
    }
}
