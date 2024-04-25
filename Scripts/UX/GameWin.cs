using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.UXSpace
{

    public class GameWin : ColorRect
    {
        [Export] NodePath selfPath;

        [Export] NodePath retryPath;
        [Export] NodePath menuPath;
        [Export] NodePath winPath;
        [Export] NodePath highPath;

        public static ColorRect self;
        Button retry;
        Button menu;
        Label high;
        Label Win;

        const string main = "res://Scenes/Menu/TitleScreen.tscn";
        const string plan = "res://Scenes/Main.tscn";

        public override void _Ready()
        {
            self = GetNode<ColorRect>(selfPath);
            retry = GetNode<Button>(retryPath);
            menu = GetNode<Button>(menuPath);
            high = GetNode<Label>(highPath);
            Win = GetNode<Label>(winPath);

            self.Visible = false;
        }

        public override void _PhysicsProcess(float delta)
        {
            retry.Text = LangManager.Retry;
            menu.Text = LangManager.Menu;
            high.Text = LangManager.High + StaticLevel.highscore;
            Win.Text = LangManager.GameWin;
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
