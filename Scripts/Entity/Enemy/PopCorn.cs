using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.EntitySpace
{
    public class PopCorn : EntityManager
    {
        public bool isDeath = false;

        int randSummon;

        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                Activator();

                if (!isDeath && canMove)
                {
                    randSummon = rand.RandiRange(0, 5);
                    rand.Randomize();
                    EntityMove();
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

        public void EntityMove()
        {
            Position += new Vector2(-speed, 0);
        }

        public void OnNormalAreaEntered(Area2D body)
        {
            if (body is PlayerBullet)
            {
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

                body.Position += new Vector2(-150, 0);

                Player.player.health--;
                Player.PlayerInstance().HitEffect();
            }
        }

        public void Health()
        {
            if (health <= 0)
            {
                Player.PlayerInstance().SfxDeathEnemy();
                Main.DeathAnim(pos, paricule);
                isDeath = true;
            }
        }
    }
}
