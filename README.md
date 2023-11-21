# Event Bus for Unity
Design pattern that I use in my projects. It allows you to build an easily scalable project structure, reduces the number of references between components significantly by using the bus as a mediator when exchanging messages (events). One component can send events and the others can receive them. It is designed for Unity, but nothing prevents you from using it in an empty C# environment.

## Example of usage
Define your event data as you like.
```cs
using CptLost.EventBus;
using UnityEngine;

public struct PlayerDamagedEvent : IBusEvent
{
    public int DamageAmount;
    public GameObject Attacker;

    public PlayerDamagedEvent(int damageAmount, GameObject attacker)
    {
        DamageAmount = damageAmount;
        Attacker = attacker;
    }
}
```

Create an event receiver and bind a method to it. You can use two types of method, with and without arguments.
```cs
using CptLost.EventBus;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private EventBusReceiver<PlayerDamagedEvent> _damagedReceiver;

    private void Awake()
    {
        _damagedReceiver = new EventBusReceiver<PlayerDamagedEvent>(OnPlayerDamaged);
        _damagedReceiver.Add(OnPlayerDamagedNoArgs);
    }

    private void OnPlayerDamaged(PlayerDamagedEvent damagedEvent)
    {
        // Do something when the event occurs
    }

    private void OnPlayerDamagedNoArgs()
    {
        // Do something when the event occurs, but without event data
    }
}
```

Invoke your event somewhere.
```cs
using CptLost.EventBus;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public void OnDamage(int damage, GameObject attacker)
    {
        ReceiveDamage(damage);

        EventBus<PlayerDamagedEvent>.Invoke(new PlayerDamagedEvent(damage, attacker));
    }
}
```
## Additional features
Create event receiver from action (don't let it be null).
```cs
using CptLost.EventBus;
using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private EventBusReceiver<PlayerDamagedEvent> _damagedReceiver;
    private Action _onDamageAction;

    private void Awake()
    {
        _damagedReceiver = _onDamageAction;
    }
}
```
## Inspiration
The system was inspired by:
- [BasicEventBus](https://github.com/pointcache/BasicEventBus)
