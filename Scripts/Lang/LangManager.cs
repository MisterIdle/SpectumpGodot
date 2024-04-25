using Godot;
using System;

namespace Com.IsartDigital.Shmup
{
    public static class LangManager
    {
        public static bool USA = false;
        public static bool France = true;

        public static string Anglais = "";
        public static string Francais = "";

        public static string NewGame = "";
        public static string Credit = "";
        public static string Back = "";
        public static string Quit = "";
        public static string Next = "";

        public static string GDP = "";
        public static string MS = "";

        public static string Move = "";
        public static string Shot = "";
        public static string Bomb = "";
        public static string GodMode = "";
        public static string Special = "";

        public static string Continue = "";
        public static string Retry = "";
        public static string Menu = "";

        public static string High = "";

        public static string GameOver = "";
        public static string GameWin = "";

        public static void Lang()
        {
            if (France)
            {
                Anglais = "Anglais";
                Francais = "Français";

                NewGame = "JOUER";
                Credit = "CRÉDITS";
                Back = "RETOUR";
                Quit = "QUITTER";
                Next = "SUITE";

                GDP = "DESIGN DU JEU & PROGRAMMATION";
                MS = "MUSIQUE & CONCEPTION SONORE";

                Move = "DÉPLACEMENT :";
                Shot = "TIRER :";
                Bomb = "BOMBE :";
                GodMode = "MODE DIEU :";
                Special = "FANTOME :";

                Continue = "CONTINUER";
                Retry = "RECOMMENCER";
                Menu = "MENU PRINCIPAL";

                GameOver = "PERDU";
                GameWin = "VICTOIRE";

                High = "MEILLEUR SCORE : ";
            }

            if (USA)
            {
                Anglais = "English";
                Francais = "French";

                NewGame = "PLAY";
                Credit = "CREDITS";
                Back = "BACK";
                Quit = "QUIT";
                Next = "NEXT";

                GDP = "GAME DESIGN & PROGRAMMING";
                MS = "MUSIC & SOUND DESIGN";

                Move = "MOVEMENT :";
                Shot = "SHOOT :";
                Bomb = "BOMB :";
                GodMode = "GOD MODE :";
                Special = "GHOST";

                Continue = "RESUME";
                Retry = "RETRY";
                Menu = "MAIN MENU";

                GameOver = "GAME OVER";
                GameWin = "VICTORY";

                High = "HIGH SCORE : ";
            }
        }

    }
}