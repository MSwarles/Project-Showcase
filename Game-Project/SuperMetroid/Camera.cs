using Microsoft.Xna.Framework;

namespace SuperMetroid
{
    public enum CameraMode { Free, Follow }

    /// <summary>
    /// Camera used for displaying the game world.
    /// </summary>
    public class Camera
    {
        #region TODO List
        // TODO: CAMERA - improve camera (add 'dead zone', 'smart' follow, look directions)
        #endregion

        #region Fields
        private Vector2 m_position;
        private Rectangle m_viewportRect;
        private float m_speed, m_zoom;
        #endregion

        #region Properties
        public CameraMode CameraMode { get; private set; }
        public Vector2 Position { get { return m_position; } set { m_position = value; } }
        public float Speed { get { return m_speed; } set { m_speed = MathHelper.Clamp(value, 1f, 16f); } }
        public float Zoom { get { return m_zoom; } set { m_zoom = MathHelper.Clamp(value, 0.25f, 5f); } }
        public Rectangle ViewportRectangle
        {
            get
            {
                return new Rectangle(
                    m_viewportRect.X,
                    m_viewportRect.Y,
                    m_viewportRect.Width,
                    m_viewportRect.Height);
            }
            set { m_viewportRect = value; }
        }
        public Matrix Transformation
        {
            get
            {
                return Matrix.CreateScale(m_zoom) *
                    Matrix.CreateTranslation(new Vector3(-Position, 0f));
            }
        }
        #endregion

        #region Constructors
        public Camera(Rectangle viewportRect)
        {
            m_speed = 4f;
            m_zoom = 1f;
            m_viewportRect = viewportRect;
            m_position = viewportRect.Location.ToVector2();
            CameraMode = CameraMode.Follow;
        }

        public Camera(Rectangle viewportRect, Vector2 position)
        {
            m_speed = 4f;
            m_zoom = 1f;
            m_viewportRect = viewportRect;
            m_position = position;
            CameraMode = CameraMode.Follow;
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime)
        {
            if (CameraMode == CameraMode.Follow) { return; }

            Vector2 motion = Vector2.Zero;
            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                m_position += motion * m_speed;
                m_position = new Vector2((int)m_position.X, (int)m_position.Y);
            }
        }
        #endregion

        #region Zoom Methods
        /// <summary>
        /// Zoom the camera in.
        /// </summary>
        public void ZoomIn()
        {
            Zoom += .25f;
        }

        /// <summary>
        /// Zoom the camera out.
        /// </summary>
        public void ZoomOut()
        {
            Zoom -= .25f;
        }

        /// <summary>
        /// Zoom the camera in/out at position.
        /// </summary>
        /// <param name="zoom"></param>
        /// <param name="position"></param>
        public void ZoomOnPosition(float zoom, Vector2 position)
        {
            float zoomRatio = zoom / m_zoom;
            m_zoom = zoom;

            position *= zoomRatio;
            SnapToPosition(position);
        }
        #endregion

        #region Positioning Methods
        /// <summary>
        /// Snaps camera to position.
        /// </summary>
        /// <param name="position">Position of camera.</param>
        public void SnapToPosition(Vector2 position)
        {
            m_position.X = position.X - (m_viewportRect.Width / 2);
            m_position.Y = position.Y - (m_viewportRect.Height / 2);
            m_position = new Vector2((int)m_position.X, (int)m_position.Y);
        }

        /// <summary>
        /// Locks camera to given bounds.
        /// </summary>
        /// <param name="xMin">Minimum x value.</param>
        /// <param name="yMin">Minimum y value.</param>
        /// <param name="xMax">Maximum x value.</param>
        /// <param name="yMax">Maximum y value.</param>
        public void LockCamera(int xMin, int yMin, int xMax, int yMax)
        {
            m_position.X = MathHelper.Clamp(
                m_position.X,
                xMin,
                xMax - m_viewportRect.Width);

            m_position.Y = MathHelper.Clamp(
                m_position.Y,
                yMin,
                yMax - m_viewportRect.Height);
        }

        /// <summary>
        /// Locks camera to level.
        /// </summary>
        /// <param name="levelWidth">Width of level.</param>
        /// <param name="levelHeight">Height of level.</param>
        public void LockToLevel(int levelWidth, int levelHeight)
        {
            int tileWidth = 16;
            m_position.X = MathHelper.Clamp(
                m_position.X,
                0,
                levelWidth * m_zoom - m_viewportRect.Width);

            m_position.Y = MathHelper.Clamp(
                m_position.Y,
                0,
                (levelHeight - tileWidth * 2) * m_zoom  - m_viewportRect.Height);
        }

        /// <summary>
        /// Locks camera to actor.
        /// </summary>
        /// <param name="actor"></param>
        public void LockToActor(Actor actor)
        {
            //m_position.X = (actor.Position.X + actor.Width / 2) * m_zoom - (m_viewportRect.Width / 2);
            //m_position.Y = (actor.Position.Y + actor.Height / 2) * m_zoom - (m_viewportRect.Height / 2);
            if (actor == null) { return; }

            m_position.X = actor.Position.X * m_zoom - (m_viewportRect.Width / 2);
            m_position.Y = actor.Position.Y * m_zoom - (m_viewportRect.Height / 2);
            m_position = new Vector2((int)m_position.X, (int)m_position.Y);
        }

        /// <summary>
        /// Toggles between various preset camera modes.
        /// </summary>
        public void ToggleCameraMode()
        {
            if (CameraMode == CameraMode.Follow)
            { 
                CameraMode = CameraMode.Free;
            }
            else if (CameraMode == CameraMode.Free)
            { 
                CameraMode = CameraMode.Follow;
            }
        }
        #endregion
    }
}