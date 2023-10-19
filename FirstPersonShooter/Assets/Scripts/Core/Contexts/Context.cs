using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-16100)]
public abstract class Context : MonoBehaviour
{
    public abstract void InstallContext();
    public abstract void DisposeContext();
}

