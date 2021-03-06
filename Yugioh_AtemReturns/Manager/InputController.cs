﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yugioh_AtemReturns.Manager
{
    public class InputController
    {
        private KeyboardState newKey;
        private KeyboardState oldKey;
        private MouseState newMouse;
        private MouseState oldMouse;
        private bool isBegin;
        public InputController()
        {
            newKey = new KeyboardState();
            oldKey = new KeyboardState();
            newMouse = new MouseState();
            isBegin = false;
        }

        public bool isKeyPress(Keys _keys)
        {
            this.checkBegin();
            bool result = false;
            if (newKey.IsKeyDown(_keys) && oldKey.IsKeyUp(_keys))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            oldKey = newKey;
            return result;
        }

        public bool IsLeftClick()
        {
            this.checkBegin();
            bool result = false;
            if (newMouse != oldMouse)
            {
                if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool IsLeftRelease()
        {
            this.checkBegin();
            bool result = oldMouse.LeftButton == ButtonState.Released;

            return result;
        }

        public bool IsRightLick()
        {
            this.checkBegin();
            bool result = false;
            if (newMouse != oldMouse)
            {
                if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released)
                {
                    result = true;
                }
            }
            return result;
        }
        public bool IsMiddletLick()
        {
            this.checkBegin();
            bool result = false;
            if (newMouse != oldMouse)
            {
                if (newMouse.MiddleButton == ButtonState.Pressed && oldMouse.MiddleButton == ButtonState.Released)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool IsLeftPress()
        {
            this.checkBegin();
            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public bool IsRightPress()
        {
            this.checkBegin();
            if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public Point MousePosition
        {
            get
            {
                return new Point(Mouse.GetState().X, Mouse.GetState().Y);
            }
        }

        public int DeltaScrollWheelValue
        {
            get
            {
                return newMouse.ScrollWheelValue - oldMouse.ScrollWheelValue;
            }
        }

        public void Begin()
        {
            if (isBegin == true)
            {
                throw new Exception("Begin method can't call before previous end");
            }
            newMouse = Mouse.GetState();
            newKey = Keyboard.GetState();
            isBegin = true;
        }

        public void End()
        {
            if (isBegin == false)
            {
                throw new Exception("End method can't call with out Begin");
            }
            oldKey = newKey;
            oldMouse = newMouse;
            isBegin = false;
        }
        private void checkBegin()
        {
            if (isBegin == false)
            {
                throw new Exception("Can't call this method with out Begin");
            }
        }
    }
}
