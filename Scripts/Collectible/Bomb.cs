using Godot;
using System;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class Bomb : Area2D
    {

        [Export] NodePath timerPath;
        Timer timer;

        public override void _Ready()
        {
            timer = GetNode<Timer>(timerPath);
            timer.Start();
        }

        public void _on_Bomb_body_entered(KinematicBody2D body)
        {
            StaticLevel.bomb = StaticLevel.bomb + 1;
            Player.PlayerInstance().AddBombEffect();
            Player.PlayerInstance().sfxBomb.Play();
            QueueFree();
        }

        public void OnTimerTimeout()
        {
            QueueFree();
        }
    }
}
