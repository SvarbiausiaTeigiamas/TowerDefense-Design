using TowerDefense.Api.GameLogic.Items.Models;

namespace TowerDefense.Api.GameLogic.Items
{
    public interface IItemCreator
    {
        IItem CreateItem();
    }

    public class BlankItemCreator : IItemCreator
    {
        public IItem CreateItem()
        {
            return new Blank();
        }
    }

    public class PlaneItemCreator : IItemCreator
    {
        public IItem CreateItem()
        {
            return new Plane();
        }
    }

    public class RocketsItemCreator : IItemCreator
    {
        public IItem CreateItem()
        {
            return new Rockets();
        }
    }

    public class ShieldItemCreator : IItemCreator
    {
        public IItem CreateItem()
        {
            return new Shield();
        }
    }

    public class PlaceholderItemCreator : IItemCreator
    {
        public IItem CreateItem()
        {
            return new Placeholder();
        }
    }
}
