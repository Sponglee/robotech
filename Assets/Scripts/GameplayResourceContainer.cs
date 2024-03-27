public struct GameplayResourceContainer
{
    public readonly string GroupId { get; }

    public readonly string PlayerView;

    public GameplayResourceContainer(string groupId)
    {
        GroupId = groupId;

        PlayerView = "PlayerView";
    }
}