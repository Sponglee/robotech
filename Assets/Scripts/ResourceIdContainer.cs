public static class ResourceIdContainer
{
    private const string GameplayGroupId = "GameplayResources";

    public static GameplayResourceContainer GameplayResourceContainer { get; }

    static ResourceIdContainer()
    {
        GameplayResourceContainer = new GameplayResourceContainer(GameplayGroupId);
    }
}