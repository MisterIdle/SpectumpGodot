using Com.IsartDigital.Shmup.MapsSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class Cam : Camera2D
    {
        public bool isShake = false;

        float shakeTime = 5f;

        public static Vector2 screen;

        public RandomNumberGenerator rand = new RandomNumberGenerator();

        private static Cam instance;

        public static Cam CamInstance()
        {
            if (instance == null) instance = new Cam();
            return instance;
        }

        public override void _Ready()
        {
            screen = GetViewportRect().Size;

            instance = this;
        }

        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                Position += new Vector2(2, 0);

                if (isShake)
                {
                    Shake();
                }
                else
                    return;
            }
        }

        public void Shake()
        {
            Offset = new Vector2(rand.RandfRange(-1, 1) * shakeTime, rand.RandfRange(-1, 1) * shakeTime);
        }
    }
}
