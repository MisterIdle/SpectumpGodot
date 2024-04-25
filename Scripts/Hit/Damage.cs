using Godot;
using System;

public class Damage : Node2D
{
    [Export] NodePath selfPath;
    Particles2D self;

    public override void _Ready()
    {
        self = GetNode<Particles2D>(selfPath);

        self.Emitting = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        Emit();
    }

    public void Emit()
    {
        if(!self.Emitting)
        {
            QueueFree();
        }
    }
}
