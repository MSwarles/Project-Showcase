using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using GDIColor = System.Drawing.Color;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid;
using SuperMetroid.Animations;

using MyXMLData.AnimationClasses;

namespace AnimationEditor
{
    /// <summary>
    /// Main form used for creating new animations, editing animations, and saving animations.
    /// </summary>
    public partial class FormMain : Form
    {
        #region TODO List
        // fix scaling issues (clicking, size of rows / columns)
        // add scroll bar functionality
        // add transitions tab (for sound triggers)
        // add animation controls for preview (play/pause, move single frame forward/backward)
        // implement command pattern (for undo/redo)
        #endregion

        #region Fields
        private SpriteBatch spriteBatch;
        private List<string> m_debugTextList = new List<string>();
        private Dictionary<string, Animation> m_animationDict = new Dictionary<string, Animation>();
        private AnimationGroupData m_animationGroupData = new AnimationGroupData();

        private List<int> frameRemovalList = new List<int>();

        private Camera m_animationCamera;
        private Camera m_previewCamera;
        private List<float> m_ZoomList;

        private Texture2D m_animationTexture;
        private Texture2D m_pixelTexture;

        private Color m_mapBackdropColor = Color.DimGray;
        private Color m_gridColor = new Color(Color.Black, .1f);
        private Color m_gridThickColor = new Color(Color.Black, .2f);
        private int m_gridLineXFrequency;
        private int m_gridLineXThickFrequency;
        private int m_gridLineYFrequency;
        private int m_gridLineYThickFrequency;

        private Point m_mouse = new Point();
        private bool m_isLeftMouseDown = false;
        private bool m_isRightMouseDown = false;
        private bool m_trackMouse = false;

        private int m_currentFrame;
        private int m_timerFrameCount;

        private int m_lastTick;
        private int m_lastFrameRate;
        private int m_frameRate;

        private bool m_switchingFrames;
        #endregion

        #region Properties
        private GraphicsDevice GraphicsDevice { get { return frameDisplay.GraphicsDevice; } }
        private GraphicsDevice GraphicsDevice2 { get { return previewDisplay.GraphicsDevice; } }
        private string CurrentKey { get { return lbAnimations.GetItemText(lbAnimations.SelectedItem); } }

        private int CurrentFrame
        {
            get { return m_currentFrame; }
            set
            {
                m_currentFrame = value;
                currentFrame_ValueChanged();
            }
        }     
        #endregion

        #region Constructor

        public FormMain()
        {
            InitializeComponent();

            #region Form Events
            this.Load += new EventHandler(FormMain_Load);
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
            this.KeyDown += new KeyEventHandler(FormMain_KeyDown);
            this.MouseWheel += new MouseEventHandler(FormMain_MouseWheel);
            #endregion

            #region File Menu Events
            newAnimationGroupToolStripMenuItem.Click += new EventHandler(newAnimationGroupToolStripMenuItem_Click);
            openAnimationGroupToolStripMenuItem.Click += new EventHandler(openAnimationGroupToolStripMenuItem_Click);
            saveAnimationGroupToolStripMenuItem.Click += new EventHandler(saveAnimationGroupToolStripMenuItem_Click);
            saveAsToolStripMenuItem.Click += new EventHandler(saveAnimationGroupToolStripMenuItem_Click);
            exitToolStripMenuItem.Click += new EventHandler(exitToolStripMenuItem_Click);
            #endregion

            #region Edit Menu Events
            #endregion

            #region Animation Menu Events
            newAnimationToolStripMenuItem.Click += new EventHandler(newAnimationToolStripMenuItem_Click);
            removeAnimationToolStripMenuItem.Click += new EventHandler(removeAnimationToolStripMenuItem_Click);
            renameAnimationToolStripMenuItem.Click += new EventHandler(renameAnimationToolStripMenuItem_Click);
            #endregion

            #region Animation ToolStrip Events
            newAnimationToolStripButton.Click += new EventHandler(newAnimationToolStripMenuItem_Click);
            removeAnimationToolStripButton.Click += new EventHandler(removeAnimationToolStripMenuItem_Click);
            animationMoveUpToolStripButton.Click += new EventHandler(animationMoveUpToolStripButton_Click);
            animationMoveDownToolStripButton.Click += new EventHandler(animationMoveDownToolStripButton_Click);
            renameAnimationToolStripButton.Click += new EventHandler(renameAnimationToolStripMenuItem_Click);
            #endregion

            #region Frame Menu Events
            newFrameToolStripMenuItem.Click += new EventHandler(newFrameToolStripMenuItem_Click);
            removeFrameToolStripMenuItem.Click += new EventHandler(removeFrameToolStripMenuItem_Click);
            #endregion

            #region Frame ToolStrip Events
            newFrameToolStripButton.Click += new EventHandler(newFrameToolStripMenuItem_Click);
            removeFrameToolStripButton.Click += new EventHandler(removeFrameToolStripMenuItem_Click);
            frameMoveUpToolStripButton.Click += new EventHandler(frameMoveUpToolStripButton_Click);
            frameMoveDownToolStripButton.Click += new EventHandler(frameMoveDownToolStripButton_Click);
            #endregion

            #region Main Tool Strip Events
            newAnimationGroupToolStripMenuButton.Click += new EventHandler(newAnimationGroupToolStripMenuItem_Click);
            openAnimationGroupToolStripMenuButton.Click += new EventHandler(openAnimationGroupToolStripMenuItem_Click);
            saveAnimationGroupToolStripMenuButton.Click += new EventHandler(saveAnimationGroupToolStripMenuItem_Click);

            zoomInToolStripMenuButton.Click += new EventHandler(zoomInToolStripMenuButton_Click);
            zoomOutToolStripMenuButton.Click += new EventHandler(zoomOutToolStripMenuButton_Click);

            displayGridToolStripButton.Click += new EventHandler(displayGridToolStripButton_Click);          
            #endregion

            #region Frame Display Events
            frameDisplay.OnInitialize += new EventHandler(frameDisplay_OnInitialize);
            frameDisplay.OnDraw += new EventHandler(frameDisplay_OnDraw);
            #endregion

            #region Preview Display Events
            previewDisplay.OnInitialize += new EventHandler(previewDisplay_OnInitialize);
            previewDisplay.OnDraw += new EventHandler(previewDisplay_OnDraw);
            #endregion
        }

