﻿using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace InvasionOfAldebaran.ViewModels
{
    public class MainMenuViewModel : Screen, IScreenViewModel
    {
        public ICommand PlayButtonCommand { get; set; }
        public ICommand CloseButtonCommand { get; set; }
        public string Highscore { get; set; }

        private MediaPlayer mainMenuPlayer;

        private readonly FrameWindowViewModel _frameModel;

        public MainMenuViewModel(FrameWindowViewModel frameModel)
        {
            mainMenuPlayer = new MediaPlayer();
            _frameModel = frameModel;
            mainMenuPlayer.Open(new Uri(@"../../Resources/themesong.mpeg", UriKind.Relative));
            mainMenuPlayer.Play();

            this.PlayButtonCommand = new RelayCommand(this.ChangeWindow);
            this.CloseButtonCommand = new RelayCommand(this.CloseWindow);
            this.Highscore = "HIGHSCORE: " + Convert.ToString(frameModel.Points);
        }

        public void CloseWindow()
        {
            _frameModel.CloseItem(_frameModel);
        }

        public void ChangeWindow()
        {
            _frameModel.ActivateItem(_frameModel.Items.Single(s => s is PlayViewModel));
        }
    }
}