using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;
using System.Collections.Generic;

namespace Com.IsartDigital.Shmup.EntitySpace
{
    public class EntityManager : Area2D
    {
        [Export] public bool one = true;
        [Export] public bool two = false;
        [Export] public bool la = false;
        [Export] public bool inside = false;
        [Export] public bool canMove = false;

        [Export] public int score;
        [Export] public int health = 3;
        [Export] public float speed = 0;
        [Export] public float rage = 1;

        [Export] public PackedScene healthScene;
        [Export] public PackedScene bombScene;

        [Export] public PackedScene paricule;
        [Export] public PackedScene enemyShot;

        [Export] public NodePath posPath;
        [Export] public NodePath selfPath;
        [Export] public NodePath extPath;
        [Export] public NodePath intPath;
        [Export] public NodePath tweenPath;
        [Export] public NodePath canonsContainerPath;

        public Polygon2D ext;
        public Polygon2D inte;
        public static EntityManager self;
        public Position2D pos;
        public Tween tween;
        public Node2D canonsContainer;

        [Export] public float X;
        [Export] public float Y;
        [Export] public float XE;
        [Export] public float YE;

        public RandomNumberGenerator rand = new RandomNumberGenerator();
        public List<Polygon2D> canonsList = new List<Polygon2D>();

        public override void _Ready()
        {
            canonsContainer = GetNode<Node2D>(canonsContainerPath);
            ext = GetNode<Polygon2D>(extPath);
            inte = GetNode<Polygon2D>(intPath);
            pos = GetNode<Position2D>(posPath);
            self = GetNode<EntityManager>(selfPath);
            tween = GetNode<Tween>(tweenPath);

            rand.Randomize();
            FormContainerToList();
        }

        public void Phase()
        {
            if (two)
            {
                Position += new Vector2(-5f, 0);
                Position += Position.DirectionTo(Player.PlayerInstance().Position) * speed;
            }
        }

        public void DpsStart()
        {
            float lDelay = tween.GetRuntime();

            tween.InterpolateProperty(this, "scale", Scale, new Vector2(X, Y), 0.2f, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            tween.InterpolateCallback(this, lDelay, nameof(DpsStop));
        }

        private void DpsStop()
        {
            tween.InterpolateProperty(this, "scale", Scale, new Vector2(XE, YE), 0.1f, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();
        }

        public void FormContainerToList()
        {
            foreach (Polygon2D item in canonsContainer.GetChildren())
            {
                canonsList.Add(item);
            }
        }

        public void ShotHard()
        {
            if (!Main.isPause)
            {
                for (int i = 0; i < 3; i++)
                {
                    Player.PlayerInstance().sfxEnemyShot.Play();
                    Area2D lShot = (Area2D)enemyShot.Instance();

                    if (rage >= 2)
                    {
                        float deg = rand.RandfRange(-2, 2);
                        lShot.RotationDegrees = RotationDegrees + i * deg;
                        rand.Randomize();
                    }

                    lShot.GlobalPosition = GlobalPosition + new Vector2(-100, 0);
                    Main.bulletContainer.AddChild(lShot);
                }
            }
        }

        public void Place()
        {
            if (!Main.isPause)
            {
                if (Position.y <= 100)
                {
                    Position += new Vector2(0, 3);
                }

                if (Position.y >= 600)
                {
                    Position += new Vector2(0, -3);
                }
            }
        }

        public void Activator()
        {
            if (!Main.isPause) 
            {
                if (Position <= Cam.screen + new Vector2(-10, 0))
                {
                    canMove = true;
                }

                if (Position >= Cam.screen + new Vector2(-10, 0))
                {
                    Position += new Vector2(-2, 0);
                }
            }
        }
    }
}
