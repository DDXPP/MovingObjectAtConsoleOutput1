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

		}
	}

	class Rectangle
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

		public static Shape[,] DisplayMatrix = new Shape[Width,Height];                            // Define a gameboard that is 10 in Length and 16 in height

		public static void OverAllDraw()
		{
			for (int j = 0; j < Height; j++)
			{
				for (int i = 0; i < Width; i++)
				{
					if (DisplayMatrix[i, j].IsPixelPrinted)
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
		public bool IsPixelPrinted { get; set; }

		public int PositionX { get; set; }

		public int PositionY { get; set; }

		public Shape()                                                                             // Constructor
		{
			IsPixelPrinted = false;
			PositionX = 5;
			PositionY = 0;
		}

		public void InitPrintPixel(int i, int j)                                                   // This block of code seems to be redundant
		{
			Display.DisplayMatrix[i,j].IsPixelPrinted = true;
		}
	}

	public class HorizontalShape : Shape
	{
		public void InitPrint()
		{
			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //   ┌─┬─┬─┬─┐
			Display.DisplayMatrix[PositionX - 1, PositionY]    .IsPixelPrinted = true;             //   └─┴─┴─┴─┘
			Display.DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted = true;
			Display.DisplayMatrix[PositionX + 2, PositionY]    .IsPixelPrinted = true;
		}

		public void Rotate()                                                                       // Rotate about the second block
		{
			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX - 1, PositionY]    .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX + 2, PositionY]    .IsPixelPrinted =false;

			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //   ┌─┐
			Display.DisplayMatrix[PositionX, PositionY + 1]    .IsPixelPrinted = true;             //   ├─┤
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   ├─┤
			Display.DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted = true;             //   ├─┤
		}                                                                                          //   └─┘

	}

	public class SShape : Shape
	{
		public void InitPrint()
		{
			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //     ┌─┬─┐
			Display.DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted = true;             //   ┌─┼─┼─┘
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   └─┴─┘
			Display.DisplayMatrix[PositionX - 1, PositionY - 1].IsPixelPrinted = true;
		}

		public void Rotate()
		{
			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX - 1, PositionY - 1].IsPixelPrinted =false;

			Display.DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted = true;             //   ┌─┐
			Display.DisplayMatrix[PositionX - 1, PositionY]    .IsPixelPrinted = true;             //   ├─┼─┐
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   └─┼─┤
			Display.DisplayMatrix[PositionX - 1, PositionY - 1].IsPixelPrinted = true;             //     └─┘
		}
	}

	public class LShape : Shape
	{
		public void InitPrint()
		{
			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //   ┌─┐
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   ├─┤
			Display.DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted = true;             //   ├─┼─┐
			Display.DisplayMatrix[PositionX + 1, PositionY - 2].IsPixelPrinted = true;             //   └─┴─┘
		}

		public void Rotate()
		{
			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted =false;
			Display.DisplayMatrix[PositionX + 1, PositionY - 2].IsPixelPrinted =false;

			Display.DisplayMatrix[PositionX - 1, PositionY - 1].IsPixelPrinted = true;             //   ┌─┐
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   ├─┤
			Display.DisplayMatrix[PositionX + 1, PositionY - 1].IsPixelPrinted = true;             //   ├─┼─┐
			Display.DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted = true;             //   └─┴─┘
		}
	}

	public class TShape : Shape
	{
		public void InitPrint()
		{
			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //     ┌─┐
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   ┌─┼─┼─┐
			Display.DisplayMatrix[PositionX, PositionY - 2]    .IsPixelPrinted = true;             //   └─┴─┴─┘
			Display.DisplayMatrix[PositionX + 1, PositionY - 1].IsPixelPrinted = true;
		}

	}

	public class OShape : Shape
	{
		public void InitPrint()
		{
			Display.DisplayMatrix[PositionX, PositionY]        .IsPixelPrinted = true;             //   ┌─┬─┐
			Display.DisplayMatrix[PositionX + 1, PositionY]    .IsPixelPrinted = true;             //   ├─┼─┤
			Display.DisplayMatrix[PositionX, PositionY - 1]    .IsPixelPrinted = true;             //   └─┴─┘
			Display.DisplayMatrix[PositionX + 1, PositionY - 1].IsPixelPrinted = true;
		}

		public void Rotate()
		{
			// It has no ROTATE method
		}
	}
}
