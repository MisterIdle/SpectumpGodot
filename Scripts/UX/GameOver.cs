using Com.IsartDigital.Shmup;
using Com.IsartDigital.Shmup.EntitySpace;
using Com.IsartDigital.Shmup.MapsSpace;
using Com.IsartDigital.Shmup.PlayerSpace;
using Com.IsartDigital.Shmup.UXSpace;
using Godot;
using System;

namespace Com.IsartDigital.Shmup.UXSpace
{

    public class GameOver : ColorRect
    {
        [Export] NodePath selfPath;
        [Export] NodePath retryPath;
        [Export] NodePath menuPath;
        [Export] NodePath goPath;


        public static ColorRect self;
        Button retry;
        Button menu;
        Label go;

        const string main = "res://Scenes/Menu/TitleScreen.tscn";
        const string plan = "res://Scenes/Main.tscn";

        public override void _Ready()
        {
            self = GetNode<ColorRect>(selfPath);
            go = GetNode<Label>(goPath);
            retry = GetNode<Button>(retryPath);
            menu = GetNode<Button>(menuPath);

            self.Visible = false;
        }

        public override void _PhysicsProcess(float delta)
        {
            retry.Text = LangManager.Retry;
            menu.Text = LangManager.Menu;
            go.Text = LangManager.GameOver;
        }

        public void OnRetryPressed()
        {
            GetTree().ChangeScene(plan);
            StaticLevel.highscore = 0;
            EntityManager.self.score = 0;
            StaticLevel.bomb = 3;
            StaticLevel.LevelCannos = 1;
            StaticLevel.LevelSho = 1;
            StaticLevel.ghost = 0;
        }

        public void OnMenuPressed()
        {
            GetTree().ChangeScene(main);
            StaticLevel.bomb = 3;
            StaticLevel.LevelCannos = 1;
            StaticLevel.LevelSho = 1;
            StaticLevel.ghost = 0;
            EntityManager.self.score = 0;
        }
    }
}
