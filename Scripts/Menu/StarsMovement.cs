using Com.IsartDigital.Shmup.EntitySpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.MapSpace
{
    public class StarsMovement : Camera2D
    {
        public override void _Process(float delta)
        {
            Position += new Vector2(2, 0);
        }
    }
}
