using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MovingObjectAtConsoleOutput
{
	class Program
	{
		public static int ShapeIndex { get; set; }
		public static int Score { get; set; }

		static void Main(string[] args)
		{
			initialization();

			MainLoop();

			Console.ReadKey();
		}

		private static void initialization()
		{
			Pixel pixel = new Pixel();
			pixel.Initialization();
		}

		private static void MainLoop()
		{
			Console.Clear();

			Pixel.OverallDisplay();

			do
			{
				ShapeIndex = RandomClassLibrary.Random.GetRandomInteger(0, 6);
				SelectFallingShapeIndex();

				Thread ChildThread = new Thread(new ThreadStart(timer));
				ChildThread.Start();

				Shape.AnchorPointX = 5;
				Shape.AnchorPointY = 0;

				Shape.RotationIndex = 0;

				do
				{
					char key = Console.ReadKey(true).KeyChar;
					Display.Move(key);
					Pixel.OverallDisplay();

				} while (!IsTouchLowerBorder() && !IsTouchPileLowerSurface());

			} while (true);
		}

		public static void SelectFallingShapeIndex()
		{
			switch (ShapeIndex)
			{
				case 0:
					HorizontalShape horizontalShape = new HorizontalShape();
					Shape.initDisplayDelegate = horizontalShape.InitDisplay;
					Shape.initRotateDelegate = horizontalShape.InitRotate;
					break;

				case 1:
					SShape sShape = new SShape();
					Shape.initDisplayDelegate = sShape.InitDisplay;
					Shape.initRotateDelegate = sShape.InitRotate;
					break;

				case 2:
					ZShape zShape = new ZShape();
					Shape.initDisplayDelegate = zShape.InitDisplay;
					Shape.initRotateDelegate = zShape.InitRotate;
					break;

				case 3:
					LShape lShape = new LShape();
					Shape.initDisplayDelegate = lShape.InitDisplay;
					Shape.initRotateDelegate = lShape.InitRotate;
					break;

				case 4:
					JShape jShape = new JShape();
					Shape.initDisplayDelegate = jShape.InitDisplay;
					Shape.initRotateDelegate = jShape.InitRotate;
					break;

				case 5:
					TShape tShape = new TShape();
					Shape.initDisplayDelegate = tShape.InitDisplay;
					Shape.initRotateDelegate = tShape.InitRotate;
					break;

				case 6:
					OShape oShape = new OShape();
					Shape.initDisplayDelegate = oShape.InitDisplay;
					Shape.initRotateDelegate = oShape.InitRotate;
					break;
			}
		}

		public static bool IsTouchLeftBorder()
		{
			for (int j = 0; j < Display.Height; j++)
			{
				if (Pixel.GetPixel(0, j).IsDisplayed)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsTouchRightBorder()
		{
			for (int j = 0; j < Display.Height; j++)
			{
				if (Pixel.GetPixel(Display.Width - 1, j).IsDisplayed)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsTouchLowerBorder()
		{
			for (int i = 0; i < Display.Width; i++)
			{
				if (Pixel.GetPixel(i, Display.Height - 1).IsDisplayed)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsTouchPileLeftSurface()
		{
			for (int j = 0; j < Display.Height; j++)
			{
				for (int i = 0; i < Display.Width; i++)
				{
					if (Pixel.GetPixel(i, j).IsDisplayed)
					{
						if (Pixel.GetPixel(i - 1, j).Isplaced)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public static bool IsTouchPileRightSurface()
		{
			for (int j = 0; j < Display.Height; j++)
			{
				for (int i = 0; i < Display.Width; i++)
				{
					if (Pixel.GetPixel(i, j).IsDisplayed)
					{
						if (Pixel.GetPixel(i + 1, j).Isplaced)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public static bool IsTouchPileLowerSurface()                                               // Must be stated after the "IsTouchLowerBoarder" statement
		{
			for (int j = 0; j < Display.Height; j++)
			{
				for (int i = 0; i < Display.Width; i++)
				{
					if (Pixel.GetPixel(i, j).IsDisplayed)
					{
						if (Pixel.GetPixel(i, j + 1).Isplaced)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public static void SetShapeToStatic()
		{
			foreach (Pixel item in Pixel.DisplayMatrix)
			{
				if (item.IsDisplayed)
				{
					item.IsDisplayed = false;
					item.Isplaced = true;
				}
			}
		}

		private static void timer()
		{
			Shape shape = new Shape();

			do
			{
				shape.InitDown();
				Console.Clear();

				if (!IsTouchLowerBorder() && !IsTouchPileLowerSurface())
				{
					Shape.initDisplayDelegate();
				}

				Pixel.OverallDisplay();

				Thread.Sleep(500);

			} while (true);
		}
	}

	class Display
	{
		public static int Width { get; set; } = 10;
		public static int Height { get; set; } = 16;

		public static void Move(char key)
		{
			Shape shape = new Shape();

			switch (key.ToString())
			{
				case "w":
					Console.Clear();
					shape.InitUp();

					if (!Program.IsTouchLowerBorder() && !Program.IsTouchPileLowerSurface())
					{
						Shape.initRotateDelegate();
					}
					break;

				case "s":
					shape.InitDown();
					Console.Clear();

					if (!Program.IsTouchLowerBorder() && !Program.IsTouchPileLowerSurface())
					{
						Shape.initDisplayDelegate();
					}
					break;

				case "a":
					Console.Clear();
					shape.InitLeft();

					if (!Program.IsTouchLowerBorder() && !Program.IsTouchPileLowerSurface())
					{
						Shape.initDisplayDelegate();
					}
					break;

				case "d":
					Console.Clear();
					shape.InitRight();

					if (!Program.IsTouchLowerBorder() && !Program.IsTouchPileLowerSurface())
					{
						Shape.initDisplayDelegate();
					}
					break;
			}
		}

		public static bool IsRowFilled(int j)
		{
			for (int i = 0; i < Width; i++)
			{
				if (!Pixel.GetPixel(i, j).Isplaced)
				{
					return false;
				}
			}
			return true;
		}

		public static bool IsRowFilled()
		{
			int PixelsInOneRow = 0;

			for (int j = Height - 1; j >= 0; j--)
			{
				for (int i = 0; i < Width; i++)
				{
					if (Pixel.GetPixel(i, j).Isplaced)
					{
						PixelsInOneRow++;
					}
				}

				if (PixelsInOneRow == Width)
				{
					return true;
				}
				else
				{
					PixelsInOneRow = 0;
				}
			}
			return false;
		}

		public static void EliminateAndMoveRow()
		{
			for (int j = Height - 1; j >= 0; j--)
			{
				if (IsRowFilled(j))
				{
					for (int i = 0; i < Width; i++)
					{
						Pixel.GetPixel(i, j).IsDisplayed = false;
						Pixel.GetPixel(i, j).Isplaced = false;
					}

					for (int upperj = j - 1; upperj >= 0; upperj--)
					{
						for (int i = 0; i < Width; i++)
						{
							if (Pixel.GetPixel(i, upperj).Isplaced || Pixel.GetPixel(i, upperj).IsDisplayed)
							{
								Pixel.GetPixel(i, upperj).Isplaced = false;
								Pixel.GetPixel(i, upperj).IsDisplayed = false;
								Pixel.GetPixel(i, upperj + 1).Isplaced = true;					
							}
						}
					}
				}
			}
		}
	}

	class Pixel : Display
	{
		public bool IsDisplayed = false;
		public bool Isplaced = false;

		public static Pixel[,] DisplayMatrix = new Pixel[Width, Height];

		public static Pixel GetPixel(int i, int j)
		{
			return DisplayMatrix[i,j];
		}

		public static void OverallDisplay()
		{
			for (int j = 0; j < 16; j++)
			{
				for (int i = 0; i < 10; i++)
				{
					if (GetPixel(i, j).IsDisplayed || GetPixel(i,j).Isplaced)
					{
						Console.Write("■");
					}
					else
					{
						Console.Write("□");
					}
				}
				Console.WriteLine("");
			}
		}

		public void Initialization()
		{
			for (int j = 0; j < 16; j++)
			{
				for (int i = 0; i < 10; i++)
				{
					DisplayMatrix[i, j] = new Pixel();
				}
			}
		}
	}

	class Shape : Display
	{
		public static int AnchorPointX { get; set; } = 4;
		public static int AnchorPointY { get; set; } = 0;
		public static int RotationIndex { get; set; } = 0;

		public void InitUp()
		{
				Program.SelectFallingShapeIndex();

			if (Program.IsTouchLowerBorder() || Program.IsTouchPileLowerSurface())
			{
				Program.SetShapeToStatic();
			}

			RemovePreviousDisplay();
		}

		public void InitLeft()
		{
			if (!Program.IsTouchLeftBorder() && !Program.IsTouchPileLeftSurface())
			{
				AnchorPointX--;
			}

			if (Program.IsTouchLowerBorder() || Program.IsTouchPileLowerSurface())
			{
				Program.SetShapeToStatic();

				if (IsRowFilled())
				{
					EliminateAndMoveRow();
				}
			}

			RemovePreviousDisplay();
		}

		public void InitRight()
		{
			if (!Program.IsTouchRightBorder() && !Program.IsTouchPileRightSurface())
			{
				AnchorPointX++;
			}

			if(Program.IsTouchLowerBorder() || Program.IsTouchPileLowerSurface())
			{
				Program.SetShapeToStatic();

				if (IsRowFilled())
				{
					EliminateAndMoveRow();
				}
			}

			RemovePreviousDisplay();
		}

		public void InitDown()
		{
			if (!Program.IsTouchLowerBorder() && !Program.IsTouchPileLowerSurface())
			{
				AnchorPointY++;
			}
			else
			{
				Program.SetShapeToStatic();

				if (IsRowFilled())
				{
					EliminateAndMoveRow();
				}
			}

			RemovePreviousDisplay();
		}

		public void RemovePreviousDisplay()
		{
			for (int j = 0; j < 16; j++)
			{
				for (int i = 0; i < 10; i++)
				{
					Pixel.GetPixel(i, j).IsDisplayed = false;
				}
			}
		}

		public delegate void InitDisplayDelegate();
		public static InitDisplayDelegate initDisplayDelegate;
		public static InitDisplayDelegate initRotateDelegate;
	}

	class HorizontalShape : Shape
	{
		public void InitDisplay()
		{
			SetPosition();
		}

		public void InitRotate()
		{
			if (RotationIndex == 1)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}

			SetPosition();
		}

		public void SetPosition()
		{
			switch (RotationIndex)
			{
				case 0:                                                                            // Anchor point is set to (0, 0)
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // ┌─┬─┬─┬─┐
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;             // └─┴─┴─┴─┘
					Pixel.GetPixel(AnchorPointX + 2, AnchorPointY).IsDisplayed = true;
					Pixel.GetPixel(AnchorPointX + 3, AnchorPointY).IsDisplayed = true;
					break;

				case 1:                                                                            // Anchor point remains unchanged
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // ├─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;             // ├─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 3).IsDisplayed = true;             // ├─┤
					break;                                                                         // └─┘
			}
		}
	}

	class SShape : Shape
	{
		public void InitDisplay()
		{
			SetPosition();
		}

		public void InitRotate()
		{

			if (RotationIndex == 1)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}

			SetPosition();
		}

		public void SetPosition()
		{
			switch (RotationIndex)
			{
				case 0:                                                                            // Anchor point is set to (1, 0)
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 //   ┌─┬─┐
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;             // ┌─┼─┼─┘
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // └─┴─┘
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;
					break;

				case 1:                                                                            // Anchor point remains unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;         // ┌─┐
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;             // ├─┼─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // └─┼─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;             //   └─┘
					break;
			}
		}
	}

	class ZShape : Shape
	{
		public void InitDisplay()
		{
			SetPosition();
		}

		public void InitRotate()
		{
			if (RotationIndex == 1)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}

			SetPosition();
		}

		public void SetPosition()
		{
			switch (RotationIndex)
			{
				case 0:                                                                            // Anchor point is set to (1, 0)
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // ┌─┬─┐
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;             // └─┼─┼─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             //   └─┴─┘
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;
					break;

				case 1:                                                                            // Anchor point remains unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;         //   ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // ┌─┼─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // ├─┼─┘
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 2).IsDisplayed = true;         // └─┘
					break;
			}
		}
	}

	class LShape : Shape
	{
		public void InitDisplay()
		{
			SetPosition();
		}

		public void InitRotate()
		{
			if (RotationIndex == 3)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}
			SetPosition();
		}

		public void SetPosition()
		{ 
			switch (RotationIndex)
			{
				case 0:                                                                            // Anchor point is set to (0, 0)
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // ├─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;             // ├─┼─┐
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 2).IsDisplayed = true;         // └─┴─┘
					break;

				case 1:                                                                            // Anchor point is changed to (1, 0)
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;             // ┌─┬─┬─┐
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;             // ├─┼─┴─┘
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // └─┘
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;
					break;

				case 2:                                                                            // Anchor point remians unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;             // ┌─┬─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // └─┼─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 //   ├─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;             //   └─┘
					break;

				case 3:                                                                            // Anchor point remain unchanged
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;             //     ┌─┐
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;         // ┌─┬─┼─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // └─┴─┴─┘
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;
					break;
			}
		}
	}

	class JShape : Shape
	{
		public void InitDisplay()
		{
			SetPosition();
		}

		public void InitRotate()
		{
			if (RotationIndex == 3)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}

			SetPosition();
		}

		public void SetPosition()
		{
			switch (RotationIndex)
			{
				case 0:                                                                            // Anchor point is set to (1, 0)
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 //   ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             //   ├─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;             // ┌─┼─┤
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 2).IsDisplayed = true;         // └─┴─┘
					break;

				case 1:                                                                            // Anchor point remains unchanged
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;             // ┌─┐
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;         // ├─┼─┬─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // └─┴─┴─┘
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;
					break;

				case 2:                                                                            // Anchor point remians unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;             // ┌─┬─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // ├─┼─┘
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // ├─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;             // └─┘
					break;

				case 3:                                                                            // Anchor point remain unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;             // ┌─┬─┬─┐
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;             // └─┴─┼─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 //     └─┘
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;
					break;
			}
		}
	}

	class TShape : Shape
	{
		public void InitDisplay()
		{
			SetPosition();
		}


		public void InitRotate()
		{
			if (RotationIndex == 3)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}

			SetPosition();
		}

		public void SetPosition()
		{
			switch (RotationIndex)
			{
				case 0:                                                                                    // Anchor point is set to (1, 0)
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         //   ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     // ┌─┼─┼─┐
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;                 // └─┴─┴─┘
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;
					break;

				case 1:                                                                                    // Anchor point remains unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;                     // ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     // ├─┼─┐
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;                 // ├─┼─┘
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 2).IsDisplayed = true;                 // └─┘
					break;

				case 2:                                                                                    // Anchor point remain unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;                     // ┌─┬─┬─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     // └─┼─┼─┘
					Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;                     //   └─┘
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;
					break;

				case 3:                                                                                    // Anchor point remain unchanged
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     //   ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;                     // ┌─┼─┤
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;                 // └─┼─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         //   └─┘
					break;
			}
		}
	}

	class OShape : Shape
	{
		public void InitDisplay()
		{
			SetPosition();
		}

		public void InitRotate()
		{
			SetPosition();
			// It has no ROTATE method
		}

		public void SetPosition()                                                                  // Anchor point is set to (0, 0)
		{
			Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         //   ┌─┬─┐
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;                     //   ├─┼─┤
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     //   └─┴─┘
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;
		}
	}

	enum Action
	{
		Up,
		Down,
		Right,
		Left
	}
}

