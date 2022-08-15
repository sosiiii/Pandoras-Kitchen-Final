
using System;

public interface IOnDeath
{
    Action DeathAction { get; set; }
}