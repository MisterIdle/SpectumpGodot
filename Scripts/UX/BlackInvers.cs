using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;

public class BlackInvers : ColorRect
{
    [Export] NodePath tweenPath;
    Tween tween;

    public override void _Ready()
    {
        tween = GetNode<Tween>(tweenPath);
        Animation();
        UX.self.Visible = false;
    }

    public void Animation()
    {
        Color color = new Color("000000");

        tween.InterpolateProperty(this, "color", Color, color, 1f); ;
        tween.Start();
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
