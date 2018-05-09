﻿using System.Windows;
using System.Windows.Input;

namespace InvasionOfAldebaran.Models
{
    public class InputHandler
    {
        public bool LeftPressed;
        public bool RightPressed;
        public bool SpacePressed;

        public InputHandler(IInputElement canvas)
        {
            canvas.PreviewKeyDown += this.OnKeyDownHandler;
            canvas.PreviewKeyUp += this.OnKeyUpDownHandler;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    LeftPressed = true;
	                RightPressed = false;
                    break;

                case Key.Right:
                    RightPressed = true;
	                LeftPressed = false;
                    break;

                case Key.Space:
                    SpacePressed = true;
                    break;
            }
        }

        private void OnKeyUpDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
				case Key.Left:
					LeftPressed = false;
					break;

				case Key.Right:
					RightPressed = false;
					break;

				case Key.Space:
                    SpacePressed = false;
                    break;
            }
        }
    }
}