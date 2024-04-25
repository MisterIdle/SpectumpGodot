using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.EntitySpace {

    public class Laser : Area2D
    {
        public void OnLaserBodyEntered(KinematicBody2D body)
        {
            if (body is Player && !Player.isInFeature && !Player.isInGodMode) 
            {
                Player.PlayerInstance().HitEffect();
                Player.player.health = Player.player.health - 5;
            }
        }

        public void OnLaserAreaEntered(Area2D body)
        {
            if(body is PlayerBullet)
            {
                if (body is PlayerBullet)
                {
                    body.QueueFree();
                }
            }
        }
    }
}
