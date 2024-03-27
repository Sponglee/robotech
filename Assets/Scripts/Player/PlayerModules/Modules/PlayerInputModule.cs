using RedAndBlue.PlayerModules;
using UnityEngine;

public class PlayerInputModule : PlayerModuleViewBase
{
    public Vector2 Move;

    public override IPlayerModulesPresenter PlayerModules { get; protected set; }

    private void Awake()
    {
    }

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
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        Move = new Vector2(moveHorizontal, moveVertical);
    }
}