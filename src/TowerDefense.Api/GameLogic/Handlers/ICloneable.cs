namespace TowerDefense.Api.GameLogic.Handlers
{
    // Let's say, hypothetically, we want to create a copy of the some handler
    // without having to re-create all the constructor params from scratch.
    // ICloneable interface allows us to do that - Prototype design pattern.
    // Couldn't find another place where this pattern could be more useful.
    public interface ICloneable<T>
    {
        T Clone();
    }
}
