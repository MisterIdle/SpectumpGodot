using Godot;
using System;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class CanonLevel : Area2D
    {
        public void _on_Level_body_entered(KinematicBody2D body)
        {
            if (body is Player && StaticLevel.LevelCannos == 1)
            {
                StaticLevel.LevelCannos = StaticLevel.LevelCannos + 1;
                Player.PlayerInstance().UpgradeCanonEffect();
                Player.PlayerInstance().sfxUpgradeShot.Play();
                QueueFree();
            }
            else if (StaticLevel.LevelCannos == 2)
            {
                StaticLevel.LevelCannos = StaticLevel.LevelCannos + 1;
                Player.PlayerInstance().UpgradeCanonEffect();
                Player.PlayerInstance().sfxUpgradeShot.Play();
                QueueFree();
            }
        }
    }
}
