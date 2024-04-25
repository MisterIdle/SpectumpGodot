using Godot;
using System;

public class Transi : Node2D
{
    [Export] NodePath tweenPath;
    Tween tween;

    public override void _Ready()
    {
        tween = GetNode<Tween>(tweenPath);
        Animation();
    }

    public void OnTimerTimeout()
    {
        GetTree().ChangeScene("res://Scenes/Menu/TitleScreen.tscn");
    }

    public void Animation()
    {
        tween.InterpolateProperty(this, "scale", Scale, new Vector2(1.5f, 1.5f), 1.2f, Tween.TransitionType.Bounce, Tween.EaseType.Out); ;
        tween.Start();
    }
}
