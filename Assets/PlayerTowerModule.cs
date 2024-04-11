using System;
using RedAndBlue.PlayerModules;
using UnityEngine;

public class PlayerTowerModule : PlayerModuleViewBase
{
    public bool DebugEnabled = false;

    public Transform TowerTransform;
    public float TowerRotationSpeed = 10f;

    public Vector2 lookDirection;

    public override IPlayerModulesPresenter PlayerModules { get; protected set; }


    public override void Initialize(IPlayerModulesPresenter playerModulesPresenter)
    {
        PlayerModules = playerModulesPresenter;

        PlayerModules.FixedUpdate += OnFixedUpdate;
    }

    public override void Dispose()
    {
        PlayerModules.FixedUpdate -= OnFixedUpdate;
    }

    public override void OnFixedUpdate(float tick)
    {
        if (!DebugEnabled)
        {
            return;
        }

        RotateTower(lookDirection, tick);
    }

    private void RotateTower(Vector2 input, float tick)
    {
        var moveHorizontal = new Vector3(input.x, 0, input.y);

        var lookRotation =
            Quaternion.LookRotation(
                Vector3.Lerp(TowerTransform.forward, moveHorizontal, TowerRotationSpeed * tick),
                Vector3.up);

        TowerTransform.rotation = lookRotation;
    }
}