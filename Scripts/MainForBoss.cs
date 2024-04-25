using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.MapsSpace
{
    public class MainForBoss : Node2D
    {
        [Export] public NodePath mainMusicPath;
        [Export] public NodePath bulletContainerPath;
        [Export] public NodePath explosionContainerPath;
        [Export] public NodePath sfxGOPath;

        public static AudioStreamPlayer mainMusic;
        public static Node2D bulletContainer;
        public static Node2D explosionContainer;
        public static AudioStreamPlayer sfxGO;

        public RandomNumberGenerator rand = new RandomNumberGenerator();

        public static Color ghost = new Color("74b952ff");
        public static Color normal = new Color("ffffff");

        public static Color wihout = new Color("00000000");
        public static Color with = new Color("69715ef3");

        public static bool isPause = false;

        public override void _Ready()
        {
            UX.bossHeath.Visible = true;
            EnemyBullet.shootSpeed = 15;

            mainMusic = GetNode<AudioStreamPlayer>(mainMusicPath);
            bulletContainer = GetNode<Node2D>(bulletContainerPath);
            explosionContainer = GetNode<Node2D>(explosionContainerPath);
            sfxGO = GetNode<AudioStreamPlayer>(sfxGOPath);
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Input.IsActionJustPressed("ui_pause") && !Player.canp)
            {
                if (isPause)
                {
                    isPause = false;
                    Player.player.timerFeature.Paused = false;
                    Player.player.timerWait.Paused = false;
                    Player.player.timerShot.Paused = false;
                    Player.player.timerLaser.Paused = false;
                    Pause.self.Visible = false;
                    UX.self.Visible = true;
                    mainMusic.VolumeDb = mainMusic.VolumeDb + 10;
                }

                else if (!isPause)
                {
                    isPause = true;
                    Player.player.timerFeature.Paused = true;
                    Player.player.timerWait.Paused = true;
                    Player.player.timerShot.Paused = true;
                    Player.player.timerLaser.Paused = true;
                    Pause.self.Visible = true;
                    UX.self.Visible = false;
                    mainMusic.VolumeDb = mainMusic.VolumeDb - 10;
                }
            }
        }

        public static void DeathAnim(Position2D pPos, PackedScene pParti)
        {
            Node2D part = (Node2D)pParti.Instance();
            part.GlobalPosition = pPos.GlobalPosition;

            explosionContainer.AddChild(part);
        }
    }
}
