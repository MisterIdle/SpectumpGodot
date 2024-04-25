using Godot;
using System;

public class Black : ColorRect
{
    [Export] NodePath tweenPath;
    Tween tween;

    public override void _Ready()
    {
        tween = GetNode<Tween>(tweenPath);
        Animation();
    }

    public void Animation()
    {
        Color color = new Color("00000000");

        float lDelay = tween.GetRuntime();

        tween.InterpolateProperty(this, "color", Color, color, 1f); ;
        tween.Start();
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
