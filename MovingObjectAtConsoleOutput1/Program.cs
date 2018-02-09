using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingObjectAtConsoleOutput
{
	class Program
	{
		static void Main(string[] args)
		{
			//initialization();

			//MainLoop();

			Random random = new Random();

			for (int i = 0; i < 6; i++)
			{
				//Console.WriteLine(RandomClassLibrary.Random.GetRandomInteger(0, 6));
				Console.WriteLine(random.Next(0, 6));
				Console.ReadKey();
			}

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

			SelectFallingShapeIndex();

			Pixel.OverallDisplay();

			Random random = new Random();
			ShapeIndex = random.Next(0, 6);

			do
			{
				char key = Console.ReadKey(true).KeyChar;
				Display.Move(key);
				Pixel.OverallDisplay();

			} while (true);
		}

		public static void SelectFallingShapeIndex()
		{

			FallingShape fallingShape = FallingShape.SShape;

			int fallingShapeIndex = (int)fallingShape;

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
			for (int j = 0; j < Display.height; j++)
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
			for (int j = 0; j < Display.height; j++)
			{
				if (Pixel.GetPixel(Display.width - 1, j).IsDisplayed)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsTouchLowerBorder()
		{
			for (int i = 0; i < Display.width; i++)
			{
				if (Pixel.GetPixel(i, Display.height - 1).IsDisplayed)
				{
					return true;
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

		public static int ShapeIndex { get; set; }
	}

	class Display
	{
		public static int width { get; set; } = 10;
		public static int height { get; set; } = 16;

		public static void Move(char key)
		{
			Shape shape = new Shape();

			switch (key.ToString())
			{
				case "w":
					shape.InitUp();
					Console.Clear();
					break;

				case "s":
					shape.InitDown();
					Console.Clear();
					Shape.initDisplayDelegate();
					break;

				case "a":
					Console.Clear();
					shape.InitLeft();
					Shape.initDisplayDelegate();
					break;

				case "d":
					Console.Clear();
					shape.InitRight();
					Shape.initDisplayDelegate();
					break;
			}
		}
	}

	class Pixel : Display
	{
		public bool IsDisplayed = false;
		public bool Isplaced = false;

		public static Pixel[,] DisplayMatrix = new Pixel[width, height];

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
					if (GetPixel(i, j).IsDisplayed)
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
		protected static int AnchorPointX { get; set; } = 4;
		protected static int AnchorPointY { get; set; } = 0;
		protected static int RotationIndex { get; set; } = 1;

		public void InitUp()
		{
			Program.SelectFallingShapeIndex();

			RemovePreviousDisplay();

			initRotateDelegate();
		}

		public void InitLeft()
		{
			if (!Program.IsTouchLeftBorder())
			{
				AnchorPointX--;
			}
			RemovePreviousDisplay();
		}

		public void InitRight()
		{
			if (!Program.IsTouchRightBorder())
			{
				AnchorPointX++;
			}
			RemovePreviousDisplay();
		}

		public void InitDown()
		{
			if (!Program.IsTouchLowerBorder())
			{
				AnchorPointY++;
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
		
		public void InitDisplay()                                                           // Anchor point is set to (1, 0)
		{
			Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         // ┌─┬─┬─┬─┐
			Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;                     // └─┴─┴─┴─┘
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;
			Pixel.GetPixel(AnchorPointX + 2, AnchorPointY).IsDisplayed = true;
		}


		public void InitRotate()                                                       // Anchor point remains unchanged
		{
			switch (RotationIndex)
			{
				case 0:
					InitDisplay();
					break;

				case 1:
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY - 1).IsDisplayed = true;             // ├─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // ├─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;             // ├─┤
					break;                                                                         // └─┘
			}

			if (RotationIndex == 1)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}
		}
	}

	class SShape : Shape
	{
		public void InitDisplay()                                                           // Anchor point is set to (1, 0)
		{
			Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         //   ┌─┬─┐
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;                     // ┌─┼─┼─┘
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     // └─┴─┘
			Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;
		}

		public void InitRotate()
		{
			switch (RotationIndex)
			{
				case 0:
					InitDisplay();
					break;

				case 1:                                                                            // Anchor point remains unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;         // ┌─┐
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;             // ├─┼─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // └─┼─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;             //   └─┘
					break;   
			}

			if (RotationIndex == 1)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}
		}
	}

	class ZShape : Shape
	{
		public void InitDisplay()                                                           // Anchor point is set to (1, 0)
		{
			Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         // ┌─┬─┐
			Pixel.GetPixel(AnchorPointX - 1, AnchorPointY).IsDisplayed = true;                     // └─┼─┼─┐
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     //   └─┴─┘
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;
		}

		public void InitRotate()
		{
			switch (RotationIndex)
			{
				case 0:
					InitDisplay();
					break;

				case 1:                                                                            // Anchor point remains unchanged
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;         //   ┌─┐
					Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                 // ┌─┼─┤
					Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;             // ├─┼─┘
					Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 2).IsDisplayed = true;         // └─┘
					break;
			}

			if (RotationIndex == 1)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}
		}

	}

	class LShape : Shape
	{
		public void InitDisplay()                                                           // Anchor point is set to (0, 0)
		{
			Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         // ┌─┐
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     // ├─┤
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;                     // ├─┼─┐
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 2).IsDisplayed = true;                 // └─┴─┘
		}

		public void InitRotate()
		{
			switch (RotationIndex)
			{
				case 0:
					InitDisplay();
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

			if (RotationIndex == 3)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}
		}
	}

	class JShape : Shape
	{
		public void InitDisplay()                                                           // Anchor point is set to (1, 0)
		{
			Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         //   ┌─┐
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     //   ├─┤
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 2).IsDisplayed = true;                     // ┌─┼─┤
			Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 2).IsDisplayed = true;                 // └─┴─┘
		}

		public void InitRotate()
		{
			switch (RotationIndex)
			{
				case 0:
					InitDisplay();
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

			if (RotationIndex == 3)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}
		}
	}


	class TShape : Shape
	{
		public void InitDisplay()                                                           // Anchor point is set to (1, 0)
		{
			Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         //   ┌─┐
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     // ┌─┼─┼─┐
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;                 // └─┴─┴─┘
			Pixel.GetPixel(AnchorPointX - 1, AnchorPointY + 1).IsDisplayed = true;
		}


		public void InitRotate()
		{
			switch (RotationIndex)
			{
				case 0:
					InitDisplay();
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

			if (RotationIndex == 3)
			{
				RotationIndex = 0;
			}
			else
			{
				RotationIndex++;
			}
		}
	}

	class OShape : Shape
	{
		public void InitDisplay()                                                           // Anchor point is set to (0, 0)
		{
			Pixel.GetPixel(AnchorPointX, AnchorPointY).IsDisplayed = true;                         //   ┌─┬─┐
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY).IsDisplayed = true;                     //   ├─┼─┤
			Pixel.GetPixel(AnchorPointX, AnchorPointY + 1).IsDisplayed = true;                     //   └─┴─┘
			Pixel.GetPixel(AnchorPointX + 1, AnchorPointY + 1).IsDisplayed = true;
		}

		public void InitRotate()
		{
			InitDisplay();
			// It has no ROTATE method
		}
	}

	enum Action
	{
		Up,
		Down,
		Right,
		Left
	}

	enum FallingShape
	{
		HorizonShape,
		SShape,
		ZShape,
		LShape,
		JShape,
		TShape,
		OShape
	};
}

