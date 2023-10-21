using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorModeController : MonoBehaviour
{
    private void Start()
    {
        MakeCursorVisibility(false);
    }

    private void MakeCursorVisibility(bool isVisible)
    {
        if (isVisible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
