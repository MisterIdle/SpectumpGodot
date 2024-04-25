using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.MapsSpace;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class EnemyBullet : Area2D
    {
        public static int shootSpeed;

        [Export] private NodePath selfPath;
        [Export] private NodePath posPath;
        [Export] private NodePath posExploPath;

        public static Area2D self;
        private Position2D posCanons;
        private Position2D posExplosion;

        [Export] PackedScene particule;

        public override void _Ready()
        {
            self = GetNode<Area2D>(selfPath);
            posCanons = GetNode<Position2D>(posPath);
            posExplosion = GetNode<Position2D>(posExploPath);
        }

        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                GlobalPosition += GlobalPosition.DirectionTo(posCanons.GlobalPosition) * -shootSpeed;
                LookAt(posCanons.GlobalPosition);

                if (GlobalPosition.x <= -900 || GlobalPosition.x >= Cam.screen.x + 900 ||
                    GlobalPosition.y <= -200 || GlobalPosition.y >= Cam.screen.y + 900)
                {
                    QueueFree();
                }
            }
        }

        public void OnBulletEnemyBodyPlayerEntered(KinematicBody2D body)
        {
            if (body is Player && !Player.isInFeature && !Player.isInGodMode)
            {
                Player.PlayerInstance().HitEffect();
                Player.player.health--;
                QueueFree();
            }
        }

        public void OnBulletBodyEntered(StaticBody2D body)
        {
            if (Player.isInFeature || Player.isInGodMode)
            {
                return;
            }

            Main.DeathAnim(posExplosion, particule);
            QueueFree();
        }

        public void OnBulletAreaEntered(Area2D body)
        {
            if (body == Beam.area)
            {
                Main.DeathAnim(posExplosion, particule);
                QueueFree();
            }
        }
    }
}