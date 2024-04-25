using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace Com.IsartDigital.Shmup.PlayerSpace
{
    public class Player : KinematicBody2D
    {
        public int multiplicator;

        [Export] public int health = 20;
        [Export] public int speed = 250;
        [Export] private float hitTime = 0.2f;

        [Export] public PackedScene playerBullet;
        [Export] public PackedScene fondu;

        [Export] private NodePath colorPath;

        [Export] private NodePath sfxShotPath;
        [Export] private NodePath sfxHitPath;
        [Export] private NodePath sfxGhostPath;
        [Export] public NodePath sfxDeathEnemyPath;
        [Export] public NodePath sfxHitEnemyPath;
        [Export] public NodePath sfxShotEnemyPath;
        [Export] public NodePath sfxHealthPath;
        [Export] public NodePath sfxUpgradeShotPath;
        [Export] public NodePath sfxUpgradeCanonsPath;
        [Export] public NodePath musicVictoryPath;
        [Export] public NodePath sfxBombPath;
        [Export] public NodePath sfxBossShotPath;

        [Export] public NodePath sfxPreExploPath;
        [Export] public NodePath sfxExploPath;

        [Export] private NodePath timerFeaturePath;
        [Export] private NodePath timerLaserPath;
        [Export] private NodePath timerWaitPath;
        [Export] private NodePath timerShotPath;
        [Export] private NodePath timerLoadPath;

        [Export] public NodePath tweenPath;
        [Export] public NodePath tween2Path;
        [Export] public NodePath particuleSpecialPath;

        [Export] public NodePath canonsContainerPath;
        [Export] public NodePath canonsPolyContainerPath;
        [Export] public NodePath miniShipContainerPath;

        [Export] public NodePath sfxBoss1Path;
        [Export] public NodePath sfxBoss2Path;
        [Export] public NodePath sfxBoss3Path;

        [Export] public NodePath sfxShieldPath;

        [Export] public NodePath levelColorPath;
        [Export] public NodePath godModePath;

        public AudioStreamPlayer sfxShot;
        public AudioStreamPlayer sfxHit;
        public AudioStreamPlayer sfxGhost;
        public AudioStreamPlayer sfxEnemyDeath;
        public AudioStreamPlayer sfxEnemyHit;
        public AudioStreamPlayer sfxEnemyShot;
        public AudioStreamPlayer sfxHealth;
        public AudioStreamPlayer sfxUpgradeShot;
        public AudioStreamPlayer sfxUpgradeCanons;

        public static AudioStreamPlayer sfxBoss1;
        public static AudioStreamPlayer sfxBoss2;
        public static AudioStreamPlayer sfxBoss3;

        public AudioStreamPlayer sfxShield;
        public AudioStreamPlayer sfxBossShot;

        public static AudioStreamPlayer musicVictory;
        public AudioStreamPlayer sfxBomb;

        public AudioStreamPlayer sfxPreExplo;
        public AudioStreamPlayer sfxExplo;

        private Tween tween;
        private Tween tween2;
        private ColorRect rect;

        public Timer timerFeature;
        public Timer timerLaser;
        public Timer timerWait;
        public Timer timerShot;
        public Timer timerLoad;

        public Particles2D particuleSpecial;
        public Polygon2D levelColor;
        public Polygon2D godmod;

        public Node2D canonsContainer;
        public Node2D canonsPolyContainer;
        public Node2D miniShipContainer;

        public const string GoBoss = "res://Scenes/MainForBoss.tscn";

        [Export] public List<AudioStreamOGGVorbis> shotAudioPlayers = new List<AudioStreamOGGVorbis>();
        [Export] public List<AudioStreamOGGVorbis> deathEnemy = new List<AudioStreamOGGVorbis>();

        [Export] public List<Position2D> canonsList = new List<Position2D>();
        [Export] public List<Polygon2D> canonsPolyList = new List<Polygon2D>();

        RandomNumberGenerator rand = new RandomNumberGenerator();

        public static bool isInFeature = false;
        public static bool isInGodMode = false;
        public static bool canShot = true;
        public static bool wait = false;
        public static bool isShot = false;
        public static bool isDeath = false;
        public static bool canp = true;
        public static bool jingleGO = false;
        public static bool Win = false;
        public static bool toBoss = false;
        public static bool OneShot = false;
        public static bool atta = false;
        public static bool canPause = true;
        public static bool isBoss;

        private Vector2 velocity = new Vector2(0, 0);

        float y;
        float langle;

        public static Player player;

        public static Player PlayerInstance()
        {
            if (player == null) player = new Player();
            return player;
        }

        public override void _Ready()
        {
            player = this;

            tween = GetNode<Tween>(tweenPath);
            tween2 = GetNode<Tween>(tween2Path);

            sfxShot = GetNode<AudioStreamPlayer>(sfxShotPath);
            sfxHit = GetNode<AudioStreamPlayer>(sfxHitPath);
            sfxGhost = GetNode<AudioStreamPlayer>(sfxGhostPath);
            sfxEnemyShot = GetNode<AudioStreamPlayer>(sfxShotEnemyPath);
            sfxHealth = GetNode<AudioStreamPlayer>(sfxHealthPath);

            rect = GetNode<ColorRect>(colorPath);

            timerFeature = GetNode<Timer>(timerFeaturePath);
            timerLaser = GetNode<Timer>(timerLaserPath);
            timerWait = GetNode<Timer>(timerWaitPath);
            timerShot = GetNode<Timer>(timerShotPath);
            timerLoad = GetNode<Timer>(timerLoadPath);

            particuleSpecial = GetNode<Particles2D>(particuleSpecialPath);

            levelColor = GetNode<Polygon2D>(levelColorPath);

            canonsContainer = GetNode<Node2D>(canonsContainerPath);
            miniShipContainer = GetNode<Node2D>(miniShipContainerPath);

            canonsPolyContainer = GetNode<Node2D>(canonsPolyContainerPath);

            sfxEnemyDeath = GetNode<AudioStreamPlayer>(sfxDeathEnemyPath);
            sfxEnemyHit = GetNode<AudioStreamPlayer>(sfxHitEnemyPath);

            sfxUpgradeShot = GetNode<AudioStreamPlayer>(sfxUpgradeShotPath);
            sfxUpgradeCanons = GetNode<AudioStreamPlayer>(sfxUpgradeCanonsPath);
            sfxBomb = GetNode<AudioStreamPlayer>(sfxBombPath);
            musicVictory = GetNode<AudioStreamPlayer>(musicVictoryPath);

            sfxBoss1 = GetNode<AudioStreamPlayer>(sfxBoss1Path);
            sfxBoss2 = GetNode<AudioStreamPlayer>(sfxBoss2Path);
            sfxBoss3 = GetNode<AudioStreamPlayer>(sfxBoss3Path);

            sfxShield = GetNode<AudioStreamPlayer>(sfxShieldPath);
            sfxBossShot = GetNode<AudioStreamPlayer>(sfxBossShotPath);

            sfxPreExplo = GetNode<AudioStreamPlayer>(sfxPreExploPath);
            sfxExplo = GetNode<AudioStreamPlayer>(sfxExploPath);

            godmod = GetNode<Polygon2D>(godModePath);

            isInFeature = false;
            isInGodMode = false;
            toBoss = false;
            canShot = true;
            wait = false;
            isShot = false;
            isDeath = false;
            canp = false;
            Win = false;
            OneShot = false;
            atta = false;
            canPause = true;

            Main.isPause = false; GameOver.self.Visible = false;
            FormContainerToList();
        }

        public override void _PhysicsProcess(float delta)
        {
            if (StaticLevel.ghost == 0)
            {
                timerFeature.WaitTime = 0.7f;
            }

            if (StaticLevel.ghost == 1)
            {
                timerFeature.WaitTime = 1; 
            }

            if(health > 20)
            {
                health--;
            } 

            if (StaticLevel.LevelCannos == 3)
            {
                miniShipContainer.Visible = true;
            }

            if (StaticLevel.LevelCannos <= 1)
            {
                miniShipContainer.Visible = false;
            }

            if(Input.IsActionJustPressed("GoToBoss"))
            {
                GetTree().ChangeScene(GoBoss);
                StaticLevel.bomb = 3;
                StaticLevel.LevelCannos = 3;
                StaticLevel.LevelSho = 3;
                StaticLevel.ghost = 1;
            }

            if (toBoss)
            {
                Position += new Vector2(10, 0);
                atta = true;
                canPause = false;
                isBoss = true;
            }

            if(Win)
            {
                if (Position.x >= 1200)
                {
                    Position = new Vector2(-10000, -10000);
                    sfxBoss1.Stop();
                    sfxBoss2.Stop();
                    sfxBoss2.Stop();
                    musicVictory.Play();

                    Cam.CamInstance().isShake = false;
                    UX.self.Visible = false;
                    GameWin.self.Visible = true;
                    StaticLevel.bomb = 3;
                } 
                else
                {
                    Position += new Vector2(10, 0);
                    canPause = false;
                }
            }

            langle += delta;

            y = 1f * Mathf.Sin(langle);

            Levels(); Shot(); Move(delta);
            rand.Randomize();

            if (health <= 0)
            {
                sfxBoss1.Stop();
                sfxBoss2.Stop();
                sfxBoss2.Stop();

                GameOver.self.Visible = true;
                Main.mainMusic.Stop();
                Main.isPause = true;
                UX.self.Visible = false;
                canp = true;
                health = health + 100;
                Main.sfxGO.Play();
                isBoss = false;
                StaticLevel.LevelCannos = 1;
                StaticLevel.LevelSho = 1;
            }

            if (GlobalPosition.y <= 30 && GlobalPosition.x >= Cam.screen.y - 1280)
            {
                Position += new Vector2(0, 2);
            }

            if (GlobalPosition.y >= 670 && GlobalPosition.x <= Cam.screen.y + 1920)
            {
                Position += new Vector2(0, -2);
            }

            if (GlobalPosition.x >= 0 && GlobalPosition.x <= Cam.screen.x - 1270)
            {
                Position += new Vector2(2, 0);
            }

            if (GlobalPosition.x >= +1250 && GlobalPosition.x <= Cam.screen.x - 10)
            {
                Position += new Vector2(-2, 0);
            }
        }

        private void Move(float pTime)
        {
            if (Input.IsActionJustPressed("fullscreen"))
            {
                if (StaticLevel.fulls == true)
                {
                    StaticLevel.fulls = false;
                    OS.WindowFullscreen = false;
                }
                else
                {
                    StaticLevel.fulls = true;
                    OS.WindowFullscreen = true;
                }
            }

            if (!Main.isPause && !Win && !toBoss)
            {
                MoveAndCollide(velocity * pTime);

                velocity = new Vector2(0, 0);

                if (Input.IsActionPressed("ui_right"))
                    velocity.x += 1;

                if (Input.IsActionPressed("ui_left"))
                    velocity.x -= 1;

                if (Input.IsActionPressed("ui_down"))
                    velocity.y += 1;

                if (Input.IsActionPressed("ui_up"))
                    velocity.y -= 1;

                if (Input.IsActionJustPressed("more"))
                    StaticLevel.LevelSho++;

                if (Input.IsActionJustPressed("less"))
                    StaticLevel.LevelSho--;

                if (Input.IsActionJustPressed("mor"))
                    StaticLevel.LevelCannos++;

                if (Input.IsActionJustPressed("les"))
                    StaticLevel.LevelCannos--;

                    velocity = velocity.Normalized() * speed;

                if (Input.IsActionJustPressed("ui_bomb") && !isInFeature)
                {
                    if (!Beam.isInBomb && timerLaser.IsStopped() && StaticLevel.bomb != 0)
                    {
                        canShot = false;
                        particuleSpecial.Emitting = true;

                        Beam.audio.Play();

                        timerLaser.Start();
                        sfxGhost.Play();
                        StaticLevel.bomb--;
                    }
                }

                if (Input.IsActionJustPressed("ui_feature") && !isInFeature && !wait && !Beam.isInBomb)
                {
                    Feature();
                }

                if(Input.IsActionJustPressed("ui_godmode"))
                {
                    if(isInGodMode)
                    {
                        isInGodMode = false;
                        godmod.Color = Colors.White;
                    } 
                    else
                    {
                        isInGodMode = true;
                        godmod.Color = Colors.Gold;
                    }
                }

            }
        }

        public void Shot()
        {
            if (!Main.isPause && !Win && !toBoss)
            {
                if (Input.IsActionPressed("ui_shoot") && canShot && !isShot)
                {
                    int lNum = rand.RandiRange(1, 4);

                    for (int i = 0; i < lNum; i++)
                    {
                        sfxShot.Stream = shotAudioPlayers[i];
                        sfxShot.Play();
                    }

                    CreateLevelShot();
                    timerShot.Start();
                    isShot = true;
                    ShotStart();
                }
            }
        }

        public void OnTimerBossTimeout()
        {
            if (atta)
            {
                if (!OneShot)
                {
                    ColorRect c = (ColorRect)fondu.Instance();
                    Main.explosionContainer.AddChild(c);
                    OneShot = true;
                }

                else if (OneShot)
                {
                    GetTree().ChangeScene(Main.MainBoss);
                }
            }
        }

        public void OnTimerLaserTimeout()
        {
            Beam.isInBomb = true;
        }

        public void OnTimerWaitTimeout()
        {
            if (!Main.isPause && !Win)
            {
                UX.xFeature.Visible = false;
                wait = false;
            }
        }

        public void OnTimerFeatureTimeout()
        {
            if (!Main.isPause && !Win)
            {
                canShot = true;
                Modulate = Main.normal;

                timerWait.Start();
                FeatureEffectStop();
            }
        }

        public void Feature()
        {
            if (!Main.isPause && !Win)
            {
                if (!isInFeature)
                {
                    sfxGhost.Play();

                    Modulate = Main.ghost;
                    isInFeature = true;

                    timerFeature.Start();
                    timerFeature.OneShot = true;

                    canShot = false;
                    FeatureEffectStart();
                }
            }
        }

        public void FeatureEffectStart()
        {
            tween.InterpolateProperty(rect, "color", rect.Color, Main.with, 0.1f, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

        }

        public void FeatureEffectStop()
        {
            UX.xFeature.Visible = true;
            tween.InterpolateProperty(rect, "color", rect.Color, Main.wihout, 0.2f, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            wait = true;
            isInFeature = false;
        }

        public void ShotStart()
        {
            for (int i = 0; i < canonsPolyList.Count; i++)
            {
                tween.InterpolateProperty(canonsPolyList[i], "offset", -canonsPolyList[i].Offset, new Vector2(10, canonsPolyList[i].Offset.y), 0.2f, Tween.TransitionType.Linear, Tween.EaseType.In);
                tween.Start();
            }
        }

        public void HitEffect()
        {
            timerWait.Start();
            timerFeature.Stop();

            Modulate = Main.normal;
            FeatureEffectStop();

            float lDelay = tween.GetRuntime();

            tween.InterpolateProperty(this, "modulate", Modulate, Colors.Red, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            Cam.CamInstance().isShake = true;
            sfxHit.Play();

            float lDelayTwo = tween.GetRuntime();
            tween.InterpolateCallback(this, lDelayTwo, nameof(EndHitEffect));

        }

        private void EndHitEffect()
        {
            tween.InterpolateProperty(this, "modulate", Modulate, Colors.White, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            Cam.CamInstance().isShake = false;

        }

        public void HealthEffect()
        {
            float lDelay = tween.GetRuntime();

            tween.InterpolateProperty(this, "modulate", Modulate, Colors.Green, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            float lDelayTwo = tween.GetRuntime();
            tween.InterpolateCallback(this, lDelayTwo, nameof(EndHitEffect));

        }

        private void HealthEndEffect()
        {
            tween.InterpolateProperty(this, "modulate", Modulate, Colors.White, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();
        }

        public void UpgradeCanonEffect()
        {
            float lDelay = tween.GetRuntime();

            tween.InterpolateProperty(this, "modulate", Modulate, Colors.Black, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            float lDelayTwo = tween.GetRuntime();
            tween.InterpolateCallback(this, lDelayTwo, nameof(UpgradeCanonEndEffect));

        }

        private void UpgradeCanonEndEffect()
        {
            tween.InterpolateProperty(this, "modulate", Modulate, Colors.White, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();
        }

        public void AddBombEffect()
        {
            float lDelay = tween.GetRuntime();

            tween.InterpolateProperty(this, "modulate", Modulate, Colors.DarkBlue, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            float lDelayTwo = tween.GetRuntime();
            tween.InterpolateCallback(this, lDelayTwo, nameof(AddBombEndEffect));

        }

        private void AddBombEndEffect()
        {
            tween.InterpolateProperty(this, "modulate", Modulate, Colors.White, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();
        }

        public void BombEffect()
        {
            float lDelay = tween.GetRuntime();

            tween.InterpolateProperty(this, "modulate", Modulate, Colors.Purple, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            float lDelayTwo = tween.GetRuntime();
            tween.InterpolateCallback(this, lDelayTwo, nameof(BombEndEffect));

        }
        private void BombEndEffect()
        {
            tween.InterpolateProperty(this, "modulate", Modulate, Colors.White, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();
        }

        public void UpgradeEffect()
        {
            float lDelay = tween.GetRuntime();

            tween.InterpolateProperty(this, "modulate", Modulate, Colors.Yellow, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

            float lDelayTwo = tween.GetRuntime();
            tween.InterpolateCallback(this, lDelayTwo, nameof(EndHitEffect));
        }

        private void UpgradeEndEffect()
        {
            tween.InterpolateProperty(this, "modulate", Modulate, Colors.White, hitTime, Tween.TransitionType.Linear, Tween.EaseType.In);
            tween.Start();

        }

        public void FormContainerToList()
        {
            foreach (Position2D item in canonsContainer.GetChildren())
            {
                canonsList.Add(item);
                GD.Print(item);
            }

            foreach (Polygon2D item in canonsPolyContainer.GetChildren())
            {
                canonsPolyList.Add(item);
            }
        }

        public void OnTimerShotTimeout()
        {
            if (isShot)
            {
                isShot = false;
            }
        }

        private void CreateLevelShot()
        {
            for (int i = 0; i < canonsList.Count; i++)
            {
                if (canonsList[i].Visible)
                {
                    if (StaticLevel.LevelSho == 1)
                    {
                        Area2D lB = (Area2D)playerBullet.Instance();
                        lB.GlobalPosition = canonsList[i].GlobalPosition;
                        PlayerBullet.shootSpeed = 20;
                        Main.bulletContainer.AddChild(lB);
                    }

                    if (StaticLevel.LevelSho == 2)
                    {
                        int lnum = 0;

                        for (int k = 0; k < 3; k++)
                        {
                            lnum++;
                            Area2D lB = (Area2D)playerBullet.Instance();
                            PlayerBullet.shootSpeed = 30;

                            lB.GlobalPosition = canonsList[i].GlobalPosition;

                            if (lnum == 1)
                            {
                                lB.RotationDegrees = 0;
                            }

                            if (lnum == 2)
                            {
                                lB.RotationDegrees = 2;
                            }

                            if (lnum == 3)
                            {
                                lB.RotationDegrees = -2;
                            }

                            Main.bulletContainer.AddChild(lB);
                        }
                    }

                    if (StaticLevel.LevelSho == 3)
                    {
                        int lnum = 0;

                        for (int k = 0; k < 5; k++)
                        {
                            lnum++;
                            Area2D lB = (Area2D)playerBullet.Instance();
                            PlayerBullet.shootSpeed = 40;

                            lB.GlobalPosition = canonsList[i].GlobalPosition;

                            if (lnum == 1)
                            {
                                lB.RotationDegrees = 0;
                            }

                            if (lnum == 2)
                            {
                                lB.RotationDegrees = 2;
                            }

                            if (lnum == 3)
                            {
                                lB.RotationDegrees = -2;
                            }

                            if (lnum == 4)
                            {
                                lB.RotationDegrees = 6;
                            }

                            if (lnum == 5)
                            {
                                lB.RotationDegrees = -6;
                            }

                            Main.bulletContainer.AddChild(lB);
                        }
                    }
                }
            }
        }

        public void Levels()
        {
            if (StaticLevel.LevelCannos == 1)
            {
                for (int i = 0; i < canonsList.Count; i++)
                {
                    canonsList[0].Visible = true;
                    canonsPolyList[0].Visible = true;

                    canonsList[1].Visible = false;
                    canonsPolyList[1].Visible = false;
                    
                    canonsList[2].Visible = false;
                    canonsPolyList[2].Visible = false;
                    
                    canonsList[3].Visible = false;
                    canonsPolyList[3].Visible = false;
                    
                    canonsList[4].Visible = false;
                    canonsPolyList[4].Visible = false;
                }
            }

            if (StaticLevel.LevelCannos == 2)
            {
                for (int i = 0; i < canonsList.Count; i++)
                {
                    canonsList[0].Visible = true;
                    canonsPolyList[0].Visible = true;

                    canonsList[1].Visible = true;
                    canonsPolyList[1].Visible = true;

                    canonsList[2].Visible = true;
                    canonsPolyList[2].Visible = true;

                    canonsList[3].Visible = false;
                    canonsPolyList[3].Visible = false;

                    canonsList[4].Visible = false;
                    canonsPolyList[4].Visible = false;
                }
            }

            if (StaticLevel.LevelCannos == 3)
            {
                canonsList[0].Visible = true;
                canonsPolyList[0].Visible = true;

                canonsList[1].Visible = true;
                canonsPolyList[1].Visible = true;

                canonsList[2].Visible = true;
                canonsPolyList[2].Visible = true;

                canonsList[3].Visible = true;
                canonsPolyList[3].Visible = true;

                canonsList[4].Visible = true;
                canonsPolyList[4].Visible = true;

                miniShipContainer.Visible = true;
            }


            if (StaticLevel.LevelSho == 1)
            {
                levelColor.Color = Colors.Green;
                multiplicator = 5;
            }

            if (StaticLevel.LevelSho == 2)
            {
                levelColor.Color = Colors.Orange;
                multiplicator = 10;
            }

            if (StaticLevel.LevelSho == 3)
            {
                levelColor.Color = Colors.Red;
                multiplicator = 15;
            }
        }

        public void SfxDeathEnemy()
        {
            sfxEnemyDeath.Play();
        }

        public void SfxHitEnemy()
        {
            int lNum = rand.RandiRange(1, 4);

            for (int i = 0; i < lNum; i++)
            {
                sfxEnemyHit.Stream = deathEnemy[i];
                sfxEnemyHit.Play();
            }
        }
    }
}
