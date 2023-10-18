using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectContext : Context
{
    private static bool _isInstalled;

    public static void Install()
    {
        if (_isInstalled)
        {
            return;
        }

        var projectContextPrefab = Resources.Load<ProjectContext>("ProjectContext");
        var projectContext = Instantiate(projectContextPrefab);
        projectContext.gameObject.name = nameof(ProjectContext);
        DontDestroyOnLoad(projectContext);

        projectContext.InstallContext();

        _isInstalled = true;
    }

    private void OnDestroy()
    {
        DisposeContext();
    }

    public static InputManager InputManager;

    public override void InstallContext()
    {
        InputManager = new InputManager();
    }

    public override void DisposeContext()
    {
        InputManager = null;
    }
}

