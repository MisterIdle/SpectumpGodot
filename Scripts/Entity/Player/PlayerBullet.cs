using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.MapsSpace;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class PlayerBullet : Area2D
    {
        public static int shootSpeed;

        [Export] private NodePath selfPath;
        [Export] private NodePath posPath;
        [Export] private NodePath posExploPath;

        [Export] PackedScene paricule;

        public static Area2D self;
        private Position2D posCanons;
        public Position2D posExplosion;

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
                GlobalPosition += GlobalPosition.DirectionTo(posCanons.GlobalPosition) * shootSpeed;
                LookAt(posCanons.GlobalPosition);

                if (GlobalPosition.y <= 0 && GlobalPosition.x >= Cam.screen.y - 1280)
                {
                    QueueFree();
                }

                if (GlobalPosition.y >= 660 && GlobalPosition.x <= Cam.screen.y + 1920)
                {
                    QueueFree();
                }

                if (GlobalPosition.x >= 0 && GlobalPosition.x <= Cam.screen.x - 1270)
                {
                    QueueFree();

                }

                if (GlobalPosition.x >= +1240 && GlobalPosition.x <= Cam.screen.x - 0)
                {
                    QueueFree();
                }
            }
        }

        public void OnBulletBodyEntered(StaticBody2D body)
        {
            Main.DeathAnim(posExplosion, paricule);
            QueueFree();
        }

        public void OnBulletAreaEntered(Area2D body)
        {
            if (body is EntityManager)
            {
                Main.DeathAnim(posExplosion, paricule);

                EntityManager.self.health--;
                QueueFree();
            }
        }
    }
}