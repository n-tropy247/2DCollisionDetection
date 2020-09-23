using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CollisionTesting
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        private readonly BoundingBox _boundingBox1;
        private readonly BoundingBox _boundingBox2;
        private readonly BoundingBox _boundingBox3;
        private readonly BoundingBox _boundingBox4;
        private readonly int _screenWidth = 1280;
        private readonly int _screenHeight = 720;
        private float speed2X = 100;
        private float speed2Y = 100;
        private float speed3X = 150;
        private float speed3Y = 150;
        private float speed4X = 115;
        private float speed4Y = 115;
        private readonly List<CollisionCell> _cells = new List<CollisionCell>();
        private Texture2D _pixel;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            var graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _boundingBox1 = new BoundingBox(new Vector2(0, _screenHeight) * 0.5f, 50, 50);
            _boundingBox2 = new BoundingBox(new Vector2(_screenWidth, _screenHeight) * 0.5f, 50, 50);
            _boundingBox3 = new BoundingBox(new Vector2(_screenWidth / 2.0f, _screenHeight) * 0.5f, 50, 50);
            _boundingBox4 = new BoundingBox(new Vector2(_screenWidth, _screenHeight / 2.0f) * 0.5f, 50, 50);

            graphics.PreferredBackBufferWidth = _screenWidth;
            graphics.PreferredBackBufferHeight = _screenHeight;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            var gcd = 10;
            for (var i = 1; i < _screenWidth && i < _screenHeight; i++)
            {
                if (_screenWidth % i == 0 && _screenHeight % i == 0)
                    gcd = i;
            }
            for (var i = 0; i < _screenWidth; i += gcd)
            {
                for (var j = 0; j < _screenHeight; j += gcd)
                {
                    _cells.Add(new CollisionCell(new Vector2(i, j)));
                }
            }
            AddToCells(_boundingBox1);
            AddToCells(_boundingBox2);
            AddToCells(_boundingBox3);
            AddToCells(_boundingBox4);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _pixel = new Texture2D(GraphicsDevice, 1, 1);

            _pixel.SetData(new[]{Color.White});

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            var speed = 200.0f;

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _boundingBox1.Position -= Vector2.UnitY * speed * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                _boundingBox1.Position += Vector2.UnitY * speed * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _boundingBox1.Position -= Vector2.UnitX * speed * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _boundingBox1.Position += Vector2.UnitX * speed * deltaTime;
            }

            if (_boundingBox1.Left <= 0)
            {
                _boundingBox1.Left = 0;

            } else if (_boundingBox1.Right > _screenWidth)
            {
                _boundingBox1.Right = _screenWidth;
            }

            if (_boundingBox1.Top < 0)
            {
                _boundingBox1.Top = 0;
            } else if (_boundingBox1.Bottom > _screenHeight)
            {
                _boundingBox1.Bottom = _screenHeight;
            }

            _boundingBox2.Position += Vector2.UnitX * speed2X / 4 * deltaTime;
            _boundingBox2.Position += Vector2.UnitY * speed2Y / 4 * deltaTime;
            if (_boundingBox2.Left <= 0)
            {
                _boundingBox2.Left = 0;
                speed2X *= -1.0f;

            }
            else if (_boundingBox2.Right > _screenWidth)
            {
                _boundingBox2.Right = _screenWidth;
                speed2X *= -1.0f;
            }
            if (_boundingBox2.Top < 0)
            {
                _boundingBox2.Top = 0;
                speed2Y *= -1.0f;
            }
            else if (_boundingBox2.Bottom > _screenHeight)
            {
                _boundingBox2.Bottom = _screenHeight;
                speed2Y *= -1.0f;
            }

            _boundingBox3.Position += Vector2.UnitX * speed3X / 4 * deltaTime;
            _boundingBox3.Position += Vector2.UnitY * speed3Y / 4 * deltaTime;
            if (_boundingBox3.Left <= 0)
            {
                _boundingBox3.Left = 0;
                speed3X *= -1.0f;

            }
            else if (_boundingBox3.Right > _screenWidth)
            {
                _boundingBox3.Right = _screenWidth;
                speed3X *= -1.0f;
            }
            if (_boundingBox3.Top < 0)
            {
                _boundingBox3.Top = 0;
                speed3Y *= -1.0f;
            }
            else if (_boundingBox3.Bottom > _screenHeight)
            {
                _boundingBox3.Bottom = _screenHeight;
                speed3Y *= -1.0f;
            }

            _boundingBox4.Position += Vector2.UnitX * speed4X / 4 * deltaTime;
            _boundingBox4.Position += Vector2.UnitY * speed4Y / 4 * deltaTime;
            if (_boundingBox4.Left <= 0)
            {
                _boundingBox4.Left = 0;
                speed4X *= -1.0f;

            }
            else if (_boundingBox4.Right > _screenWidth)
            {
                _boundingBox4.Right = _screenWidth;
                speed4X *= -1.0f;
            }
            if (_boundingBox4.Top < 0)
            {
                _boundingBox4.Top = 0;
                speed4Y *= -1.0f;
            }
            else if (_boundingBox4.Bottom > _screenHeight)
            {
                _boundingBox4.Bottom = _screenHeight;
                speed4Y *= -1.0f;
            }

            _boundingBox1.RemoveAllCells();
            AddToCells(_boundingBox1);

            _boundingBox2.RemoveAllCells();
            AddToCells(_boundingBox2);

            _boundingBox3.RemoveAllCells();
            AddToCells(_boundingBox3);

            _boundingBox4.RemoveAllCells();
            AddToCells(_boundingBox4);

            base.Update(gameTime);
        }

        private void AddToCells(BoundingBox b)
        {
            var topLeft = (int) b.Position.Y / 80 + _screenHeight / 80 * ((int) b.Position.X / 80);
            var topRight = (int)b.Position.Y / 80 + _screenHeight / 80 * (((int)b.Position.X + (int)b.Width) / 80); 
            var bottomLeft = ((int)b.Position.Y + (int)b.Height) / 80 + _screenHeight / 80 * ((int)b.Position.X / 80);
            var bottomRight = ((int)b.Position.Y + (int)b.Height) / 80 + _screenHeight / 80 * (((int)b.Position.X + (int)b.Width) / 80);

            if (topLeft > -1 && topLeft < _cells.Count)
            {
                _cells[topLeft].AddToCell(b);
            }

            if (topLeft != topRight && topRight > -1 && topRight < _cells.Count)
            {
                _cells[topRight].AddToCell(b);
            }

            if (topLeft != bottomRight && topRight != bottomRight && bottomRight > -1 && bottomRight < _cells.Count)
            {
                _cells[bottomRight].AddToCell(b);
            }

            if (topLeft != bottomLeft && topRight != bottomLeft && bottomRight != bottomLeft && bottomLeft > -1 && bottomLeft < _cells.Count)
            {
                _cells[bottomLeft].AddToCell(b);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var boxColorCollide = Color.Green;
            var cellColorCollide = Color.Yellow;
            var boxColorNoCollide = Color.Red;
            var cellColorNoCollide = Color.Blue;

            _boundingBox1.DrawColor = boxColorNoCollide;
            _boundingBox2.DrawColor = boxColorNoCollide;
            _boundingBox3.DrawColor = boxColorNoCollide;
            _boundingBox4.DrawColor = boxColorNoCollide;

            foreach (var c in _cells)
            {
                if (c.Items.Count > 1)
                {
                    c.DrawColor = cellColorCollide;
                    for (var i = 0; i < c.Items.Count - 1; i++)
                    {
                        if (!c.Items[i].Bounds.Intersects(c.Items[i + 1].Bounds)) continue;
                        c.Items[i].DrawColor = boxColorCollide;
                        c.Items[i + 1].DrawColor = boxColorCollide;
                    }
                }
                else
                {
                    c.DrawColor = cellColorNoCollide;
                }
            }

            _spriteBatch.Begin();

            foreach (var c in _cells)
            {
                _spriteBatch.Draw(_pixel, new Rectangle((int) c.Position.X, (int) c.Position.Y, 1, 81), c.DrawColor);
                _spriteBatch.Draw(_pixel, new Rectangle((int)c.Position.X, (int)c.Position.Y, 81, 1), c.DrawColor);
                _spriteBatch.Draw(_pixel, new Rectangle((int)c.Position.X + 80, (int)c.Position.Y, 1, 81), c.DrawColor);
                _spriteBatch.Draw(_pixel, new Rectangle((int)c.Position.X, (int)c.Position.Y + 80, 81, 1), c.DrawColor);
            }

            _spriteBatch.Draw(_pixel, _boundingBox1.Bounds, _boundingBox1.DrawColor);
            _spriteBatch.Draw(_pixel, _boundingBox2.Bounds, _boundingBox2.DrawColor);
            _spriteBatch.Draw(_pixel, _boundingBox3.Bounds, _boundingBox3.DrawColor);
            _spriteBatch.Draw(_pixel, _boundingBox4.Bounds, _boundingBox4.DrawColor);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
