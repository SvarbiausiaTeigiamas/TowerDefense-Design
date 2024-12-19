using System.Collections;
using TowerDefense.Api.GameLogic.Attacks;
using TowerDefense.Api.GameLogic.Grid;
using TowerDefense.Api.GameLogic.Visitor;

namespace TowerDefense.Api.GameLogic.Items.Models;

public class CompositeWeapon : IItem, IEnumerable<IItem>
{
    public string Id { get; set; } = nameof(CompositeWeapon);
    public ItemType ItemType { get; set; } = ItemType.CompositeWeapon;
    public IItemStats Stats { get; set; } = new BasicDefenseItemStats();
    private readonly List<IItem> _components = new();

    public IEnumerable<IItem> Components => _components.AsReadOnly();

    public CompositeWeapon()
    {
        Console.WriteLine($"Creating {nameof(CompositeWeapon)}");
        AddComponent(new Shield());
        AddComponent(new Rockets());
        AddComponent(new Plane());
        AggregateStats();
    }

    public void AddComponent(IItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        _components.Add(item);
        Console.WriteLine(
            $"Added component {item.ItemType} with ID: {item.Id} to {nameof(CompositeWeapon)}"
        );
        AggregateStats();
    }

    public void RemoveComponent(IItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        _components.Remove(item);
        Console.WriteLine(
            $"Removed component {item.ItemType} with ID: {item.Id} from {nameof(CompositeWeapon)}"
        );
        AggregateStats();
    }

    // Iterator Implementation
    public IEnumerator<IItem> GetEnumerator()
    {
        Console.WriteLine(
            $"Iterator: Starting iteration over {nameof(CompositeWeapon)} components."
        );
        foreach (var component in _components)
        {
            Console.WriteLine($"Iterator: Yielding component {component.ItemType}");
            yield return component;
        }
        Console.WriteLine(
            $"Iterator: Completed iteration over {nameof(CompositeWeapon)} components."
        );
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Aggregates Damage and Health using the Iterator
    private void AggregateStats()
    {
        int aggregatedDamage = 0;
        int aggregatedHealth = 0;

        Console.WriteLine($"Aggregating stats for {nameof(CompositeWeapon)} with ID: {Id}");

        foreach (var component in this) // Utilizing the Iterator
        {
            Console.WriteLine(
                $"Aggregating component {component.ItemType} - Damage: {component.Stats.Damage}, Health: {component.Stats.Health}"
            );
            aggregatedDamage += component.Stats.Damage;
            aggregatedHealth += component.Stats.Health;
        }

        Stats.Damage = aggregatedDamage;
        Stats.Health = aggregatedHealth;

        Console.WriteLine(
            $"Aggregated Stats for {nameof(CompositeWeapon)} - Total Damage: {Stats.Damage}, Total Health: {Stats.Health}"
        );
    }

    public IEnumerable<AttackDeclaration> Attack(
        IArenaGrid opponentsArenaGrid,
        int attackingGridItemId
    )
    {
        Console.WriteLine($"{nameof(CompositeWeapon)} is attacking with its components.");

        // Aggregated Damage and Health are already computed
        Console.WriteLine(
            $"Using Aggregated Damage: {Stats.Damage}, Aggregated Health: {Stats.Health}"
        );

        var affectedGridItemList = ItemHelpers.GetAttackedGridItems(
            opponentsArenaGrid,
            attackingGridItemId
        );

        // Create attack declarations based on aggregated damage
        var attackDeclarations = affectedGridItemList.Select(
            x => new AttackDeclaration { GridItemId = x, Damage = Stats.Damage }
        );

        Console.WriteLine($"{nameof(CompositeWeapon)} executed its attack.");
        return attackDeclarations;
    }

    public void Accept(IItemVisitor visitor)
    {
        visitor.Visit(this);
    }
}
