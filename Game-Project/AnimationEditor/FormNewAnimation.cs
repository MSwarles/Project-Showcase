﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SuperMetroid.Animations;

namespace AnimationEditor
{
    /// <summary>
    /// Form used to create a new animation.
    /// </summary>
    public partial class FormNewAnimation : Form
    {
        #region Fields
        private Dictionary<string, Animation> m_animationDict;
        #endregion

        #region Properties
        public string AnimationName { get; private set; }
        public bool OKPressed { get; private set; }
        #endregion

        #region Constructor
        public FormNewAnimation(Dictionary<string, Animation> animationDict)
        {
            InitializeComponent();

            m_animationDict = animationDict;
            tbAnimationName.Text = "default";

            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }
        #endregion

        #region Button Event Handlers
        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAnimationName.Text))
            {
                MessageBox.Show("You must enter a name for the animation.", "Missing Animation Name");
                return;
            }

            if (m_animationDict.ContainsKey(tbAnimationName.Text))
            {
                MessageBox.Show("Animation name must be unique!\n", "Duplicate Animation Name");
                return;
            }

            AnimationName = tbAnimationName.Text;
            OKPressed = true;
            this.Close();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            OKPressed = false;
            this.Close();
        }
        #endregion
    }
}