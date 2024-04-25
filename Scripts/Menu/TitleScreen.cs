using Com.IsartDigital.Shmup;
using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;

public class TitleScreen : Control
{
    [Export] public NodePath anglaisPath;
    [Export] public NodePath francePath;
    [Export] public NodePath btnStartPath;
    [Export] public NodePath btnQuitPath;
    [Export] public NodePath btnCreditPath;
    [Export] public NodePath btnBackPath;
    [Export] public NodePath playerPath;
    [Export] public NodePath creditPath;
    [Export] public NodePath HelpOnePath;
    [Export] public NodePath HelpTwoPath;
    [Export] public NodePath btnNextPath;
    [Export] public NodePath btnPlayPath;
    [Export] public NodePath gdpPath;
    [Export] public NodePath msPath;
    [Export] public NodePath titlePath;
    [Export] public NodePath clickPath;

    [Export] public NodePath explicationMovePath;
    [Export] public NodePath explicationShotPath;
    [Export] public NodePath explicationSpeacialPath;

    Button anglais;
    Button france;
    Button btnStart; 
    Button btnQuit; 
    Button btnCredit;
    Button btnBack;
    Button btnNext;
    Button btnPlay;
    Control credit;
    Control helpOne;
    Control helpTwo;
    Node2D player;
    Label gdp;
    Label ms;
    Label explicationMove;
    Label explicationShot;
    Label explicationSpecial;
    Label title;
    AudioStreamPlayer click;

    public override void _Ready()
    {
        helpOne = GetNode<Control>(HelpOnePath);
        helpTwo = GetNode<Control>(HelpTwoPath);
        btnStart = GetNode<Button>(btnStartPath);
        btnQuit = GetNode<Button>(btnQuitPath);
        btnCredit = GetNode<Button>(btnCreditPath);
        btnBack = GetNode<Button>(btnBackPath);
        btnNext = GetNode<Button>(btnNextPath);
        credit = GetNode<Control>(creditPath);
        btnPlay = GetNode<Button>(btnPlayPath);
        anglais = GetNode<Button>(anglaisPath);
        france = GetNode<Button>(francePath);
        gdp = GetNode<Label>(gdpPath);
        ms = GetNode<Label>(msPath);
        explicationMove = GetNode<Label>(explicationMovePath);
        explicationShot = GetNode<Label>(explicationShotPath);
        explicationSpecial = GetNode<Label>(explicationSpeacialPath);
        click = GetNode<AudioStreamPlayer>(clickPath);
        title = GetNode<Label>(titlePath);
        player = GetNode<Node2D>(playerPath);
    }

    public override void _PhysicsProcess(float delta)
    {
        LangManager.Lang();

        btnStart.Text = LangManager.NewGame;
        btnQuit.Text = LangManager.Quit;
        btnCredit.Text = LangManager.Credit;
        btnBack.Text = LangManager.Back;
        btnNext.Text = LangManager.Next;
        btnPlay.Text = LangManager.NewGame;
        france.Text = LangManager.Francais;
        anglais.Text = LangManager.Anglais;
        gdp.Text = LangManager.GDP;
        ms.Text = LangManager.MS;
        explicationMove.Text = LangManager.Move;
        explicationShot.Text = LangManager.Shot;
        explicationSpecial.Text = LangManager.Special;

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
    }
    
    public void _on_PlayGame_pressed()
    {
        GetTree().ChangeScene("res://Scenes/Main.tscn");
        StaticLevel.bomb = 3;
        StaticLevel.LevelCannos = 1;
        StaticLevel.LevelSho = 1;
        StaticLevel.ghost = 0;
    }

    public void _on_Start_pressed()
    {
        france.Visible = false;
        anglais.Visible = false;
        btnCredit.Visible = false;
        btnQuit.Visible = false;
        btnStart.Visible = false;
        player.Visible = false;
        helpOne.Visible = true;
        title.Visible = false;
    }

    public void _on_Skip_pressed()
    {
        helpOne.Visible = false;
        helpTwo.Visible = true;
    }

    public void _on_Credit_pressed()
    {
        btnCredit.Visible = false;
        btnQuit.Visible = false;
        btnStart.Visible = false;
        player.Visible = false;
        btnBack.Visible = true;
        credit.Visible = true;
    }

    public void _on_Back_pressed()
    {
        btnCredit.Visible = true;
        btnQuit.Visible = true;
        btnStart.Visible = true;
        player.Visible = true;
        btnBack.Visible = false;
        credit.Visible = false;
    }

    public void _on_Anglais_pressed()
    {
        LangManager.USA = true;
        LangManager.France = false;
    }

    public void _on_Francais_pressed()
    {
        LangManager.USA = false;
        LangManager.France = true;
    }

    public void _on_Quit_pressed()
    {
        GetTree().Quit();
    }

    public void _on_Start_mouse_entered()
    {
        btnStart.RectScale = RectScale * 1.1f;
        click.Play();
    }

    public void _on_Credit_mouse_entered()
    {
        click.Play();
    }

    public void _on_Quit_mouse_entered()
    {
        click.Play();
    }

    public void _on_Anglais_mouse_entered()
    {
        click.Play();
    }

    public void _on_Francais_mouse_entered()
    {
        click.Play();
    }

    public void _on_Back_mouse_entered()
    {
        click.Play();
    }

    public void _on_Skip_mouse_entered()
    {
        click.Play();
    }

    public void _on_Play_mouse_entered()
    {
        click.Play();
    }

    public void _on_Start_mouse_exited()
    {
        btnStart.RectScale = RectScale * 1f;
    }
}