        #endregion

        #region Form Main Event Handlers

        void FormMain_Load(object sender, EventArgs e)
        {
            Rectangle viewPort = new Rectangle(
                0,
                0,
                frameDisplay.Width,
                frameDisplay.Height);
            m_animationCamera = new Camera(viewPort);

            Rectangle viewport = new Rectangle(
                0,
                0,
                previewDisplay.Width,
                previewDisplay.Height);
            m_previewCamera = new Camera(viewport);

            controlTimer.Tick += new EventHandler(controlTimer_Tick);
            controlTimer.Interval = 8;

            frameDisplay.SizeChanged += new EventHandler(frameDisplay_SizeChanged);

            cbZoom.SelectedIndexChanged += new EventHandler(zoomToolStripComboBox_SelectedIndexChanged);
            cbZoom.SelectedIndex = (trackBarZoom.Maximum + trackBarZoom.Minimum) / 2 - 1;
            trackBarZoom.ValueChanged += new EventHandler(trackBarZoom_ValueChanged);
            m_ZoomList = new List<float>() { .25f, .3333f, .45f, .60f, .75f, 1f, 1.5f, 2f, 2.5f, 3f, 4f};

            nudGridLineXFrequency.ValueChanged += new EventHandler(nudGridLineXFrequency_ValueChanged);
            m_gridLineXFrequency = (int)nudGridLineXFrequency.Value;
            nudGridLineYFrequency.ValueChanged += new EventHandler(nudGridLineYFrequency_ValueChanged);
            m_gridLineYFrequency = (int)nudGridLineYFrequency.Value;
            nudGridLineXThickFrequency.ValueChanged += new EventHandler(nudGridLineXThickFrequency_ValueChanged);
            m_gridLineXThickFrequency = (int)nudGridLineXThickFrequency.Value;
            nudGridLineYThickFrequency.ValueChanged += new EventHandler(nudGridLineYThickFrequency_ValueChanged);
            m_gridLineYThickFrequency = (int)nudGridLineYThickFrequency.Value;

            lbAnimations.SelectedIndexChanged += new EventHandler(lbAnimations_SelectedIndexChanged);
            nudFrameWidth.ValueChanged += new EventHandler(nudFrameWidth_ValueChanged);
            nudFrameHeight.ValueChanged += new EventHandler(nudFrameHeight_ValueChanged);
            nudFrames.ValueChanged += new EventHandler(nudFrames_ValueChanged);
            checkBoxLoop.CheckedChanged += new EventHandler(checkBoxLoop_CheckChanged);
            checkBoxAdjustXY.CheckedChanged += new EventHandler(checkBoxAdjustXY_CheckChanged);

            nudXOffset.ValueChanged += new EventHandler(nudXOffset_ValueChanged);
            nudYOffset.ValueChanged += new EventHandler(nudYOffset_ValueChanged);
            btnColor.Click += new EventHandler(btnColor_Click);
            nudOpacity.ValueChanged += new EventHandler(nudOpacity_ValueChanged);
            cbSpriteEffects.SelectedIndexChanged += new EventHandler(cbSpriteEffects_SelectedIndexChanged);
            cbSpriteEffects.SelectedIndex = 0;
            nudRotation.ValueChanged += new EventHandler(nudRotation_ValueChanged);
            nudScale.ValueChanged += new EventHandler(nudScale_ValueChanged);

            cbTriggerType.SelectedIndexChanged += new EventHandler(cbTriggerType_SelectedIndexChanged);
            cbTriggerType.SelectedIndex = 0;

            tbTriggerInfo.TextChanged += new EventHandler(tbTriggerInfo_TextChanged);

            CurrentFrame = -1;

            this.Text = "Animation Editor";
        }

        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text.EndsWith("*"))
            {
                var result = MessageBox.Show("Animation is not saved! Are you sure you want to exit?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation);

                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            // New frame hotkey
            if (e.KeyCode == Keys.N && e.Modifiers == (Keys.Control | Keys.Shift) && newFrameToolStripMenuItem.Enabled)
            {
                newFrameToolStripMenuItem_Click(sender, new EventArgs());
            }
            // New animation hotkey
            if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control)
            {
                newAnimationGroupToolStripMenuItem_Click(sender, new EventArgs());
            }
            // Open hotkey
            if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
            {
                openAnimationGroupToolStripMenuItem_Click(sender, new EventArgs());
            }
            // Save hotkey
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                saveAnimationGroupToolStripMenuItem_Click(sender, new EventArgs());
            }
            // Remove frame hotkey
            if (e.KeyCode == Keys.Delete && e.Modifiers == (Keys.Control | Keys.Shift) && removeFrameToolStripButton.Enabled)
            {
                removeFrameToolStripMenuItem_Click(sender, new EventArgs());
            }
            // Zoom in hotkey
            if (e.KeyCode == Keys.Z && cbZoom.Enabled)
            {
                zoomInToolStripMenuButton_Click(sender, new EventArgs());
            }
            // Zoom out hotkey
            if (e.KeyCode == Keys.X && cbZoom.Enabled)
            {
                zoomOutToolStripMenuButton_Click(sender, new EventArgs());
            }
            // Center on mouse hotkey
            if (e.KeyCode == Keys.C && m_trackMouse)
            {
                m_animationCamera.SnapToPosition(new Vector2(
                    m_animationCamera.Position.X + m_mouse.X,
                    m_animationCamera.Position.Y + m_mouse.Y));

                Cursor.Position = new System.Drawing.Point(
                    Cursor.Position.X - (m_mouse.X - (frameDisplay.Width / 2)),
                    Cursor.Position.Y - (m_mouse.Y - (frameDisplay.Height / 2)));
            }
            // Previous frame hotkey
            if (e.KeyCode == Keys.Left && e.Modifiers == Keys.Control ||
                e.KeyCode == Keys.Down && e.Modifiers == Keys.Control)
            {
                if (lbAnimations.SelectedItem == null || lbAnimations.SelectedIndex == -1)
                { }
                else if (CurrentFrame > 0)
                {
                    CurrentFrame--;
                }
            }
            // Next frame hotkey
            if (e.KeyCode == Keys.Right && e.Modifiers == Keys.Control ||
                e.KeyCode == Keys.Up && e.Modifiers == Keys.Control)
            {
                if (lbAnimations.SelectedItem == null || lbAnimations.SelectedIndex == -1)
                { }
                else if (CurrentFrame < m_animationDict[CurrentKey].AnimationFrames.Count - 1)
                {
                    CurrentFrame++;
                }
            }
        }

        void FormMain_MouseWheel(object sender, MouseEventArgs e)
        {
            // e.Delta value is 120;
            if (ModifierKeys == Keys.Control)
            {
                if (e.Delta < 0 && cbZoom.SelectedIndex > 0)
                    cbZoom.SelectedIndex -= 1;
                else if (e.Delta > 0 && cbZoom.SelectedIndex < cbZoom.Items.Count - 1)
                    cbZoom.SelectedIndex += 1;
            }
        }
        /// <summary>
        /// Updates the Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void controlTimer_Tick(object sender, EventArgs e)
        {
            m_timerFrameCount = ++m_timerFrameCount % 16;
            frameDisplay.Invalidate();
            previewDisplay.Invalidate();
            Logic();
        }

        #endregion

        #region File Menu Event Handlers

        void newAnimationGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text.EndsWith("*"))
            {
                var result = MessageBox.Show("Animation is not saved! Discard changes?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation);

                if (result == DialogResult.No)
                    return;
            }

            using (FormNewAnimationGroup frmNewAnimationGroup = new FormNewAnimationGroup())
            {
                frmNewAnimationGroup.ShowDialog();

                if (frmNewAnimationGroup.OKPressed)
                {
                    m_animationGroupData = new AnimationGroupData(
                        new List<string>(),
                        new List<AnimationData>(),
                        frmNewAnimationGroup.GroupName,
                        frmNewAnimationGroup.TextureFilePath);

                    try
                    {
                        Stream stream = new FileStream(m_animationGroupData.TextureFilePath, FileMode.Open, FileAccess.Read);
                        m_animationTexture = Texture2D.FromStream(GraphicsDevice, stream);

                        stream.Close();
                        stream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading file. \n" + ex.Message, "Error reading image");
                        return;
                    }

                    this.Text = "Animation Editor  -  " + m_animationGroupData.GroupName + "*";

                    Rectangle viewPort = new Rectangle(
                        0,
                        0,
                        frameDisplay.Width,
                        frameDisplay.Height);

                    m_animationCamera = new Camera(viewPort);

                    m_animationDict.Clear();
                    lbAnimations.Items.Clear();

                    cbZoom.Enabled = true;

                    nudXOffset.Maximum = m_animationTexture.Width;
                    nudYOffset.Maximum = m_animationTexture.Height;

                    saveAnimationGroupToolStripMenuItem.Enabled = true;
                    saveAnimationGroupToolStripMenuButton.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;

                    animationToolStripMenuItem.Enabled = true;
                    newAnimationToolStripButton.Enabled = true;

                    m_switchingFrames = false;
                }
            }
        }
        void openAnimationGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text.EndsWith("*"))
            {
                var result = MessageBox.Show("Animation is not saved! Discard changes?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation);

                if (result == DialogResult.No)
                    return;
            } 
            
            DialogResult ofResult = ofDialog.ShowDialog();

            if (ofResult != DialogResult.OK)
                return;

            string path = Path.GetDirectoryName(ofDialog.FileName);
            AnimationGroupData animationGroupData = null;

            // read animation data from xml file
            try
            {
                animationGroupData = XnaSerializer.Deserialize<AnimationGroupData>(ofDialog.FileName);

                Stream stream = new FileStream(animationGroupData.TextureFilePath, FileMode.Open, FileAccess.Read);
                m_animationTexture = Texture2D.FromStream(GraphicsDevice, stream);
                m_animationGroupData.GroupName = animationGroupData.GroupName;
                m_animationGroupData.TextureFilePath = animationGroupData.TextureFilePath;

                stream.Close();
                stream.Dispose();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error reading animation!");
                return;
            }

            List<string> animationNames = animationGroupData.AnimationNames.ToList<string>();
            List<AnimationData> animationData = animationGroupData.AnimationData.ToList<AnimationData>();
            m_animationDict.Clear();

            // load animation data into dictionary
            for (int i = 0; i < animationGroupData.AnimationNames.Count(); i++)
            {
                Animation animation = Animation.FromAnimationData(animationData[i]);
                animation.Texture = m_animationTexture;
                m_animationDict.Add(animationNames[i], animation);
            }

            // Set up camera
            cbZoom.Enabled = true;

            // Enable saving
            saveAnimationGroupToolStripMenuItem.Enabled = true;
            saveAnimationGroupToolStripMenuButton.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;

            // Set up Animation
            lbAnimations.Items.Clear();

            foreach (KeyValuePair<string, Animation> entry in m_animationDict)
            {
                lbAnimations.Items.Add(entry.Key);
            }

            if (lbAnimations.Items.Count > 0)
                lbAnimations.SelectedIndex = 0;

            newAnimationToolStripButton.Enabled = true;
            animationToolStripMenuItem.Enabled = true;
            nudFrameWidth.Enabled = true;
            nudFrameHeight.Enabled = true;
            nudFrames.Enabled = true;

            if (m_animationDict.Count() > 0)
            {
                removeAnimationToolStripMenuItem.Enabled = true;
                removeAnimationToolStripButton.Enabled = true;
                renameAnimationToolStripButton.Enabled = true;
            }

            // Set up Frames
            nudXOffset.Maximum = m_animationTexture.Width;
            nudYOffset.Maximum = m_animationTexture.Height;
            frameToolStripMenuItem.Enabled = true;
            newFrameToolStripButton.Enabled = true;
            CurrentFrame = -1;

            this.Text = "Animation Editor  -  " + animationGroupData.GroupName;
        }
        void saveAnimationGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_animationDict == null)
                return;

            List<string> animationNames = new List<string>();
            List<AnimationData> animationData = new List<AnimationData>();
            
            for (int i = 0; i < lbAnimations.Items.Count; i++)
            {
                List<AnimationFrameData> animationFrameData = new List<AnimationFrameData>();
                string currentKey = lbAnimations.GetItemText(lbAnimations.Items[i]);

                foreach (AnimationFrame f in m_animationDict[currentKey].AnimationFrames)
                {
                    AnimationFrameData frame = new AnimationFrameData(
                        f.XOffset,
                        f.YOffset,
                        f.Color,
                        f.Alpha,
                        f.Rotation,
                        f.Scale,
                        f.SpriteEffects,
                        f.MessageType,
                        f.MessageInfo);

                    frame.MessageInfo = f.MessageInfo;
                    frame.MessageType = f.MessageType;

                    animationFrameData.Add(frame);
                }

                animationNames.Add(currentKey);
                animationData.Add(new AnimationData(
                    animationFrameData,
                    m_animationDict[currentKey].FrameWidth,
                    m_animationDict[currentKey].FrameHeight,
                    m_animationDict[currentKey].FramesPerSecond,
                    m_animationDict[currentKey].IsLoop));
            }
           
            AnimationGroupData animationGroupData = new AnimationGroupData(
                animationNames, 
                animationData, 
                m_animationGroupData.GroupName,
                m_animationGroupData.TextureFilePath);

            sfDialog.FileName = animationGroupData.GroupName;
            DialogResult sfResult = sfDialog.ShowDialog();

            if (sfResult != DialogResult.OK)
                return;
            
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(sfDialog.FileName);
                this.Text = "Animation Editor  -  " + fileName  + ".anim";
                m_animationGroupData.GroupName = fileName;
                animationGroupData.GroupName = fileName;

                XnaSerializer.Serialize<AnimationGroupData>(sfDialog.FileName, animationGroupData);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error saving animation!");
            }
        }

        #endregion

        #region Animation Menu Event Handlers

        void newAnimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormNewAnimation frmNewAnimation = new FormNewAnimation(m_animationDict))
            {
                frmNewAnimation.ShowDialog();

                if (frmNewAnimation.OKPressed)
                {                  
                    string animationName = frmNewAnimation.AnimationName;
                    m_animationDict.Add(animationName, new Animation(
                        m_animationTexture,
                        (int)nudFrameWidth.Value,
                        (int)nudFrameHeight.Value,
                        (int)nudFrames.Value,
                        true));

                    lbAnimations.Items.Add(animationName);

                    if (lbAnimations.SelectedItem == null)
                        lbAnimations.SelectedIndex = 0;
                    else
                        lbAnimations.SelectedIndex = lbAnimations.Items.Count - 1;

                    removeAnimationToolStripMenuItem.Enabled = true;
                    renameAnimationToolStripMenuItem.Enabled = true;
                    removeAnimationToolStripButton.Enabled = true;
                    renameAnimationToolStripButton.Enabled = true;

                    frameToolStripMenuItem.Enabled = true;
                    newFrameToolStripButton.Enabled = true;

                    m_animationDict[CurrentKey].Texture = m_animationTexture;

                    if (!this.Text.EndsWith("*"))
                        this.Text += "*";
                }
            }
        }
        void removeAnimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1)
                return;

            var result = MessageBox.Show("Are you sure you want to delete the animation \'" + lbAnimations.Text + "\' ?",
                "Delete Animation",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            int selectedIndex = lbAnimations.SelectedIndex;
                 
            if (m_animationDict.ContainsKey(CurrentKey))
                m_animationDict.Remove(CurrentKey);        
            
            lbAnimations.Items.RemoveAt(selectedIndex);
            lbAnimations.SelectedIndex = -1;

            if (!this.Text.EndsWith("*"))
                this.Text += "*";
        }
        void animationMoveUpToolStripButton_Click(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1)
                return;           

            object tempAnimation = lbAnimations.Items[lbAnimations.SelectedIndex];
            lbAnimations.Items[lbAnimations.SelectedIndex] = lbAnimations.Items[lbAnimations.SelectedIndex - 1];
            lbAnimations.Items[lbAnimations.SelectedIndex - 1] = tempAnimation;
            lbAnimations.SelectedIndex -= 1;
        }
        void animationMoveDownToolStripButton_Click(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1)
                return;

            object tempAnimation = lbAnimations.Items[lbAnimations.SelectedIndex];
            lbAnimations.Items[lbAnimations.SelectedIndex] = lbAnimations.Items[lbAnimations.SelectedIndex + 1];
            lbAnimations.Items[lbAnimations.SelectedIndex + 1] = tempAnimation;
            lbAnimations.SelectedIndex += 1;
        }
        void renameAnimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1)
                return;

            using (FormRenameAnimation frmRenameAnimation = new FormRenameAnimation(
                CurrentKey, 
                m_animationDict))
            {
                frmRenameAnimation.ShowDialog();

                if (frmRenameAnimation.OKPressed)
                {
                    string newName = frmRenameAnimation.AnimationName;

                    Animation tempAnimData = m_animationDict[CurrentKey];
                    m_animationDict.Remove(CurrentKey);
                    m_animationDict.Add(newName, tempAnimData);
                    lbAnimations.Items[lbAnimations.SelectedIndex] = newName;

                    if (!this.Text.EndsWith("*"))
                        this.Text += "*";
                }
            }
        }

        #region Animation Controls
        void lbAnimations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedItem == null || lbAnimations.SelectedIndex == -1)
            {
                removeAnimationToolStripMenuItem.Enabled = false;
                renameAnimationToolStripMenuItem.Enabled = false;
                removeAnimationToolStripButton.Enabled = false;
                renameAnimationToolStripButton.Enabled = false;
                animationMoveUpToolStripButton.Enabled = false;
                animationMoveDownToolStripButton.Enabled = false;

                newFrameToolStripButton.Enabled = false;
                newFrameToolStripMenuItem.Enabled = false;

                CurrentFrame = -1;
                nudFrameWidth.Enabled = false;
                nudFrameHeight.Enabled = false;
                nudFrames.Enabled = false;
                checkBoxLoop.Enabled = false;
                return;
            }

            removeAnimationToolStripMenuItem.Enabled = true;
            renameAnimationToolStripMenuItem.Enabled = true;
            removeAnimationToolStripButton.Enabled = true;
            renameAnimationToolStripButton.Enabled = true;
            animationMoveUpToolStripButton.Enabled = false;
            animationMoveDownToolStripButton.Enabled = false;

            int currentAnimationIndex = lbAnimations.SelectedIndex;

            if (currentAnimationIndex == 0 && lbAnimations.Items.Count > 1)
            {
                animationMoveDownToolStripButton.Enabled = true;
            }
            else if (currentAnimationIndex == lbAnimations.Items.Count - 1)
            {
                animationMoveUpToolStripButton.Enabled = true;
            }
            else
            {
                animationMoveUpToolStripButton.Enabled = true;
                animationMoveDownToolStripButton.Enabled = true;
            }

            newFrameToolStripButton.Enabled = true;
            newFrameToolStripMenuItem.Enabled = true;

            CurrentFrame = -1;
            nudFrameWidth.Enabled = true;
            nudFrameHeight.Enabled = true;
            nudFrames.Enabled = true;
            checkBoxLoop.Enabled = true;
            nudFrameWidth.Value = m_animationDict[CurrentKey].FrameWidth;
            nudFrameHeight.Value = m_animationDict[CurrentKey].FrameHeight;
            nudFrames.Value = m_animationDict[CurrentKey].FramesPerSecond;
        }

        void nudFrameWidth_ValueChanged(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1 || m_switchingFrames)
                return;
            
            m_animationDict[CurrentKey].FrameWidth = (int)nudFrameWidth.Value;

            if (!this.Text.EndsWith("*"))
                this.Text += "*";
        }
        void nudFrameHeight_ValueChanged(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1 || m_switchingFrames)
                return;

            m_animationDict[CurrentKey].FrameHeight = (int)nudFrameHeight.Value;

            if (!this.Text.EndsWith("*"))
                this.Text += "*";
        }
        void nudFrames_ValueChanged(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1 || m_switchingFrames)
                return;

            m_animationDict[CurrentKey].FramesPerSecond = (int)nudFrames.Value;

            if (!this.Text.EndsWith("*"))
                this.Text += "*";
        }

        void checkBoxLoop_CheckChanged(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1 || m_switchingFrames)
                return;

            m_animationDict[CurrentKey].IsLoop = checkBoxLoop.Checked;

            if (!this.Text.EndsWith("*"))
                this.Text += "*";
        }
        void checkBoxAdjustXY_CheckChanged(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedIndex == -1 || m_switchingFrames)
                return;

            if (checkBoxAdjustXY.Checked)
            {
                nudXOffset.Increment = nudFrameWidth.Value;
                nudYOffset.Increment = nudFrameHeight.Value;
            }
            else
            {
                nudXOffset.Increment = 1;
                nudYOffset.Increment = 1;
            }
        }

        #endregion

        #endregion

        #region Frame Menu Event Handlers

        void newFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbAnimations.SelectedItem == null || lbAnimations.SelectedIndex == -1)
                return;

            Color color = new Color(
                btnColor.BackColor.R,
                btnColor.BackColor.G,
                btnColor.BackColor.B);

            m_animationDict[CurrentKey].AnimationFrames.Add(new AnimationFrame(
                (int)nudXOffset.Value,
                (int)nudYOffset.Value,
                color,
                (float)nudRotation.Value * (float)Math.PI / 180,
                (float)nudScale.Value,
                SpriteEffects.None));
                        
            CurrentFrame = m_animationDict[CurrentKey].AnimationFrames.Count() - 1;

            if (!this.Text.EndsWith("*"))
                this.Text += "*";
        }
        void removeFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsValidFrame())
            {
                var result = MessageBox.Show("Are you sure you want to delete Frame " + m_currentFrame + "?",
                "Delete Frame",
                MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                    return;

                frameRemovalList.Add(m_currentFrame);

                if (!this.Text.EndsWith("*"))
                    this.Text += "*";
            }
        }
        void frameMoveUpToolStripButton_Click(object sender, EventArgs e)
        {
            if (IsValidFrame())
            {
                AnimationFrame tempFrame = m_animationDict[CurrentKey].AnimationFrames[m_currentFrame];
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame] = m_animationDict[CurrentKey].AnimationFrames[m_currentFrame - 1];
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame - 1] = tempFrame;
                CurrentFrame -= 1;
            }
        }
        void frameMoveDownToolStripButton_Click(object sender, EventArgs e)
        {
            if (IsValidFrame())
            {
                AnimationFrame tempFrame = m_animationDict[CurrentKey].AnimationFrames[m_currentFrame];
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame] = m_animationDict[CurrentKey].AnimationFrames[m_currentFrame + 1];
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame + 1] = tempFrame;
                CurrentFrame += 1;
            }
        }

        void currentFrame_ValueChanged()
        {
            m_switchingFrames = true;
            if (m_currentFrame == -1)
            {
                removeFrameToolStripMenuItem.Enabled = false;
                removeFrameToolStripButton.Enabled = false;
                frameMoveUpToolStripButton.Enabled = false;
                frameMoveDownToolStripButton.Enabled = false;

                // Set defaults
                nudXOffset.Enabled = false;
                nudXOffset.Value = 0;
                nudYOffset.Enabled = false;
                nudYOffset.Value = 0;
                btnColor.Enabled = false;
                btnColor.BackColor = GDIColor.White;
                nudOpacity.Enabled = false;
                nudOpacity.Value = 1;
                nudRotation.Enabled = false;
                nudRotation.Value = 0;
                nudScale.Enabled = false;
                nudScale.Value = 1;
                cbSpriteEffects.Enabled = false;
                cbSpriteEffects.SelectedIndex = 0;
                cbTriggerType.Enabled = false;
                cbTriggerType.SelectedIndex = 0;
                tbTriggerInfo.Enabled = false;
                tbTriggerInfo.Text = "";
                m_switchingFrames = false;

                return;
            }

            AnimationFrame currentFrame = m_animationDict[CurrentKey].AnimationFrames[m_currentFrame];

            nudXOffset.Enabled = true;
            nudXOffset.Value = currentFrame.XOffset;
            nudYOffset.Enabled = true;
            nudYOffset.Value = currentFrame.YOffset;
            btnColor.Enabled = true;
            btnColor.BackColor = GDIColor.FromArgb(
                currentFrame.Color.R,
                currentFrame.Color.G,
                currentFrame.Color.B);

            nudOpacity.Enabled = true;
            nudOpacity.Value = (decimal)currentFrame.Alpha;
            nudRotation.Enabled = true;
            nudRotation.Value = (decimal)(currentFrame.Rotation * 180 / Math.PI);
            nudScale.Enabled = true;
            nudScale.Value = (decimal)(currentFrame.Scale);

            cbSpriteEffects.Enabled = true;
            if (currentFrame.SpriteEffects == SpriteEffects.None)
                cbSpriteEffects.SelectedIndex = 0;
            else if (currentFrame.SpriteEffects == SpriteEffects.FlipHorizontally)
                cbSpriteEffects.SelectedIndex = 1;
            else if (currentFrame.SpriteEffects == SpriteEffects.FlipVertically)
                cbSpriteEffects.SelectedIndex = 2;

            cbTriggerType.Enabled = true;
            cbTriggerType.SelectedIndex = currentFrame.MessageType;
            tbTriggerInfo.Enabled = true;
            tbTriggerInfo.Text = currentFrame.MessageInfo;

            removeFrameToolStripMenuItem.Enabled = true;
            removeFrameToolStripButton.Enabled = true;
            frameMoveUpToolStripButton.Enabled = false;
            frameMoveDownToolStripButton.Enabled = false;

            if (m_animationDict[CurrentKey].AnimationFrames.Count() == 1)
            {

            } else if (m_currentFrame == 0 && m_animationDict[CurrentKey].AnimationFrames.Count() > 1) {
                frameMoveDownToolStripButton.Enabled = true;
            } else if (m_currentFrame == m_animationDict[CurrentKey].AnimationFrames.Count() - 1) {
                frameMoveUpToolStripButton.Enabled = true;
            } else {
                frameMoveUpToolStripButton.Enabled = true;
                frameMoveDownToolStripButton.Enabled = true;
            }

            if (checkBoxAdjustXY.Checked)
            {
                nudXOffset.Increment = nudFrameWidth.Value;
                nudYOffset.Increment = nudFrameHeight.Value;
            }
            else
            {
                nudXOffset.Increment = 1;
                nudYOffset.Increment = 1;
            }

            m_switchingFrames = false;
        }

        void nudXOffset_ValueChanged(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].XOffset = (int)nudXOffset.Value;

                if (!this.Text.EndsWith("*"))
                    this.Text += "*";
            }
        }
        void nudYOffset_ValueChanged(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].YOffset = (int)nudYOffset.Value;

                if (!this.Text.EndsWith("*"))
                    this.Text += "*";
            }
        }
        void btnColor_Click(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                colorDialog.Color = btnColor.BackColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    btnColor.BackColor = colorDialog.Color;
                    m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].Color = new Color(
                        btnColor.BackColor.R,
                        btnColor.BackColor.G,
                        btnColor.BackColor.B);

                    if (!this.Text.EndsWith("*"))
                        this.Text += "*";
                }
            }
        }
        void nudOpacity_ValueChanged(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].Alpha = (float)nudOpacity.Value;

                if (!this.Text.EndsWith("*"))
                    this.Text += "*";
            }
        }
        void cbSpriteEffects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                if (cbSpriteEffects.GetItemText(cbSpriteEffects.SelectedItem) == "None")
                    m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].SpriteEffects = SpriteEffects.None;
                else if (cbSpriteEffects.GetItemText(cbSpriteEffects.SelectedItem) == "Flip Horizontally")
                    m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].SpriteEffects = SpriteEffects.FlipHorizontally;
                else if (cbSpriteEffects.GetItemText(cbSpriteEffects.SelectedItem) == "Flip Vertically")
                    m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].SpriteEffects = SpriteEffects.FlipVertically;
                else
                    m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].SpriteEffects = SpriteEffects.None;

                if (!this.Text.EndsWith("*"))
                    this.Text += "*";
            }
        }
        void nudRotation_ValueChanged(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                if (nudRotation.Value == 360)
                    nudRotation.Value = 0;
                else if (nudRotation.Value == -1)
                    nudRotation.Value = 359;

                float rotationInRadians = (float)nudRotation.Value * (float)Math.PI / 180;
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].Rotation = rotationInRadians;

                if (!this.Text.EndsWith("*"))
                    this.Text += "*";
            }
        }
        void nudScale_ValueChanged(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].Scale = (float)nudScale.Value;

                if (!this.Text.EndsWith("*"))
                    this.Text += "*";
            }
        }
        void cbTriggerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].MessageType = cbTriggerType.SelectedIndex;
            }
        }
        void tbTriggerInfo_TextChanged(object sender, EventArgs e)
        {
            if (IsValidFrame() && !m_switchingFrames)
            {
                m_animationDict[CurrentKey].AnimationFrames[m_currentFrame].MessageInfo = tbTriggerInfo.Text;
            }
        }

        private bool IsValidFrame()
        {
            if (lbAnimations.SelectedItem == null || lbAnimations.SelectedIndex == -1 ||
                m_currentFrame < 0 || m_currentFrame >= m_animationDict[CurrentKey].AnimationFrames.Count)
                return false;

            return true;
        }

        #endregion

        #region Zoom Event Handlers
        void zoomToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_animationCamera == null)
                return;
        }

        void zoomInToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (trackBarZoom.Value >= trackBarZoom.Maximum || !trackBarZoom.Enabled)
                return;

            trackBarZoom.Value += 1;
        }

        void zoomOutToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (trackBarZoom.Value <= trackBarZoom.Minimum || !trackBarZoom.Enabled)
                return;

            trackBarZoom.Value -= 1;
        }
        void trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            if (m_animationCamera == null)
                return;

            float newZoom = m_ZoomList[trackBarZoom.Value - 1];
            cbZoom.SelectedIndex = trackBarZoom.Value - 1;
            Vector2 newPosition = new Vector2(m_animationCamera.Position.X + (m_animationCamera.ViewportRectangle.Width / 2),
                m_animationCamera.Position.Y + (m_animationCamera.ViewportRectangle.Height / 2));

            m_animationCamera.SetZoom(newZoom, newPosition);
            m_animationCamera.LockCamera(0, 0, frameDisplay.Width, frameDisplay.Height);

            frameDisplay.Invalidate();
        }
        #endregion

        #region Grid Event Handlers
        private void displayGridToolStripButton_Click(object sender, EventArgs e)
        {
            displayGridToolStripButton.Checked = !displayGridToolStripButton.Checked;
        }
        private void nudGridLineXFrequency_ValueChanged(object sender, EventArgs e)
        {
            m_gridLineXFrequency = (int)nudGridLineXFrequency.Value;
        }
        private void nudGridLineYFrequency_ValueChanged(object sender, EventArgs e)
        {
            m_gridLineYFrequency = (int)nudGridLineYFrequency.Value;
        }
        private void nudGridLineXThickFrequency_ValueChanged(object sender, EventArgs e)
        {
            m_gridLineXThickFrequency = (int)nudGridLineXThickFrequency.Value;
        }
        private void nudGridLineYThickFrequency_ValueChanged(object sender, EventArgs e)
        {
            m_gridLineYThickFrequency = (int)nudGridLineYThickFrequency.Value;
        }
        #endregion

        #region Frame Display Event Handlers

        void frameDisplay_OnInitialize(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            frameDisplay.MouseEnter += new EventHandler(frameDisplay_MouseEnter);
            frameDisplay.MouseLeave += new EventHandler(frameDisplay_MouseLeave);
            frameDisplay.MouseMove += new MouseEventHandler(frameDisplay_MouseMove);
            frameDisplay.MouseUp += new MouseEventHandler(frameDisplay_MouseUp);
            frameDisplay.MouseDown += new MouseEventHandler(frameDisplay_MouseDown);

            try
            {
                using (Stream stream = new FileStream(@"Content\pixel.png", FileMode.Open, FileAccess.Read))
                {
                    m_pixelTexture = Texture2D.FromStream(GraphicsDevice, stream);
                    stream.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error reading images");
                m_pixelTexture = null;
            }
        }

        void frameDisplay_OnDraw(object sender, EventArgs e)
        {
            GraphicsDevice.Clear(m_mapBackdropColor);
            DrawGrid();
            DrawAnimationFrames();
        }

        void frameDisplay_SizeChanged(object sender, EventArgs e)
        {
            frameDisplay.Invalidate();
        }

        void frameDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { m_isLeftMouseDown = false; }
            if (e.Button == MouseButtons.Right) { m_isRightMouseDown = false; }
        }

        void frameDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { m_isLeftMouseDown = true; }
            if (e.Button == MouseButtons.Right) { m_isRightMouseDown = true; }
        }

        void frameDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            m_mouse.X = e.X;
            m_mouse.Y = e.Y;
        }

        void frameDisplay_MouseEnter(object sender, EventArgs e)
        {
            m_trackMouse = true;
        }

        void frameDisplay_MouseLeave(object sender, EventArgs e)
        {
            m_trackMouse = false;
        }

        #endregion

        #region Preview Display Event Handlers

        void previewDisplay_OnInitialize(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        void previewDisplay_OnDraw(object sender, EventArgs e)
        {
            GraphicsDevice2.Clear(m_mapBackdropColor);
            DrawAnimationPreview();
        }

        void previewDisplay_SizeChanged(object sender, EventArgs e)
        {
            previewDisplay.Invalidate();
        }

        #endregion

        #region Display Rendering and Logic

        /// <summary>
        /// Updates and draws the animation preview on the right side.
        /// </summary>
        private void DrawAnimationPreview()
        {
            if (lbAnimations.SelectedItem == null || lbAnimations.SelectedIndex == -1 ||
                m_animationTexture == null || m_animationDict[CurrentKey].AnimationFrames.Count == 0 ||
                m_previewCamera == null)
            {
                return;
            }

            // Remove frames safely
            if (frameRemovalList.Count > 0)
            {
                foreach (int frameNumber in frameRemovalList)
                {
                    m_animationDict[CurrentKey].RemoveFrame(frameNumber);
                }

                if (m_animationDict[CurrentKey].AnimationFrames.Count() == 0)
                {
                    CurrentFrame = -1;
                }
                else if (m_currentFrame >= m_animationDict[CurrentKey].AnimationFrames.Count())
                {
                    CurrentFrame = m_animationDict[CurrentKey].AnimationFrames.Count() - 1;
                }
                else
                {
                    CurrentFrame = m_currentFrame;
                }

                frameRemovalList.Clear();
            }

            // Draw the animation preview
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                m_previewCamera.Transformation);
          
            Animation animation = (Animation)m_animationDict[CurrentKey].Clone();
            
            if (animation.CurrentFrame != null)
            {
                // Move camera controls to camera function
                m_previewCamera.SetZoom(4f);
                m_previewCamera.SnapToPosition(new Vector2(previewDisplay.Width / 2, previewDisplay.Height / 2));

                Vector2 position = new Vector2(
                    (previewDisplay.Width / m_previewCamera.Zoom - (animation.FrameWidth * animation.CurrentFrame.Scale)) / 2,
                    (previewDisplay.Height / m_previewCamera.Zoom - (animation.FrameHeight * animation.CurrentFrame.Scale)) / 2);

                animation.Draw(spriteBatch, position);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Upates and draws the animation frames in the main window.
        /// </summary>
        private void DrawAnimationFrames()
        {
            if (lbAnimations.SelectedItem == null || lbAnimations.SelectedIndex == -1 ||
                m_animationTexture == null || m_animationCamera == null)
            {
                return;
            }

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                m_animationCamera.Transformation);

            // Draws [i] frame 
            Animation currentAnimation = m_animationDict[CurrentKey];
            List<AnimationFrame> currentAnimationFrames = m_animationDict[CurrentKey].AnimationFrames;
            Vector2 position = Vector2.Zero;

            for (int i = 0; i < currentAnimationFrames.Count(); i++)
            {
                if ((position.X + currentAnimation.FrameWidth * currentAnimationFrames[i].Scale) * m_animationCamera.Zoom > frameDisplay.Width)
                {
                    position.X = 0;
                    position.Y += currentAnimation.FrameHeight * currentAnimationFrames[i].Scale;
                }
                
                spriteBatch.Draw(
                    m_animationTexture,
                    new Vector2(position.X + currentAnimation.Center.X * currentAnimationFrames[i].Scale,
                        position.Y + currentAnimation.Center.Y * currentAnimationFrames[i].Scale),
                    new Rectangle(
                        currentAnimationFrames[i].XOffset,
                        currentAnimationFrames[i].YOffset,
                        currentAnimation.FrameWidth,
                        currentAnimation.FrameHeight),
                    currentAnimationFrames[i].Color,
                    currentAnimationFrames[i].Rotation,
                    currentAnimation.Center,
                    currentAnimationFrames[i].Scale,
                    currentAnimationFrames[i].SpriteEffects,
                    0);

                // Draws rectangle around currently selected frame (***efficiency can be improved here***)
                if (i == m_currentFrame)
                {
                    Rectangle destRect = new Rectangle(
                        (int)position.X,
                        (int)position.Y,
                        1,
                        1);

                    int yMax = (int)(position.Y + currentAnimation.FrameHeight * currentAnimationFrames[i].Scale);
                    int xMax = (int)(position.X + currentAnimation.FrameWidth * currentAnimationFrames[i].Scale);
                    for (int y = (int)position.Y; y <= yMax; y++)
                    {
                        destRect.Y = y;
                        for (int x = (int)position.X; x <= xMax; x++)
                        {                            
                            destRect.X = x;
                            if (y == (int)position.Y || y == yMax || x == (int)position.X || x == xMax)
                            {
                                spriteBatch.Draw(m_pixelTexture, destRect, Color.White);
                            }
                        }
                    }                     
                }

                position = new Vector2(position.X + currentAnimation.FrameWidth * currentAnimationFrames[i].Scale, position.Y);
            }

            spriteBatch.End();            
        }

        /// <summary>
        /// Draws the background grid.
        /// </summary>
        private void DrawGrid()
        {
            if (!displayGridToolStripButton.Checked || m_animationCamera == null)
            {
                return;
            }

            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                m_animationCamera.Transformation);

            int yMin = (int)(m_animationCamera.Position.Y / m_animationCamera.Zoom);
            yMin = yMin - (int)(yMin % m_gridLineYFrequency);
            int xMin = (int)(m_animationCamera.Position.X / m_animationCamera.Zoom);
            xMin = xMin - (int)(xMin % m_gridLineXFrequency);
            int yMax = (int)(frameDisplay.Height / m_animationCamera.Zoom + yMin);
            int xMax = (int)(frameDisplay.Width / m_animationCamera.Zoom + xMin);

            Rectangle destRect = new Rectangle(xMin, yMin, (int)(frameDisplay.Width / m_animationCamera.Zoom), 1);
            
            for (int y = yMin; y < yMax; y += m_gridLineYFrequency)
            {
                destRect.Y = y;

                if (y % (m_gridLineYFrequency * m_gridLineYThickFrequency) == 0)
                {
                    spriteBatch.Draw(m_pixelTexture, destRect, m_gridThickColor);
                }
                else
                {
                    spriteBatch.Draw(m_pixelTexture, destRect, m_gridColor);
                }
            }

            destRect = new Rectangle(xMin, yMin, 1, (int)(frameDisplay.Height / m_animationCamera.Zoom));

            for (int x = xMin; x < xMax; x += m_gridLineXFrequency)
            {
                destRect.X = x;

                if (x % (m_gridLineXFrequency * m_gridLineXThickFrequency) == 0)
                {
                    spriteBatch.Draw(m_pixelTexture, destRect, m_gridThickColor);
                }
                else
                {
                    spriteBatch.Draw(m_pixelTexture, destRect, m_gridColor);
                }
            }

            spriteBatch.End();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Logic()
        {
            CalculateFrameRate();
            UpdateDebugText();

            if (lbAnimations.SelectedItem == null || lbAnimations.SelectedIndex == -1 ||
                m_animationTexture == null)
            {
                return;
            }

            m_animationDict[CurrentKey].Update();

            if (!m_trackMouse)
            {
                return;
            }

            if (lbAnimations.SelectedIndex != -1 && m_isLeftMouseDown && m_animationDict.Count() > 0)
            {
                int currentFrameX = (int)(m_mouse.X / ((float)nudFrameWidth.Value * (float)nudScale.Value * m_animationCamera.Zoom));
                int currentFrameY = (int)((m_mouse.Y / ((float)nudFrameHeight.Value * (float)nudScale.Value * m_animationCamera.Zoom)));
                currentFrameY *= (int)(frameDisplay.Width / ((float)nudFrameWidth.Value * (float)nudScale.Value * m_animationCamera.Zoom));
                int mousePos = currentFrameX + currentFrameY;

                if (mousePos >= 0 && mousePos < m_animationDict[CurrentKey].AnimationFrames.Count())
                {
                    CurrentFrame = mousePos;
                }
            }
        }
        
        /// <summary>
        /// Updates debug text values.
        /// </summary>
        private void UpdateDebugText()
        {
            m_debugTextList.Clear();
            lblStatusText.Text = "";
            lblStatusText.Text += "  Mouse.X: " + m_mouse.X + "  Mouse.Y: " + m_mouse.Y + "  CurrentFrame: " + m_currentFrame;
            lblStatusText.Text += "  FPS: " + m_lastFrameRate;
            lblStatusText.Text += "  X: " + m_previewCamera.Position.X + "  Y: " + m_previewCamera.Position.Y;
        }

        /// <summary>
        /// Calculates the frame rate.
        /// </summary>
        private void CalculateFrameRate()
        {
            if (Environment.TickCount - m_lastTick >= 1000)
            {
                m_lastFrameRate = m_frameRate;
                m_frameRate = 0;
                m_lastTick = Environment.TickCount;
            }

            m_frameRate++;
        }

        #endregion
    }
}