using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;

public class BossActivator : Area2D
{
    public override void _PhysicsProcess(float delta)
    {
        if (!Main.isPause)
        {
            Position += new Vector2(-2, 0);
        }
        else
            return;
    }

    public void OnActivatorBodyEntered(KinematicBody2D body)
    {
        if(body is Player)
        {
            Player.toBoss = true;
        }
    }
}
