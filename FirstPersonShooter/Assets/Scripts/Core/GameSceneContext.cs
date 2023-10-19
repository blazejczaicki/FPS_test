using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneContext : SceneContext
{
    public static IMovementInput
        MovementAdapter;

    public override void InstallContext()
    {
        ProjectContext.InputManager.ToggleActionMap(ProjectContext.InputManager.Controls.Player);

        MovementAdapter = new PlayerInputAdapter(ProjectContext.InputManager.Controls);
    }

    public override void DisposeContext()
    {
    }
}
