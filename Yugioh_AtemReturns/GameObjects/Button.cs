﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using Yugioh_AtemReturns.Manager;

namespace Yugioh_AtemReturns.GameObjects
{
    public delegate void ActionSender(object sender);
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
        private InputController inputController;

        #region Properties
        public bool Selected 
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (isSelected)
                {
                    if (selectedImage != null)
                    {
                        SelectedImage.CopyAnimation(Sprite);
                        this.Sprite = selectedImage;
                    }
                        
                }
                else
                {
                    if (normalImage != null)
                    {
                        NormalImage.CopyAnimation(Sprite);
                        this.Sprite = normalImage;
                    }
                }
                
            }
        }
        public bool CanSelect { set; get; }
        public bool Clicked { set; get; }
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
                if (value == true)
                {
                    HoverImage.CopyAnimation(Sprite);
                    this.Sprite = hoverImage;
                }
                else // ADD
                {
                    if (normalImage != null)
                    {
                        normalImage.CopyAnimation(Sprite);
                        this.Sprite = normalImage;
                    }
                }
            }
        }
        public Sprite NormalImage { get { return normalImage; } }
        public Sprite SelectedImage
        {
            get
            {
                return selectedImage ?? normalImage;
            }
        }
        public Sprite HoverImage
        {
            get
            {
                return hoverImage ?? normalImage;
            }
        }
        public string Tag { get; set; }
        public bool Enable { get; set; }
        #endregion

        public Button(Sprite image)
        {
            this.Sprite = image;
            this.CanSelect = false;
            this.isSelected = false;
            this.Enable = true;
        }
        public Button(Sprite normal, Sprite hover)
        {
            normalImage = normal;
            hoverImage = hover;

            this.Sprite = normal;
            this.CanSelect = false;
            this.isSelected = false;
            this.Enable = true;
        }
        public Button(Sprite normal, Sprite hover, Sprite selected)
        {
            normalImage = normal;
            hoverImage = hover;
            selectedImage = selected;

            this.Sprite = normal;
            this.CanSelect = true;
            this.isSelected = false;
            this.Enable = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //////////////////
            this.Sprite.Draw(spriteBatch);
        }
        
        public override void Update(GameTime gameTime)
        {
            if (inputController == null)
                inputController = new InputController();
            //base.Update(gameTime);

            //Mouse
            this.UpdateProperties();

            //Enable mới kt chuột
            if(Enable)
                this.CheckMouseUpdate();
            
            base.Update(gameTime);
        }

        private void CheckMouseUpdate()
        {
            inputController.Begin(); //Begin get inputController

            this.Clicked = false;

            //LEFT CLICK
            if (inputController.IsLeftClick())
            {
                if (isHovered)
                {
                    Debug.WriteLine(String.Format("{0},{1}", inputController.MousePosition.X, inputController.MousePosition.Y));
                    
                    if(this.CanSelect)
                        this.isSelected = true;
                    
                    this.Clicked = true;
                    this.DoButtonEvent();
                    this.DoButtonEventWithSender(this);
                }
                else
                {
                    if (this.CanSelect)
                        this.isSelected = false;
                }
            }

            //RIGHT CLICK
            if (inputController.IsRightLick())
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

            if (this.Sprite.Bound.Contains(inputController.MousePosition) && !isHovered)
            {
                if (hoverImage != null)
                {
                    HoverImage.CopyAnimation(Sprite);
                    this.Sprite = HoverImage;
                }
                isHovered = true;
            }

            if (!this.Sprite.Bound.Contains(inputController.MousePosition) && isHovered)
            {
                isHovered = false;
                if (normalImage != null)
                {
                    NormalImage.CopyAnimation(Sprite);
                    this.Sprite = NormalImage;
                }
            }

            if (isSelected && this.CanSelect)
            {
                if (selectedImage != null)
                {
                    SelectedImage.CopyAnimation(Sprite);
                    this.Sprite = SelectedImage;
                }
                else if (normalImage != null)
                {
                    NormalImage.CopyAnimation(Sprite);
                    this.Sprite = NormalImage;
                }
            }

            inputController.End(); //End get inputController
        }

        public event Action ButtonEvent;
        public event Action RightClick;
        public event ActionSender ButtonEventWithSender;

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
        private void DoButtonEventWithSender(object sender)
        {
            if (ButtonEventWithSender != null)
                ButtonEventWithSender(sender);
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
