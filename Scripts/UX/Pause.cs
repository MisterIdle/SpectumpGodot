using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.UXSpace
{

    public class Pause : ColorRect
    {
        [Export] NodePath selfPath;

        [Export] NodePath continuPath;
        [Export] NodePath retryPath;
        [Export] NodePath menuPath;
        [Export] NodePath frPath;
        [Export] NodePath enPath;


        public static ColorRect self;
        Button continu;
        Button retry;
        Button menu;
        Button fr;
        Button en;

        const string main = "res://Scenes/Menu/TitleScreen.tscn";
        const string plan = "res://Scenes/Main.tscn";

        public override void _Ready()
        {
            self = GetNode<ColorRect>(selfPath);
            continu = GetNode<Button>(continuPath);
            retry = GetNode<Button>(retryPath);
            menu = GetNode<Button>(menuPath);

            self.Visible = false;
        }

        public override void _PhysicsProcess(float delta)
        {
            continu.Text = LangManager.Continue;
            retry.Text = LangManager.Retry;
            menu.Text = LangManager.Menu;
        }

        public void OnRetryPressed()
        {
            GetTree().ChangeScene(plan);
            StaticLevel.highscore = 0;
            EntityManager.self.score = 0;
            StaticLevel.LevelCannos = 1;
            StaticLevel.LevelSho = 1;
            StaticLevel.bomb = 3;
            StaticLevel.ghost = 0;
        }

        public void OnMenuPressed()
        {
            GetTree().ChangeScene(main);
            StaticLevel.LevelCannos = 1;
            StaticLevel.LevelSho = 1;
            StaticLevel.bomb = 3;
            StaticLevel.ghost = 0;
            EntityManager.self.score = 0;
        }

        public void OnContinueButtonUp()
        {
            Main.isPause = false;
            self.Visible = false;
        }
    }
}
