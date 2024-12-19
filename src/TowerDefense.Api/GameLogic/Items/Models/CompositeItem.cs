using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Grid;
using System.Collections;
using TowerDefense.Api.GameLogic.Visitor;

namespace TowerDefense.Api.GameLogic.Items.Models;

public class CompositeItem : IItem, IEnumerable<IItem>
{
    public string Id { get; set; } = "Composite";
    public IItemStats Stats { get; set; } = new DefaultZeroItemStats();
    public ItemType ItemType { get; set; } = ItemType.Blank;
    private readonly List<IItem> _children = new();
    public IEnumerable<IItem> Children => _children.AsReadOnly();

    public void Add(IItem item)
    {
        _children.Add(item);
        Console.WriteLine(
            $"[CompositeItem] Added item '{item.Id}' to composite '{Id}'. Total children: {_children.Count}"
        );
    }

    public void Remove(IItem item)
    {
        _children.Remove(item);
        Console.WriteLine(
            $"[CompositeItem] Removed item '{item.Id}' from composite '{Id}'. Total children: {_children.Count}"
        );
    }

    public IItem GetChild(int index)
    {
        if (index < 0 || index >= _children.Count)
        {
            Console.WriteLine(
                $"[CompositeItem] GetChild: Invalid index {index} for composite '{Id}'."
            );
            return null;
        }
        var child = _children[index];
        Console.WriteLine(
            $"[CompositeItem] GetChild: Retrieved item '{child.Id}' from composite '{Id}' at index {index}."
        );
        return child;
    }

    public IEnumerable<AttackDeclaration> Attack(
        IArenaGrid opponentsArenaGrid,
        int attackingGridItemId
    )
    {
        // For composite, aggregate attacks from all children
        var results = new List<AttackDeclaration>();
        Console.WriteLine(
            $"[CompositeItem] Composite '{Id}' initiating attack with {_children.Count} children."
        );
        foreach (var child in _children)
        {
            results.AddRange(child.Attack(opponentsArenaGrid, attackingGridItemId));
            Console.WriteLine($"[CompositeItem] Child '{child.Id}' performed an attack.");
        }
        return results;
    }

    public IEnumerator<IItem> GetEnumerator()
    {
        return ((IEnumerable<IItem>)_children).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Accept(IItemVisitor visitor)
    {
        throw new NotImplementedException();
    }
}
