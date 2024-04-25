using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.EntitySpace
{
    public class Walls : EntityManager
    {
        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                RotationDegrees += -1;
            }
        }

        public void OnWallsAreaEntered(Area2D body)
        {
            if (body is PlayerBullet)
            {
                body.QueueFree();

                Player.PlayerInstance().SfxHitEnemy();

                health--;
                Health();
                DpsStart();
            } 
            else if (body == Beam.area)
            {
                Main.DeathAnim(pos, paricule);
                QueueFree();
            }
        }

        public void OnWallsBodyEntered(KinematicBody2D body)
        {
            if (body is Player)
            {
                health--;
                Health();

                body.Position += new Vector2(-100, 0);

                Player.player.health--;
                Player.PlayerInstance().HitEffect();
            }
        }

        public void Health()
        {
            Color lBreakOne = new Color("ffd100");
            Color lBreakTwo = new Color("ff0000");

            if (health <= 2)
            {
                Modulate = lBreakOne;
                Main.DeathAnim(pos, paricule);
            }
            if (health <= 1)
            {
                Modulate = lBreakTwo;
                Main.DeathAnim(pos, paricule);
            }
            if (health <= 0)
            {
                QueueFree();
                Main.DeathAnim(pos, paricule);
            }
        }
    }
}
