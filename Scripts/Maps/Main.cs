using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.MapsSpace
{
    public class Main : Node2D
    {
        [Export] public NodePath mainMusicPath;
        [Export] public NodePath bulletContainerPath;
        [Export] public NodePath enemyContainerPath;
        [Export] public NodePath explosionContainerPath;
        [Export] public NodePath wallContainerPath;
        [Export] public NodePath colContainerPath;
        [Export] public NodePath sfxGOPath;

        public static AudioStreamPlayer mainMusic;
        public static Node2D bulletContainer;
        public static Node2D enemyContainer;
        public static Node2D explosionContainer;
        public static Node2D wallContainer;
        public static Node2D colContainer;
        public static AudioStreamPlayer sfxGO;

        public RandomNumberGenerator rand = new RandomNumberGenerator();

        public static Color ghost = new Color("74b952ff");
        public static Color normal = new Color("ffffff");

        public static Color wihout = new Color("00000000");
        public static Color with = new Color("69715ef3");

        public static bool isPause = false;

        public static string MainBoss = "res://Scenes/MainForBoss.tscn";

        public override void _Ready()
        {
            UX.bossHeath.Visible = false;
            EnemyBullet.shootSpeed = 15;

            mainMusic = GetNode<AudioStreamPlayer>(mainMusicPath);
            bulletContainer = GetNode<Node2D>(bulletContainerPath);
            enemyContainer = GetNode<Node2D>(enemyContainerPath);
            explosionContainer = GetNode<Node2D>(explosionContainerPath);
            colContainer = GetNode<Node2D>(colContainerPath);
            wallContainer = GetNode<Node2D>(wallContainerPath);
            sfxGO = GetNode<AudioStreamPlayer>(sfxGOPath);

            Rand();
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Input.IsActionJustPressed("ui_pause") && !Player.canp && Player.canPause)
            {
                if (isPause)
                {
                    isPause = false;
                    Player.player.timerFeature.Paused = false;
                    Player.player.timerWait.Paused = false;
                    Player.player.timerShot.Paused = false;
                    Player.player.timerLaser.Paused = false;

                    if (Boss.inBoss)
                    {
                        Boss.timerChange.Paused = false;
                        Boss.timerShot.Paused = false;
                        Boss.canonsTimer.Paused = false;
                        Boss.chanceTimer.Paused = false;
                        Boss.laserTimer.Paused = false;
                    }

                    Pause.self.Visible = false;
                    UX.self.Visible = true;
                    mainMusic.VolumeDb = mainMusic.VolumeDb + 10;
                    Player.sfxBoss1.VolumeDb = Player.sfxBoss1.VolumeDb + 10;
                    Player.sfxBoss2.VolumeDb = Player.sfxBoss2.VolumeDb + 10;
                    Player.sfxBoss3.VolumeDb = Player.sfxBoss3.VolumeDb + 10;

                }

                else if (!isPause)
                {
                    isPause = true;
                    Player.player.timerFeature.Paused = true;
                    Player.player.timerWait.Paused = true;
                    Player.player.timerShot.Paused = true;
                    Player.player.timerLaser.Paused = true;


                    if (Boss.inBoss)
                    {
                        Boss.timerChange.Paused = true;
                        Boss.timerShot.Paused = true;
                        Boss.canonsTimer.Paused = true;
                        Boss.chanceTimer.Paused = true;
                        Boss.laserTimer.Paused = true;
                        LaserBoss.timer.Paused = true;
                    }

                    Pause.self.Visible = true;
                    UX.self.Visible = false;
                    mainMusic.VolumeDb = mainMusic.VolumeDb - 10;
                    Player.sfxBoss1.VolumeDb = Player.sfxBoss1.VolumeDb - 10;
                    Player.sfxBoss2.VolumeDb = Player.sfxBoss2.VolumeDb - 10;
                    Player.sfxBoss3.VolumeDb = Player.sfxBoss3.VolumeDb - 10;
                }
            }

            if (Input.IsActionJustPressed("8") && !Player.canp)
            {
                GetTree().ChangeScene(MainBoss);
            }
        }

        public static void DeathAnim(Position2D pPos, PackedScene pParti)
        {
            Node2D part = (Node2D)pParti.Instance();
            part.GlobalPosition = pPos.GlobalPosition;

            explosionContainer.AddChild(part);
        }

        public void Rand()
        {
            foreach (Area2D block in wallContainer.GetChildren())
            {
                block.RotationDegrees = rand.RandfRange(1, 361);
            }
        }
    }
}
