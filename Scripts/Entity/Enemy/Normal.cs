using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Com.IsartDigital.Shmup.EntitySpace
{
    public class Normal : EntityManager 
    {
        bool shooting = true;
        public bool isDeath = false;
        public bool isLoad = false;

        float lAngle;

        [Export] float angle;
        [Export] float radius;

        [Export] NodePath timerPath;
        [Export] NodePath canonOnePath;
        [Export] NodePath canonTwoPath;

        Position2D canonOne;
        Position2D canonTwo;
        Timer timer;

        int randSummon;

        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                Activator();

                if (!isLoad)
                {
                    canonOne = GetNode<Position2D>(canonOnePath);
                    canonTwo = GetNode<Position2D>(canonTwoPath);
                    timer = GetNode<Timer>(timerPath);

                    isLoad = true;
                }

                if (!isDeath && isLoad && canMove)
                {
                    randSummon = rand.RandiRange(0, 5);
                    rand.Randomize();
                    EntityMove();
                    Phase();
                    angle += delta;
                }

                if (isDeath)
                {
                    if (randSummon == 1)
                    {
                        Area2D lHealth = (Area2D)healthScene.Instance();
                        lHealth.GlobalPosition = Player.player.GlobalPosition + new Vector2(500, 0);
                        Main.explosionContainer.AddChild(lHealth);
                    }

                    if (randSummon == 2)
                    {
                        Area2D lBomb = (Area2D)bombScene.Instance();
                        lBomb.GlobalPosition = Player.player.GlobalPosition + new Vector2(500, 0);
                        Main.explosionContainer.AddChild(lBomb);
                    }
                    else
                    {
                        GD.Print("miss");
                    }


                    QueueFree();
                    StaticLevel.highscore = StaticLevel.highscore + score;
                    UX.ScoreEffect();
                }
            }
        }

        public void OnTimerShotTimeout()
        {
            if (!Main.isPause)
            {
                if (canMove && !two)
                {
                    float lTimer = rand.RandfRange(1f, 1.5f);

                    timer.WaitTime = lTimer;

                    ShotStart();
                    ShotNormal();

                    rand.Randomize();
                }
            }
        }

        public void EntityMove()
        {
            float y = radius * Mathf.Sin(angle);
            Position += new Vector2(1, y);

            if (one)
            {
                Position += new Vector2(-3, y);
            }
        }

        public void OnNormalAreaEntered(Area2D body)
        {
            if (body is PlayerBullet)
            {
                body.QueueFree();
                health--;

                Health();
                DpsStart();
                Player.PlayerInstance().SfxHitEnemy();
            }
            else if (body == Beam.area)
            {
                Main.DeathAnim(pos, paricule);
                QueueFree();
            }
        }

        public void OnNormalBodyEntered(KinematicBody2D body)
        {
            if (body is Player && !Player.isInFeature && !Player.isInGodMode)
            {
                health--;
                Health();

                body.Position += new Vector2(-200, 0);

                Player.player.health--;
                Player.PlayerInstance().HitEffect();
            }
        }

        public void Health()
        {
            Color lBreakOne = new Color("8084ff");
            Color lBreakTwo = new Color("ff9b00");
            Color lBreakThree = new Color("ff0000");


            if (health <= 20)
            {
                ext.Modulate = lBreakOne;
                Main.DeathAnim(pos, paricule);
            }
            if (health <= 10)
            {
                ext.Modulate = lBreakTwo;
                Main.DeathAnim(pos, paricule);
            }

            if(health <= 5)
            {
                two = true;
                ext.Modulate = lBreakThree;
                Main.DeathAnim(pos, paricule);
            }

            if (health <= 0)
            {
                Player.PlayerInstance().SfxDeathEnemy();
                Main.DeathAnim(pos, paricule);
                isDeath = true;
            }
        }

        public void ShotNormal()
        {
            Player.PlayerInstance().sfxEnemyShot.Play();

            for (int i = 0; i < 2; i++)
            {
                Area2D lShotOne = (Area2D)enemyShot.Instance();
                Area2D lShotTwo = (Area2D)enemyShot.Instance();

                lShotOne.GlobalPosition = canonOne.GlobalPosition;
                lShotTwo.GlobalPosition = canonTwo.GlobalPosition;

                Main.bulletContainer.AddChild(lShotOne);
                Main.bulletContainer.AddChild(lShotTwo);
            }
        }

        public void ShotStart()
        {
            for (int i = 0; i < canonsList.Count; i++)
            {
                tween.InterpolateProperty(canonsList[i], "offset", -canonsList[i].Offset, new Vector2(-1, canonsList[i].Offset.y), 0.2f, Tween.TransitionType.Linear, Tween.EaseType.In);
                tween.Start();
            }
        }
    }
}
