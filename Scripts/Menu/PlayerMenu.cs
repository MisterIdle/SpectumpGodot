using Godot;
using System;

namespace Com.IsartDigital.Shmup.EntitySpace
{
    public class PlayerMenu : Node2D
    {
        [Export] public NodePath pathFollowPath;
        [Export] public float speed;

        public PathFollow2D pathFollow;
        public override void _Ready()
        {
            pathFollow = GetNode<PathFollow2D>(pathFollowPath);
        }

        public override void _Process(float delta)
        {
            pathFollow.Offset += speed;
        }
    }
}

