using RedAndBlue.PlayerModules;
using UnityEngine;

public class PlayerAnimationModule : PlayerModuleViewBase
{
    private const string IsMovingKey = "IsMoving";
    private static readonly int IsMoving = Animator.StringToHash(IsMovingKey);

    public Animator PlayerAnimator;

    public override IPlayerModulesPresenter PlayerModules { get; protected set; }

    public override void Initialize(IPlayerModulesPresenter playerModulesPresenter)
    {
        PlayerModules = playerModulesPresenter;

        PlayerModules.FrameUpdate += OnFixedUpdate;
    }

    public override void Dispose()
    {
        PlayerModules.FrameUpdate -= OnFixedUpdate;
    }

    public override void OnFixedUpdate(float tick)
    {
        var move = PlayerModules.GetInputValues();

        if (move.x == 0 && move.y == 0)
        {
            PlayerAnimator.SetBool(IsMoving, false);
            return;
        }

        PlayerAnimator.SetBool(IsMoving, true);
    }
}