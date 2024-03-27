using RedAndBlue.PlayerModules;
using UnityEngine;

public class PlayerMovementModule : PlayerModuleViewBase
{
    public Rigidbody Rigidbody;
    public Collider Collider;
    public float MoveSpeed = 10f;
    public override IPlayerModulesPresenter PlayerModules { get; protected set; }

    public override void Initialize(IPlayerModulesPresenter playerModulesPresenter)
    {
        PlayerModules = playerModulesPresenter;

        PlayerModules.FrameUpdate += OnFrameUpdate;
    }

    public override void Dispose()
    {
        PlayerModules.FrameUpdate -= OnFrameUpdate;
    }

    public override void OnFrameUpdate(float tick)
    {
        var input = PlayerModules.GetInputValues();
        MovePlayer(input);
    }

    private void MovePlayer(Vector2 move)
    {
        Rigidbody.velocity = new Vector3(move.x, Rigidbody.velocity.y, move.y) * MoveSpeed;
    }
}