using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Com.IsartDigital.Shmup.EntitySpace
{
    public class Boss : Area2D
    {
        public static bool isDeath = false;

        [Export] public PackedScene enemyShot;
        [Export] public PackedScene laserShot;
        [Export] public PackedScene paricule;

        [Export] NodePath bossPath;
        [Export] NodePath canonOnePath;
        [Export] NodePath canonTwoPath;
        [Export] NodePath laserPath;
        [Export] NodePath helpPath;
        [Export] NodePath chargePath;
        [Export] NodePath laserTimerPath;
        [Export] NodePath chanceTimerPath;
        [Export] NodePath canonsTimerPath;
        [Export] NodePath timerShotPath;
        [Export] NodePath timerChangePath;

        [Export] NodePath pos1Path;
        [Export] NodePath pos2Path;
        [Export] NodePath pos3Path;
        [Export] NodePath pos4Path;
        [Export] NodePath tweenPath;

        [Export] NodePath phaseOnePath;
        [Export] NodePath phaseTwoPath;
        [Export] NodePath phaseThreePath;

        [Export] NodePath posHautPath;

        [Export] NodePath warnPath;

        [Export] NodePath posPath;
        [Export] NodePath posLookPath;

        public Area2D boss;
        public Node2D canonOne;
        public Node2D canonTwo;
        public Node2D canonPos;
        
        public Node2D phaseOne;
        public Node2D phaseTwo;
        public Node2D phaseThree;

        public Area2D laser;
        public Particles2D help;
        public Particles2D charge;
        public Position2D pos;
        public Node2D warn;
        public static Timer laserTimer;
        public static Timer chanceTimer;
        public static Timer canonsTimer;
        public static Timer timerShot;
        public static Timer timerChange;

        public Position2D pos1;
        public Position2D pos2;
        public Position2D pos3;
        public Position2D pos4;

        public Position2D posHaut;
        public Tween tween;

        public Position2D posLook;

        private List<Position2D> canonsList1 = new List<Position2D>();
        private List<Position2D> canonsList2 = new List<Position2D>();

        RandomNumberGenerator rand = new RandomNumberGenerator();

        public static float phase = 0f;

        public static float health = 3000;
        //9000 = 2min

        bool wait = false;
        bool waitLaser;
        bool laserPass;
        bool oneShot = false;
        bool CanonUpAndDown = false;
        bool pass1 = false;
        bool pass2 = false;
        public static bool inBoss = false;

        float radius;
        float angle;
        float ampli;

        int lnum = 1;
        int lnum2 = 1;

        [Export] private float X;
        [Export] private float Y;
        [Export] private float YE;
        [Export] private float XE;

        public override void _Ready()
        {
            health = 3000;
            inBoss = true;

            boss = GetNode<Area2D>(bossPath);
            canonOne = GetNode<Node2D>(canonOnePath);
            canonTwo = GetNode<Node2D>(canonTwoPath);
            help = GetNode<Particles2D>(helpPath);
            charge = GetNode<Particles2D>(chargePath);

            pos1 = GetNode<Position2D>(pos1Path);
            pos2 = GetNode<Position2D>(pos2Path);
            pos3 = GetNode<Position2D>(pos3Path);
            pos4 = GetNode<Position2D>(pos4Path);

            posLook = GetNode<Position2D>(posLookPath);

            warn = GetNode<Node2D>(warnPath);
            laserTimer = GetNode<Timer>(laserTimerPath);
            chanceTimer = GetNode<Timer>(chanceTimerPath);
            canonsTimer = GetNode<Timer>(canonsTimerPath);
            timerShot = GetNode<Timer>(timerShotPath);
            timerChange = GetNode<Timer>(timerChangePath);

            posHaut = GetNode<Position2D>(posHautPath);

            phaseOne = GetNode<Node2D>(phaseOnePath);
            phaseTwo = GetNode<Node2D>(phaseTwoPath);
            phaseThree = GetNode<Node2D>(phaseThreePath);

            laser = GetNode<Area2D>(laserPath);
            pos = GetNode<Position2D>(posPath);
            tween = GetNode<Tween>(tweenPath);

            canonsList1.Add(pos1);
            canonsList1.Add(pos2);
            canonsList2.Add(pos3);
            canonsList2.Add(pos4);

            laser.Position = new Vector2(1000, 1000);

            phase = 0;

            Player.sfxBoss1.VolumeDb = -80;
            Player.sfxBoss2.VolumeDb = -80;
            Player.sfxBoss3.VolumeDb = -80;

            phaseOne.Visible = true; phaseTwo.Visible = false; phaseThree.Visible = false;

        }

        public override void _PhysicsProcess(float delta)
        {
            if (!Main.isPause)
            {
                Phase();
                Place();
                Prepa();
                Prepa2();

                if (Input.IsActionJustPressed("1"))
                {
                    health = 2000;
                }

                if (Input.IsActionJustPressed("2"))
                {
                    health = 1000;
                }


                if (Input.IsActionJustPressed("3"))
                {
                    health = 2;
                }

                if (health <= 2000f)
                {
                    if (!pass1)
                    {
                        Player.sfxBoss2.VolumeDb = 0;

                        phase = 1.9f;
                        pass1 = true;
                    }
                }

                if (health <= 1000f)
                {
                    if (!pass2)
                    {
                        Player.sfxBoss2.VolumeDb = 0;

                        phase = 3f;
                        pass2 = true;
                    }
                }

                if (health <= 0)
                {
                    Player.player.sfxExplo.Play();
                    Player.Win = true;

                    Player.isInGodMode = true;
                    QueueFree();
                }
            }

            angle += delta;
            ampli = radius * Mathf.Sin(angle);

            float ampli2 = 5 * Mathf.Sin(angle);
            posLook.Position += new Vector2(0, ampli2);

            float ampli3 = 2f * Mathf.Sin(angle);
            posHaut.Position += new Vector2(0, ampli3);

            for (int i = 0; i < canonsList1.Count; i++)
            {
                canonsList1[i].RotationDegrees = canonTwo.RotationDegrees + 180;
            }
            
            for (int i = 0; i < canonsList2.Count; i++)
            {
                canonsList2[i].RotationDegrees = canonOne.RotationDegrees + 180;
            }
        }
        public void Phase()
        {
            if (phase == 0)
            {

                Cam.CamInstance().isShake = true;

                if (Position.x >= 1060)
                {
                    canonOne.LookAt(Player.player.Position);
                    canonTwo.LookAt(Player.player.Position);
                    Position += new Vector2(-3, 0);
                    UX.bossHeath.Visible = true;
                }
                else
                {
                    phase = 1;

                    Player.sfxBoss1.Play();
                    Player.sfxBoss1.Play();
                    Player.sfxBoss1.Play();

                    Player.sfxBoss1.Autoplay = true;
                    Player.sfxBoss2.Autoplay = true;
                    Player.sfxBoss3.Autoplay = true;

                    Player.sfxBoss1.VolumeDb = 0;
                }
            }

            else if (phase == 1)
            {
                Cam.CamInstance().isShake = false;
                EnemyBullet.shootSpeed = 15;
                chanceTimer.WaitTime = 1;
                Position += new Vector2(0, ampli);

                canonOne.LookAt(Player.player.Position);
                canonTwo.LookAt(Player.player.Position);
                laserPass = false;
            }

            else if (phase == 1.5f)
            {
                if (oneShot == false)
                {
                    Cam.CamInstance().isShake = true;
                    help.Emitting = true;
                    oneShot = true;

                    warn.Visible = true;
                }

                else if (oneShot && !laserPass)
                {
                    Area2D lLaser = (Area2D)laserShot.Instance();
                    lLaser.GlobalPosition = Player.player.Position + new Vector2(-2000, -500);
                    Main.enemyContainer.AddChild(lLaser);

                    laserPass = true;
                    oneShot = false;
                }

                if (laserPass) return;
            }

            if (phase == 2)
            {
                phaseOne.Visible = false; phaseTwo.Visible = true; phaseThree.Visible = false;

                EnemyBullet.shootSpeed = 20;
                RotationDegrees = 0;
                Position += new Vector2(-3, 0);

                if (Position.x < 1090)
                {
                    Cam.CamInstance().isShake = false;
                    phase = 2.1f;
                }
            }

            if (phase == 2.1f)
            {
                canonOne.LookAt(posLook.Position);
                canonTwo.LookAt(posLook.Position);
            }

            if (phase == 2.2f)
            {
                canonOne.LookAt(Player.player.Position);
                canonTwo.LookAt(Player.player.Position);
            }

            if (phase == 2.3f)
            {
                if (oneShot == false)
                {
                    Cam.CamInstance().isShake = true;
                    help.Emitting = true;
                    oneShot = true;
                    warn.Visible = true;
                }

                else if (oneShot && !laserPass)
                {

                    Area2D lLaser = (Area2D)laserShot.Instance();
                    lLaser.GlobalPosition = Player.player.Position + new Vector2(0, -5000);
                    Main.enemyContainer.AddChild(lLaser);

                    laserPass = true;
                    oneShot = false;
                }

                if (laserPass) return;
            }

            if (phase == 3.1f)
            {
                phaseOne.Visible = false; phaseTwo.Visible = false; phaseThree.Visible = true;
                
                EnemyBullet.shootSpeed = 25;
                Position += new Vector2(-3, 0);
                RotationDegrees = 0;
                laser.Position = new Vector2(1000, 1000);
                laserTimer.Autostart = false;
                laserTimer.Stop();

                if (Position.x < 1090)
                {
                    Cam.CamInstance().isShake = false;
                    phase = 3.2f;
                    laser.Position = new Vector2(1000, 1000);
                }
            }

            if (phase == 3.2f)
            {
                laser.Position = new Vector2(0, 0);
                if (!oneShot)
                {
                    if (posHaut.Position.y >= 63)
                    {
                        posHaut.Position += new Vector2(0, -3);
                    }
                } else
                {
                    canonOne.LookAt(posHaut.Position);
                    canonTwo.LookAt(posHaut.Position);

                    Position += new Vector2(0, ampli);
                }
            }

            else if (phase == 3.3f)
            {
                laser.Position = new Vector2(0, 0);

                if (!oneShot)
                {
                    if (posHaut.Position.y >= 440)
                    {
                        posHaut.Position += new Vector2(0, -3);
                    }
                }
                else
                {
                    canonOne.LookAt(posHaut.Position);
                    canonTwo.LookAt(posHaut.Position);

                    Position += new Vector2(0, ampli);
                }
            }
        }


        public void Prepa()
        {
            if (phase == 1.9f && !Main.isPause)
            {
                for (int i = 0; i < 2; i++)
                {
                    Cam.CamInstance().isShake = true;

                    if (lnum == 1)
                    {
                        Position += new Vector2(0, 10);
                        laser.Position = new Vector2(1000, 1000);

                        if (Position.y > 900)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            warn.Visible = true;
                            RotationDegrees = 90;
                            lnum++;
                        }
                    }

                    else if (lnum == 2)
                    {
                        Position += new Vector2(-10, 2);

                        if (Position.x < 0)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            RotationDegrees = 180;
                            lnum++;
                        }
                    }

                    else if (lnum == 3)
                    {
                        Position += new Vector2(0, -10);

                        laser.Position = new Vector2(0, 0);

                        if (Position.y < -200)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            RotationDegrees = -90;
                            laser.Position = new Vector2(1000, 1000);
                            lnum++;
                        }
                    }

                    else if (lnum == 4)
                    {
                        Position += new Vector2(10, -2);
                        RotationDegrees = -90;

                        warn.Visible = false;
                        if (Position.x > 1200)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            lnum++;
                        }
                    }

                    else if (lnum == 5)
                    {
                        if (oneShot)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            phase = 2;
                            Position = new Vector2(1575, 329);
                        }
                        else
                        {
                            RotationDegrees = 0;
                            lnum = lnum - 4;
                            oneShot = true;
                        }
                    }
                }
            }
        }

        public void Prepa2()
        {
            if (phase == 3f && !Main.isPause) 
            {
                for (int i = 0; i < 2; i++)
                {
                    Cam.CamInstance().isShake = true;

                    if (lnum2 == 1)
                    {
                        laser.Position = new Vector2(0, 0);
                        Position += new Vector2(0, 5);
                        warn.Visible = true;

                        if (Position.y > 900)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            RotationDegrees = 90;
                            lnum2++;
                        }
                    }

                    else if (lnum2 == 2)
                    {
                        laser.Position = new Vector2(0, 0);
                        Position += new Vector2(-5, 2);

                        if (Position.x < 0)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            RotationDegrees = 180;
                            lnum2++;
                        }
                    }

                    else if (lnum2 == 3)
                    {
                        laser.Position = new Vector2(0, 0);
                        Position += new Vector2(0, -5);

                        if (Position.y < -200)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            RotationDegrees = -90;
                            lnum2++;
                        }
                    }

                    else if (lnum2 == 4)
                    {
                        laser.Position = new Vector2(0, 0);
                        Position += new Vector2(5, -2);
                        RotationDegrees = -90;

                        if (Position.x > 1200)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            lnum2++;
                        }
                    }

                    else if (lnum2 == 5)
                    {
                        if (oneShot)
                        {
                            Player.PlayerInstance().sfxPreExplo.Play();
                            phase = 3.1f;
                            laser.Position = new Vector2(1000, 1000);
                            Position = new Vector2(1575, 329);
                            warn.Visible = false;
                        }
                        else
                        {
                            RotationDegrees = 0;
                            lnum2 = lnum2 - 4;
                            oneShot = true;
                        }
                    }
                }
            }
        }

        public void OnTimerDoomTimeout()
        {
            if (!Main.isPause) {
                if (phase >= 3.2f)
                {
                    warn.Visible = true;
                    help.Emitting = true;

                    Area2D lLaser = (Area2D)laserShot.Instance();
                    lLaser.GlobalPosition = Player.player.Position + new Vector2(-2000, -500);
                    Main.enemyContainer.AddChild(lLaser);
                }
            }
        }

        public void OnTimerLaserTimeout()
        {
            if (!Main.isPause)
            {
                if (phase >= 2.1f)
                {
                    if (waitLaser)
                    {
                        waitLaser = false;
                        laser.Position = new Vector2(1000, 1000);

                        charge.Emitting = false;

                        if (laserTimer.TimeLeft > 1f)
                        {
                            charge.Emitting = true;
                        }
                    }
                    else
                    {
                        waitLaser = true;
                        laser.Position = new Vector2(0, 0);
                    }
                }

                else if (phase >= 2.3f) laser.Position = new Vector2(1000, 1000);

                else if (phase == 3) return;
            }
        }

        public void OnTimerCanonsTimeout()
        {
            if (!Main.isPause)
            {
                if (phase >= 2.1)
                {
                    if (CanonUpAndDown)
                    {
                        CanonUpAndDown = false;
                    }
                    else
                    {
                        CanonUpAndDown = true;
                    }
                }

                if (phase >= 3) return;
            }
        }

        public void OnTimerRandTimeout()
        {
            radius = rand.RandiRange(-3, 3);
        }

        public void OnTimerShotTimeout()
        {
            if (!Main.isPause)
            {
                if (phase == 1 && wait)
                {
                    ShotBoss();
                }

                if (phase == 2.1f && wait)
                {
                    ShotBoss();
                }

                if (phase == 2.2f && wait)
                {
                    ShotBoss();
                }

                if (phase == 3.2f && wait)
                {
                    ShotBoss();
                }

                if (phase == 3.3f && wait)
                {
                    ShotBoss();
                }

                if (phase == 1.5f)
                    return;
            }
        }

        public void OnTimerChanceTimeout()
        {
            if (!Main.isPause) 
            {
                if (phase == 1 || phase == 2.1f || phase == 2.2f || phase == 3.2f || phase == 3.3f)
                {
                    if (wait)
                    {
                        wait = false;
                    }
                    else
                    {
                        wait = true;
                    }
                }
            }
        }

        public void OnTimerChangeTimeout()
        {
            if (!Main.isPause)
            {
                if (phase == 1)
                {
                    phase = 1.5f;
                }

                else if (phase == 1.5f)
                {
                    warn.Visible = false;
                    Cam.CamInstance().isShake = false;
                    phase = 1f;
                }

                if (phase == 2.1f)
                {
                    phase = 2.2f;
                    oneShot = false;
                    laserPass = false;
                }

                else if (phase == 2.2f)
                {
                    phase = 2.3f;
                }

                else if (phase == 2.3f)
                {
                    phase = 2.1f;
                    Cam.CamInstance().isShake = false;
                    warn.Visible = false;
                }

                if (phase == 3.2f)
                {
                    phase = 3.3f;

                    timerShot.WaitTime = 0.1f;
                    chanceTimer.WaitTime = 0.1f;
                    timerChange.WaitTime = 5f;
                }
                else if (phase == 3.3f)
                {
                    phase = 3.2f;

                    timerShot.WaitTime = 0.1f;
                    chanceTimer.WaitTime = 0.1f;
                    timerChange.WaitTime = 5f;
                }
            }
        }

        public void OnBossBodyEntered(KinematicBody2D body)
        {
            if (body is Player && !Player.isInFeature && !Player.isInGodMode && !Main.isPause)
            {
                health--;

                Player.player.health--;
                Player.PlayerInstance().HitEffect();
            }
        }

        public void OnBossAreaEntered(Area2D body)
        {
            if (phase != 1.9f || phase != 3f && !Main.isPause)
            {
                if (body is PlayerBullet)
                {
                    body.QueueFree();
                    health--;

                    DpsStart();

                    Player.PlayerInstance().SfxHitEnemy();
                }
                else if (body == Beam.area)
                {
                    return;
                }
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

        public void ShotBoss()
        {
            if (!Main.isPause)
            {
                Player.player.sfxBossShot.Play();

                for (int j = 0; j < canonsList1.Count; j++)
                {
                    Area2D lShot = (Area2D)enemyShot.Instance();
                    lShot.RotationDegrees = canonsList1[j].RotationDegrees;

                    lShot.GlobalPosition = canonsList1[j].GlobalPosition;
                    Main.bulletContainer.AddChild(lShot);
                }

                for (int k = 0; k < canonsList2.Count; k++)
                {
                    Area2D lShot = (Area2D)enemyShot.Instance();
                    lShot.RotationDegrees = canonsList2[k].RotationDegrees;

                    lShot.GlobalPosition = canonsList2[k].GlobalPosition;
                    Main.bulletContainer.AddChild(lShot);
                }
            }
        }

        public void Place()
        {
            if (!Main.isPause)
            {
                if (Position.y <= 300)
                {
                    Position += new Vector2(0, 3);
                }

                if (Position.y >= 800)
                {
                    Position += new Vector2(0, -3);
                }
            }
        }
    }
}
