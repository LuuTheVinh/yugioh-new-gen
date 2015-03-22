using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.GameObjects
{
    /// <summary>
    /// Button có các chức năng Selected, Hovered, thực hiện sự kiện.
    /// Muốn tạo sự kiện cho Button click vào thì dùng ButtonEvent. Ví dụ:
    /// testButton.ButtonEvent += new Action(doSomething); 
    /// * hàm doSomething là hàm kiểu void() *
    /// </summary>
    public class Button : MyObject
    {
        private Sprite normalImage;
        private Sprite selectedImage;
        private Sprite hoverImage;

        private bool isHovered = false;
        private bool isSelected = false;
        private bool isRightClick;
        private InputController input;
        #region Properties
        
        public bool Selected 
        {
            get { return isSelected; }
            set { 
                isSelected = value;
                this.Sprite = selectedImage;
            }
        }
        public bool IsRightClick
        {
            get
            {
                return isRightClick;
            }
            set {
                isRightClick = value;
            }
        }
        public bool Hovered {
            get { return isHovered; }
            set 
            {
                this.Rotation = this.Sprite.Rotation;
                isHovered = value;
                this.Sprite = hoverImage;
            }
        }

        #endregion

        public Button(Sprite image)
        {
            this.Sprite = image;
        }
        public Button(Sprite normal, Sprite hover)
        {
            normalImage = normal;
            hoverImage = hover;

            this.Sprite = normalImage;
        }
        public Button(Sprite normal, Sprite hover, Sprite selected)
        {
            normalImage = normal;
            hoverImage = hover;
            selectedImage = selected;

            this.Sprite = normalImage;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //////////////////
            this.Sprite.Draw(spriteBatch);
        }
        
        public override void Update(GameTime gameTime)
        {
            if (input == null)
                input = new InputController();
            base.Update(gameTime);
            //Mouse
            this.CheckMouseUpdate();
            this.UpdateProperties();
        }

        //private void CheckMouseUpdate()
        //{
        //    InputController.getInstance().Begin(); //Begin get input


        //    if (InputController.getInstance().IsLeftClick() && isHovered)
        //    {
        //        Debug.WriteLine(String.Format("{0},{1}", InputController.getInstance().MousePosition.X, InputController.getInstance().MousePosition.Y));
        //        this.isSelected = true;
        //        this.DoButtonEvent();
        //    }
        //    else if (InputController.getInstance().IsLeftClick() && !isHovered)
        //    {
        //        this.isSelected = false;
        //    }


        //    if (this.Sprite.Bound.Contains(InputController.getInstance().MousePosition) && !isHovered)
        //    {
        //        Debug.WriteLine("hovered!");
        //        if (hoverImage != null)
        //        {
        //            this.Sprite = hoverImage;
        //        }
        //        isHovered = true;
        //    }
        //    if (!this.Sprite.Bound.Contains(InputController.getInstance().MousePosition))
        //    {
        //        isHovered = false;
        //        if (normalImage != null)
        //        {
        //            this.Sprite = normalImage;
        //        }
        //        //Debug.WriteLine("no hovered!");
        //        //Debug.WriteLine(Convert.ToString(InputController.getInstance().MousePosition.X) + " " +Convert.ToString(InputController.getInstance().MousePosition.Y));
        //    }
            
        //    if(isSelected)
        //    {
        //        if (selectedImage != null)
        //        {
        //            this.Sprite = selectedImage;
        //        }
        //    }

        //    InputController.getInstance().End(); //End get input
        //}
        private void CheckMouseUpdate()
        {
            input.Begin(); //Begin get input


            if (input.IsLeftClick())
            {
                if (isHovered)
                {
                    Debug.WriteLine(String.Format("{0},{1}", input.MousePosition.X, input.MousePosition.Y));
                    this.isSelected = true;
                    this.DoButtonEvent();
                }
                else
                {
                    this.isSelected = false;
                }
            }

            if (input.IsRightLick())
            {
                if (isHovered)
                {
                    this.isRightClick = true;
                    this.OnRightClick();
                }
                else
                {
                    this.isRightClick = false;
                }
            }

            if (this.Sprite.Bound.Contains(input.MousePosition) && !isHovered)
            {
                Debug.WriteLine("hovered!");
                if (hoverImage != null)
                {
                    this.Sprite = hoverImage;
                }
                isHovered = true;
            }
            if (!this.Sprite.Bound.Contains(input.MousePosition))
            {
                isHovered = false;
                if (normalImage != null)
                {
                    this.Sprite = normalImage;
                }
            }

            if (isSelected)
            {
                if (selectedImage != null)
                {
                    this.Sprite = selectedImage;
                }
            }

            input.End(); //End get input
        }
        public event Action ButtonEvent;
        public event Action RightClick;
        private void DoButtonEvent()
        {
            if (ButtonEvent != null)
                ButtonEvent();
        }
        private void OnRightClick()
        {
            if (RightClick != null)
                RightClick();
        }
        private void UpdateProperties()
        {
            if (normalImage != null && Sprite != null)
            {
                if (normalImage.Position != Sprite.Position)
                    normalImage.Position = Sprite.Position;
                if (normalImage.Rotation != Sprite.Rotation)
                    normalImage.Rotation = Sprite.Rotation;
                if (normalImage.Scale != Sprite.Scale)
                    normalImage.Scale = Sprite.Scale;
                if (normalImage.Origin != Sprite.Origin)
                    normalImage.Origin = Sprite.Origin;
            }

            if (selectedImage != null && Sprite != null)
            {
                if (selectedImage.Position != Sprite.Position)
                    selectedImage.Position = Sprite.Position;
                if (selectedImage.Rotation != Sprite.Rotation)
                    selectedImage.Rotation = Sprite.Rotation;
                if (selectedImage.Scale != Sprite.Scale)
                    selectedImage.Scale = Sprite.Scale;
                if (selectedImage.Origin != Sprite.Origin)
                    selectedImage.Origin = Sprite.Origin;
            }

            if (hoverImage != null && Sprite != null)
            {
                if (hoverImage.Position != Sprite.Position)
                    hoverImage.Position = Sprite.Position;
                if (hoverImage.Rotation != Sprite.Rotation)
                    hoverImage.Rotation = Sprite.Rotation;
                if (hoverImage.Scale != Sprite.Scale)
                    hoverImage.Scale = Sprite.Scale;
                if (hoverImage.Origin != Sprite.Origin)
                    hoverImage.Origin = Sprite.Origin;
            }
        }
    }
}
