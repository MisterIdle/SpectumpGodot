using Godot;
using System;

namespace Com.IsartDigital.Shmup.PlayerSpace 
{
    public class ShootingLevel : Area2D
    {
        public void OnLevelBodyEntered(KinematicBody2D body)
        {
            if (body is Player && StaticLevel.LevelSho == 1)
            {
                StaticLevel.LevelSho = StaticLevel.LevelSho + 1;
                Player.PlayerInstance().UpgradeEffect();
                Player.PlayerInstance().sfxUpgradeCanons.Play();
                QueueFree();
            } 
            else if (StaticLevel.LevelSho == 2)
            {
                StaticLevel.LevelSho = StaticLevel.LevelSho + 1;
                Player.PlayerInstance().UpgradeEffect();
                Player.PlayerInstance().sfxUpgradeCanons.Play();
                QueueFree();
            }
        }
    }
}

