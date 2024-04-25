using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.EntitySpace
{
    public class LaserBoss : Area2D
    {
        [Export] NodePath timerPath;
        public static Timer timer;

        public override void _Ready()
        {
            timer = GetNode<Timer>(timerPath);
            timer.Start();
        }

        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                if (Boss.phase == 1.5f)
                {
                    Position += new Vector2(15, 0);
                }
                if (Boss.phase == 2.3f)
                {
                    Position += new Vector2(0, 20);
                }
                if (Boss.phase >= 3.2f)
                {
                    Position += new Vector2(7, 0);
                }

                Clear();
            }
        }

        public void OnLaserBossBodyEntered(KinematicBody2D body)
        {
            if (!Main.isPause)
            {
                if (body is Player && !Player.isInFeature && !Player.isInGodMode)
                {
                    Player.PlayerInstance().HitEffect();
                    Player.player.health = Player.player.health - 5;
                }
            }
        }

        public void OnLaserBossAreaEntered(Area2D body)
        {
            if (!Main.isPause)
            {
                if (body is PlayerBullet)
                {
                    if (body is PlayerBullet)
                    {
                        body.QueueFree();
                    }
                }
            }
        }

        public void OnTimerTimeout()
        {
            if (!Main.isPause)
                QueueFree();
        }

        public void Clear()
        {
            if (!Main.isPause)
            {
                if (Boss.phase == 1.9f)
                {
                    QueueFree();
                }

                if (Boss.phase == 3f)
                {
                    QueueFree();
                }
            }
        }
    }
}