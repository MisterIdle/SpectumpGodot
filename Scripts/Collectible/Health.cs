using Godot;
using System;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class Health : Area2D
    {
        [Export] NodePath timerPath;
        Timer timer;

        public override void _Ready()
        {
            timer = GetNode<Timer>(timerPath);
            timer.Start();
        }

        public void OnHealthBodyEntered(KinematicBody2D body) 
        {
            if(body is Player)
            {
                Player.player.health = Player.player.health + 2;
                Player.PlayerInstance().HealthEffect();
                Player.PlayerInstance().sfxHealth.Play();
                QueueFree();
            }
        }

        public void OnTimerTimeout()
        {
            QueueFree();
        }
    }
}
