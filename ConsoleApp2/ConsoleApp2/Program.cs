using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
	class Program
	{
		enum Days { Monday, Tuesday, Wesnesday, Thursday, Friday, Saturday, Sunday};
		enum Volume
		{
			Low = 20,
			Medium = 50,
			High = 100
		};

		static void Main(string[] args)
		{
			Days today = Days.Sunday;
			Console.WriteLine(today);
			Console.WriteLine((int) today);

			if (today == Days.Sunday)
			{
				Console.WriteLine("Today is Sunday");
			}

			Volume myVolumeLevel = Volume.Low;
			int systemVolume = 0;
			if (myVolumeLevel == Volume.Low)
			{
				systemVolume = (int)Volume.Low;
			}
			else if (myVolumeLevel == Volume.Medium)
			{
				systemVolume = (int)Volume.Medium;
			}
			else if (myVolumeLevel == Volume.High)
			{
				systemVolume = (int)Volume.High;
			}
			Console.WriteLine("System Volume is : " + systemVolume);

			foreach (string s in System.Enum.GetNames(typeof(Volume)))
			{
				Console.WriteLine(s);
			}

			foreach (Volume volume in System.Enum.GetValues(typeof(Volume)))
			{
				Console.WriteLine("{0} = {1}", volume, (int)volume);
			}

			int[] numbers = new int[5] { 4, 5, 6, 7, 8 };
			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i] = i * i;
			}
			int[] numbers2 = { 1, 2, 3, 4, 5 };
			Console.WriteLine(numbers);
			Console.WriteLine("Rupendra  Bandyopadhyay");
			Console.ReadKey();

			int[][] jaggedarray = new int[3][];
			jaggedarray[0] = new int[] { 1, 2, 3 };
			jaggedarray[1] = new int[] { 5, 6, 7, 8 };
			jaggedarray[2] = new int[] { 11, 12, 13 };

			// Multi Dimensional Array
			int[,] multiarray = new int[5,3];
			int[,] arr2D = new int[5, 3] {
				{1,2,3 },
				{4,5,6 },
				{4,1,0 },
				{2,2,2 },
				{0,0,1 }
			};
			Console.WriteLine(arr2D[3,1]);
		}
	}

}
