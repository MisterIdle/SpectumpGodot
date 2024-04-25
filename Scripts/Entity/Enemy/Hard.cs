using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.EntitySpace
{
    public class Hard : EntityManager
    {
        public bool isDeath = false;
        public bool isLoad = false;
        public bool shoting = false;
        bool shot = false;
        bool lastphase = false;

        [Export] NodePath timerPath;
        [Export] NodePath canonsPoly;

        Timer timer;
        Polygon2D canons;

        [Export] float angle;
        [Export] float radius;

        int randSummon;

        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                Activator();

                if (!isLoad)
                {
                    timer = GetNode<Timer>(timerPath);
                    canons = GetNode<Polygon2D>(canonsPoly);

                    isLoad = true;
                }

                if (!isDeath && isLoad && canMove)
                {
                    randSummon = rand.RandiRange(0, 5);
                    rand.Randomize();
                    EntityMove();
                    Place();
                    angle += 0.1f;

                    if (lastphase)
                    {
                        shot = true;
                    }
                }
                else if (isDeath)
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

        public void EntityMove()
        {
            float y = radius * Mathf.Sin(angle);

            if (one && canMove)
            {
                Position += new Vector2(speed, y);
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

        public void OnTimerShotTimeout()
        {
            if (!Main.isPause)
            {
                if (canMove)
                {
                    ShotStart();
                    ShotHard();
                }
            }
        }

        public void ShotStart()
        {
            tween.InterpolateProperty(canons, "offset", -canons.Offset, new Vector2(-1, canons.Offset.y), 0.2f, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();
        }

        public void OnTimerChanceTimeout()
        {
            if (!Main.isPause)
            {
                if (shot)
                {
                    shot = false;
                    radius = rand.RandfRange(-10, -10);
                    angle = rand.RandfRange(-10, 10);
                    Position += new Vector2(-speed, 0);
                }
                else if (!shot)
                {
                    shot = true;
                    radius = 0;
                    angle = 0;
                    Position += new Vector2(speed, 0);
                }
            }
        }


        public void Health()
        {
            Color lBreak = new Color("ff0000");
            Color lBreakOne = new Color("ff9b00");
            Color lBreakTwo = new Color("ff0000");

            if (health <= 150)
            {
                ext.Modulate = lBreakOne;
                Main.DeathAnim(pos, paricule);
            }

            if (health <= 100)
            {
                ext.Modulate = lBreakTwo;
                Main.DeathAnim(pos, paricule);
            }

            if (health <= 50)
            {
                inte.Modulate = lBreak;
                Main.DeathAnim(pos, paricule);
                lastphase = true;
            }

            if (health <= 0)
            {
                Player.PlayerInstance().SfxDeathEnemy();
                Main.DeathAnim(pos, paricule);
                isDeath = true;
            }
        }
    }
}
