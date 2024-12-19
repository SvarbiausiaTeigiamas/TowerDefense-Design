namespace TowerDefense.Api.GameLogic.Mediator
{
    public interface IMediator
    {
        Task Notify(object sender, string eventCode);
    }
}
