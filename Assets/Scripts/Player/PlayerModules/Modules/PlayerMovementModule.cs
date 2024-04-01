using RedAndBlue.PlayerModules;
using UnityEngine;

public class PlayerMovementModule : PlayerModuleViewBase
{
    public Rigidbody Rigidbody;
    public Collider Collider;
    public float MoveSpeed = 10f;
    public float RotationSpeed = 10f;
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

        MovePlayer(input, tick);

        RotatePlayer(input, tick);
    }

    private void MovePlayer(Vector2 input, float tick)
    {
        if (input == default) return;

        var moveWithGravity = new Vector3(input.x, Rigidbody.velocity.y, input.y) * MoveSpeed * tick;

        Rigidbody.velocity = moveWithGravity;
    }

    private void RotatePlayer(Vector2 input, float tick)
    {
        var moveHorizontal = new Vector3(input.x, 0, input.y);

        var lookRotation =
            Quaternion.LookRotation(
                Vector3.Lerp(transform.forward, moveHorizontal, RotationSpeed * tick),
                Vector3.up);

        transform.rotation = lookRotation;
    }
}