using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingObjectAtConsoleOutput
{
	class AXDD_AnimationLibrary
	{
		public static void Decay(double decayConstant, Pixel pixel)
		{
			double p = 1 - Math.Exp(-decayConstant);

			double randomNumber = RandomClassLibrary.Random.GetRandom();

			if (randomNumber < p)
			{
				pixel.Isplaced = false;
				pixel.IsDisplayed = false;
			}
		}
	}
}
