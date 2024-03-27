using System;
using RedAndBlue.PlayerModules;
using UnityEngine;

public abstract class PlayerModuleViewBase : MonoBehaviour, IDisposable
{
    public abstract IPlayerModulesPresenter PlayerModules { get; protected set; }
    public abstract void Initialize(IPlayerModulesPresenter playerModulesPresenter);
    public abstract void Dispose();
    public abstract void OnFrameUpdate(float tick);
}