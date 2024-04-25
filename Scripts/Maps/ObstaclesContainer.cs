using Com.IsartDigital.Shmup.MapsSpace;
using Godot;
using System;

public class ObstaclesContainer : Node2D
{
    public override void _PhysicsProcess(float delta)
    {
        if(!Main.isPause)
        {
            Position += new Vector2(-2, 0);
        }
    }
}
