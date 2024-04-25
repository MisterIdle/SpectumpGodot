using Godot;
using System;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class Ghost : Area2D
    {
        public void _on_Ghost_body_entered(KinematicBody2D body)
        {
            StaticLevel.ghost = StaticLevel.ghost + 1;
            Player.PlayerInstance().BombEffect();
            Player.PlayerInstance().sfxShield.Play();
            QueueFree();
        }
    }
}
