﻿using System;
using Melontilla.Models;

namespace Melontilla
{
    /// <summary>
    /// Tells Utilla that a plugin wants to use attributes for room join callbacks.
    /// </summary>
    /// <remarks>
    /// This attribute does nothing by itself.
    /// This attribute must be applied to the class marked with [BepInPlugin].
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ModdedGamemodeAttribute : Attribute
    {
        public readonly Gamemode gamemode;

        public ModdedGamemodeAttribute()
        {
            gamemode = null;
        }

        public ModdedGamemodeAttribute(string id, string displayName, BaseGamemode baseGamemode = BaseGamemode.Infection)
        {
            gamemode = new Gamemode(id, displayName, baseGamemode);
        }

        public ModdedGamemodeAttribute(string id, string displayName, Type gameManager)
        {
            gamemode = new Gamemode(id, displayName, gameManager);
        }
    }
}
