﻿namespace FG
{
    public interface IWeapon
    {
        bool Enabled { get; set; }
        string Name { get; }
        float Shoot();
    }
}
