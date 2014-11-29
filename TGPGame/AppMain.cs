using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace TGPGame
{
	public class AppMain
	{
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene gameScene;
		private static Sce.PlayStation.HighLevel.UI.Scene uiScene;
		private static Sce.PlayStation.HighLevel.UI.Label scoreLabel;
		
		private static Obstacle[] obstacles;
		private static Player player;
		private static Background background;
		
		//private static GraphicsContext graphics;
		
		public static void Main (string[] args)
		{

			Initialize();

			bool quitGame = false;
			while (!quitGame) 
			{
				Update ();
				
				Director.Instance.Update();
				Director.Instance.Render();
				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}		
			player.Dispose();
			foreach(Obstacle obstacle in obstacles)
				obstacle.Dispose();
			background.Dispose();
			
			Director.Terminate ();		
		}

		public static void Initialize ()
		{
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Set ui scene
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			scoreLabel = new Sce.PlayStation.HighLevel.UI.Label();
			scoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
			scoreLabel.VerticalAlignment = VerticalAlignment.Top;
			scoreLabel.SetPosition(
				Director.Instance.GL.Context.GetViewport().Width/2 - scoreLabel.Width/2,
				Director.Instance.GL.Context.GetViewport().Height*0.1f - scoreLabel.Height/2);
			scoreLabel.Text = "0";
			panel.AddChildLast(scoreLabel);
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);
			
			//Create objects
			background = new Background(gameScene);
			
			player = new Player(gameScene);
			
			obstacles = new Obstacle[3];
			obstacles[0] = new Obstacle(Director.Instance.GL.Context.GetViewport().Width*0.5f, gameScene);	
			obstacles[1] = new Obstacle(Director.Instance.GL.Context.GetViewport().Width, gameScene);
			obstacles[2] = new Obstacle(Director.Instance.GL.Context.GetViewport().Width, gameScene);
			Director.Instance.RunWithScene(gameScene, true);
		}	

		public static void Update ()
		{
			// Query gamepad for current state
			//var gamePadData = GamePad.GetData (0);
			
			//Determine whether the player tapped the screen
			var touches = Touch.GetData(0);
			if(touches.Count > 0)
				player.Tapped();
			//Update
			player.Update(0.0f);
			
			if(player.Alive)
			{
				background.Update(0.0f);

				foreach(Obstacle obstacle in obstacles)
					obstacle.Update(0.0f);
			}
		}

		//public static void Render ()
		//{
			// Clear the screen
			//graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			//graphics.Clear ();

			// Present the screen
			//graphics.SwapBuffers ();
		//}
	}
}













