using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace TGPGame
{
	public class Player
	{
		//Private variables.
		private static SpriteUV sprite;
		private static TextureInfo textureInfo;
		private static int pushAmount = 100;
		private static float yPositionBeforePush;
		private static float yPositionAfterPush;
		private static bool	rise;
		private static bool fall;
		//private static float angle;
		private static bool	alive;
		
		public bool Alive { get{return alive;} set{alive = value;} }
		
		//Public functions.
		public Player (Scene scene)
		{
			textureInfo = new TextureInfo("/Application/textures/egg.png");
			
			sprite = new SpriteUV();
			sprite = new SpriteUV(textureInfo);	
			sprite.Quad.S = textureInfo.TextureSizef;
			
			sprite.Position = new Vector2(50.0f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			//sprite.Pivot = new Vector2(0.5f,0.5f);
			
			rise  = false;
			fall = false;
			alive = true;
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float deltaTime)
		{			
			//adjust the push
			if(rise)
			{
				
				if( (sprite.Position.Y-yPositionBeforePush) < pushAmount)
					sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y + 3f);
				else
				{
					rise = false;
					fall = true;
				}
			}
			else
			{
				sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y - 0);
			}
			
			
			if(fall)
			{
				if( (sprite.Position.Y-yPositionAfterPush) > pushAmount) // if sprite is higher than the water
					sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y - 3f);
				else
					fall = false;
			}
			else
			{
				sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y - 0);
			}
		}	
		
		public void Tapped()
		{
			if(!rise) // if we are not currently on the rise
			{
				rise = true;
				yPositionBeforePush = sprite.Position.Y;
			}
		}
	}
}

