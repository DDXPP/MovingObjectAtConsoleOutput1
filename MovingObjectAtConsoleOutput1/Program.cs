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
			/*
			int x = 0;
			Rectangle rectangle = new Rectangle();
			rectangle.SetLocation();
			rectangle.Print();
			Console.CursorVisible = false;
			do
			{
				ConsoleKeyInfo Key = Console.ReadKey();

				if (Key.KeyChar.ToString() == "w")
				{

					rectangle.Up();
				}

				if (Key.KeyChar.ToString() == "s")
				{
					rectangle.Down();
				}

				if (Key.KeyChar.ToString() == "a")
				{
					rectangle.Left();
				}

				if (Key.KeyChar.ToString() == "d")
				{
					rectangle.Right();
				}

			} while (x == 0);
			*/

			/*
			HorizontalShape horizontalShape = new HorizontalShape();
			horizontalShape.InitPrint();
			Display.OverAllDraw();
			*/
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

			SelectFallingShapeIndex();

			Pixel pixel = new Pixel();

			pixel.OverallDisplay();

			do
			{
				char key = Console.ReadKey(true).KeyChar;
				Display.Move(key);
				pixel.OverallDisplay();

			} while (true);
		}

		public static void SelectFallingShapeIndex()
		{
			FallingShape fallingShape = FallingShape.ZShape;

			int fallingShapeIndex = (int)fallingShape;

			switch (fallingShapeIndex)
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
	}

	/*	class Rectangle
		{
			public int Width { get; set; }
			public int Height { get; set; }
			public int PosX { get; set; }
			public int PosY { get; set; }

			public Rectangle()                                                               //Constructor
			{
				Width = 5;
				Height = 5;
				PosX = 2;
				PosY = 2;
			}

			static int DisplayMatrixWidth = 10;
			static int DisplayMatrixHeight = 10;
			bool[,] DisplayMatrix = new bool[DisplayMatrixWidth, DisplayMatrixHeight];

			public void SetLocation()
			{
				int i = PosX, j = PosY;
				for (i = PosX; i < Width + PosX; i++)                                        // Draw Upper & Lower Side
				{
					DisplayMatrix[i, PosY] = true;
					DisplayMatrix[i, Height + PosY - 1] = true;
				}
				for (j = PosY; j < Height + PosY; j++)                                       // Draw Left & Right Side
				{
					DisplayMatrix[PosX, j] = true;
					DisplayMatrix[Width + PosX - 1, j] = true;
				}
			}

			public void Print()
			{
				Console.Clear();

				for (int j = 0; j < DisplayMatrixHeight; j++)
				{
					for (int i = 0; i < DisplayMatrixWidth; i++)
					{
						if (DisplayMatrix[i, j] == true)
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

			public void Moving(object sender, EventArgs e)
			{

			}

			public void Left()
			{
				for (int j = 0; j < DisplayMatrixHeight; j++)
				{
					for (int i = 0; i < DisplayMatrixWidth; i++)
					{
						if (DisplayMatrix[i, j] == true)
						{
							DisplayMatrix[i, j] = false;
							DisplayMatrix[i - 1, j] = true;
						}
					}
				}
				Print();
			}

			public void Right()
			{
				for (int j = 0; j < DisplayMatrixHeight; j++)
				{
					for (int i = DisplayMatrixWidth - 1; i >= 0; i--)
					{
						if (DisplayMatrix[i, j] == true)
						{
							DisplayMatrix[i, j] = false;
							DisplayMatrix[i + 1, j] = true;
						}
					}
				}
				Print();
			}

			public void Up()
			{
				for (int j = 0; j < DisplayMatrixHeight; j++)
				{
					for (int i = 0; i < DisplayMatrixWidth; i++)
					{
						if (DisplayMatrix[i, j] == true)
						{
							DisplayMatrix[i, j] = false;
							DisplayMatrix[i, j - 1] = true;
						}
					}
				}
				Print();
			}

			public void Down()
			{
				for (int j = DisplayMatrixHeight - 1; j >= 0; j--)
				{
					for (int i = 0; i < DisplayMatrixWidth; i++)
					{
						if (DisplayMatrix[i, j] == true)
						{
							DisplayMatrix[i, j] = false;
							DisplayMatrix[i, j + 1] = true;
						}
					}
				}
				Print();
			}

		}

		public class Display
		{
			public static int Width
			{
				get
				{
					return 10;
				}
			}

			public static int Height
			{
				get
				{
					return 16;
				}
			}


			public static void OverAllDraw()
			{
				for (int j = 0; j < Height; j++)
				{
					for (int i = 0; i < Width; i++)
					{
						if (Shape.DisplayMatrixExchanger()[i,j].IsPixelPrinted)
						{
							Console.Write("■");
						}
						else
						{
							Console.Write("□");
						}
						Console.WriteLine();
					}
				}
			}
		}

		public class Shape
		{
			public static Shape[,] DisplayMatrix = new Shape[Display.Width,Display.Height];                            // Define a gameboard that is 10 in Length and 16 in height

			public static Shape[,] DisplayMatrixExchanger()
			{
				return DisplayMatrix;
			}

			public bool IsPixelPrinted { get; set; }

			public int PositionX { get; set; }

			public int PositionY { get; set; }

			public Shape()                                                                             // Constructor
			{
				IsPixelPrinted = false;
				PositionX = 5;
				PositionY = 0;

				for (int j = 0; j < Display.Height; j++)
				{
					for (int i = 0; i < Display.Width; i++)
					{
						DisplayMatrix[i, j] = new Shape();
					}
				}

			}

			public void InitPrintPixel(int i, int j)                                                   // This block of code seems to be redundant
			{
				DisplayMatrix[i,j].IsPixelPrinted = true;
			}
		}

		public class HorizontalShape : Shape
		{
			public void InitPrint()
			{
				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //   ┌─┬─┬─┬─┐
				DisplayMatrix[PositionX - 1, PositionY]    .IsPixelPrinted = true;             //   └─┴─┴─┴─┘
				DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted = true;
				DisplayMatrix[PositionX + 2, PositionY]    .IsPixelPrinted = true;
			}

			public void Rotate()                                                                       // Rotate about the second block
			{
				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted =false;
				DisplayMatrix[PositionX - 1, PositionY]    .IsPixelPrinted =false;
				DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted =false;
				DisplayMatrix[PositionX + 2, PositionY]    .IsPixelPrinted =false;

				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //   ┌─┐
				DisplayMatrix[PositionX, PositionY + 1]    .IsPixelPrinted = true;             //   ├─┤
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   ├─┤
				DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted = true;             //   ├─┤
			}                                                                                          //   └─┘

		}

		public class SShape : Shape
		{
			public void InitPrint()
			{
				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //     ┌─┬─┐
				DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted = true;             //   ┌─┼─┼─┘
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   └─┴─┘
				DisplayMatrix[PositionX - 1, PositionY - 1].IsPixelPrinted = true;
			}

			public void Rotate()
			{
				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted =false;
				DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted =false;
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted =false;
				DisplayMatrix[PositionX - 1, PositionY - 1].IsPixelPrinted =false;

				DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted = true;             //   ┌─┐
				DisplayMatrix[PositionX - 1, PositionY]    .IsPixelPrinted = true;             //   ├─┼─┐
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   └─┼─┤
				DisplayMatrix[PositionX - 1, PositionY - 1].IsPixelPrinted = true;             //     └─┘
			}
		}

		public class LShape : Shape
		{
			public void InitPrint()
			{
				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //   ┌─┐
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   ├─┤
				DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted = true;             //   ├─┼─┐
				DisplayMatrix[PositionX + 1, PositionY - 2].IsPixelPrinted = true;             //   └─┴─┘
			}

			public void Rotate()
			{
				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted =false;
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted =false;
				DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted =false;
				DisplayMatrix[PositionX + 1, PositionY - 2].IsPixelPrinted =false;

				DisplayMatrix[PositionX - 1, PositionY - 1].IsPixelPrinted = true;             //   ┌─┐
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   ├─┤
				DisplayMatrix[PositionX + 1, PositionY - 1].IsPixelPrinted = true;             //   ├─┼─┐
				DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted = true;             //   └─┴─┘
			}
		}

		public class TShape : Shape
		{
			public void InitPrint()
			{
				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //     ┌─┐
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   ┌─┼─┼─┐
				DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted = true;             //   └─┴─┴─┘
				DisplayMatrix[PositionX + 1, PositionY - 1].IsPixelPrinted = true;
			}

		}

		public class OShape : Shape
		{
			public void InitPrint()
			{
				DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //   ┌─┬─┐
				DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted = true;             //   ├─┼─┤
				DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   └─┴─┘
				DisplayMatrix[PositionX + 1, PositionY - 1].IsPixelPrinted = true;
			}

			public void Rotate()
			{
				// It has no ROTATE method
			}
		}
	*/


	class Display
	{
		protected static int width { get; set; } = 10;
		protected static int height { get; set; } = 16;

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

		public void OverallDisplay()
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
			AnchorPointX--;
			RemovePreviousDisplay();
		}

		public void InitRight()
		{
			AnchorPointX++;
			RemovePreviousDisplay();
		}

		public void InitDown()
		{
			AnchorPointY++;
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

