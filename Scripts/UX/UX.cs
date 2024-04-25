using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;
using System.Runtime.CompilerServices;

namespace Com.IsartDigital.Shmup.UXSpace
{
    public class UX : Control
    {
        [Export] NodePath selfPath;
        [Export] NodePath playerHealthPath;
        [Export] NodePath playerBombPath;
        [Export] NodePath playerFeaturePath;
        [Export] NodePath xFeaturePath;
        [Export] NodePath scorePath;
        [Export] NodePath tweenPath;
        [Export] NodePath scoreNodePath;
        [Export] NodePath bossHeathPath;

        public static Control self;
        public static ProgressBar playerHealth;
        public static ProgressBar bossHeath;
        public static Label playerBomb;
        public static ProgressBar playerFeature;
        public static Polygon2D xFeature;
        public static Label score;
        public static Tween tween;
        public static Node2D scoreNode;

        public override void _Ready()
        {
            self = GetNode<Control>(selfPath);
            playerHealth = GetNode<ProgressBar>(playerHealthPath);
            playerFeature = GetNode<ProgressBar>(playerFeaturePath);
            xFeature = GetNode<Polygon2D>(xFeaturePath);
            playerBomb = GetNode<Label>(playerBombPath);
            score = GetNode<Label>(scorePath);
            tween = GetNode<Tween>(tweenPath);
            scoreNode = GetNode<Node2D>(scoreNodePath);
            bossHeath = GetNode<ProgressBar>(bossHeathPath);
        }

        public override void _PhysicsProcess(float delta)
        {
            playerHealth.Value = Player.PlayerInstance().health;

            if (Player.isInFeature)
            {
                playerFeature.Value = Player.PlayerInstance().timerFeature.TimeLeft;
            }
            else
            {
                playerFeature.Value = Player.PlayerInstance().timerFeature.WaitTime;
            }

            playerBomb.Text = "x " + StaticLevel.bomb;
            score.Text = "SCORE : " + StaticLevel.highscore;
            bossHeath.Value = Boss.health;
        }
        public static void ScoreEffect()
        {
            tween.InterpolateProperty(scoreNode, "scale", -scoreNode.Scale, new Vector2(1f, 1f), 0.1f, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();
        }
    }
}
