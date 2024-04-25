using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.MapsSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class Beam : RayCast2D
    {
        [Export] public NodePath linePath;
        [Export] public NodePath followPath;
        [Export] public NodePath audioPath;
        [Export] public NodePath areaPath;

        public Line2D line;
        public PathFollow2D follow;
        public static AudioStreamPlayer audio;
        public static Area2D area;

        public float speed = 15;

        public static bool isInBomb = false;

        public override void _Ready()
        {
            line = GetNode<Line2D>(linePath);
            follow = GetNode<PathFollow2D>(followPath);
            audio = GetNode<AudioStreamPlayer>(audioPath);
            area = GetNode<Area2D>(areaPath);
        }

        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                if (isInBomb)
                {
                    follow.Offset += speed;
                    Player.PlayerInstance().Modulate = Main.ghost;
                    Cam.CamInstance().Shake();
                    Player.PlayerInstance().particuleSpecial.Emitting = false;
                }

                if (follow.Offset >= 1650)
                {
                    isInBomb = false;
                    Player.canShot = true;
                    Player.PlayerInstance().Modulate = Main.normal;
                    follow.Offset = 0;
                }
            }
        }
    }
}