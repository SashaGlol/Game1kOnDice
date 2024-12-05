using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1000Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game1k Game = new Game1k();
            //Game.WriteChenceToScore555OutOf();
            //Game.StartItarationChenceToScorePoints();
            Game.ChaenceToScorePointsWithOrWithoutALayout();
            Console.ReadLine();
        }
    }

    internal class Game1k
    {
        private Dictionary<string, int> Points = new Dictionary<string, int>();
        private Dictionary<string, string> RullRebut = new Dictionary<string, string>();
        Random rand = new Random();

        // Create
        public Game1k()
        {
            CreateComdinations(false);
        }
        public Game1k(bool WhriteRull)
        {
            CreateComdinations(WhriteRull);
        }
        private void CreateComdinations(bool WhriteRull)
        {
            int[] r = new int[5];
            for (int i = 0; i < 5; i++) r[i] = 1;

            while (true)
            {
                for(int i = 0; i < 4; i++)
                {
                    if (r[i] > 6)
                    {
                        r[i] = 1;
                        r[i+1]++;
                    }
                }
                if (r[4] > 6) break;

                string rull = "" + r[0] + r[1] + r[2] + r[3] + r[4];
                Points.Add(rull, WhatsPointsM(r));
                RullRebut.Add(rull, WhatsRullsM(r));
                if(WhriteRull)
                {
                    Console.WriteLine(rull +": "+ Points[rull] +" Mutability: "+ RullRebut[rull]); 
                }
                r[0]++;
            }
        }
        private int WhatsPointsM(int[] rull)
        {
            int[] x = new int[5];
            for (int i = 0; i < 5; i++) x[i] = rull[i];
            List<List<int>> prr = new List<List<int>>();
            for (int i = 0; i < 6; i++) prr.Add(new List<int>());
            for (int i = 0; i < 5; i++) prr[x[i] - 1].Add(i);

            int sum = 0;
            for (int i = 0; i < 6; i++)
            {
                if (prr[i].Count() == 5) sum += (i + 1) * 100;
                if (prr[i].Count() == 4) sum += (i + 1) * 20;
                if (prr[i].Count() == 3) sum += (i + 1) * 10;
                if (i == 0) sum *= 10;
            }
            if (prr[0].Count() == 2) sum += 20;
            if (prr[0].Count() == 1) sum += 10;
            if (prr[4].Count() == 2) sum += 10;
            if (prr[4].Count() == 1) sum += 5;

            return sum;
        }
        private string WhatsRullsM(int[] rull)
        {
            int[] x = new int[5];
            for (int i = 0; i < 5; i++) x[i] = rull[i];
            List<List<int>> prr = new List<List<int>>();
            for (int i = 0; i < 6; i++) prr.Add(new List<int>());
            for (int i = 0; i < 5; i++) prr[x[i] - 1].Add(i);

            bool[] ans = new bool[5];

            for (int i = 0; i < 6; i++)
            {
                if (prr[i].Count() > 2)
                {
                    for (int j = 0; j < prr[i].Count(); j++) ans[prr[i][j]] = true;
                }
            }
            if (prr[0].Count() == 2) for (int j = 0; j < prr[0].Count(); j++) ans[prr[0][j]] = true;
            if (prr[0].Count() == 1) ans[prr[0][0]] = true;
            if (prr[4].Count() == 2) for (int j = 0; j < prr[4].Count(); j++) ans[prr[4][j]] = true;
            if (prr[4].Count() == 1) ans[prr[4][0]] = true;

            string ansS = "";
            for (int i = 0; i < 5; i++) ansS += ans[i] ? 1 : 0;
            return ansS;
        }

        // ChenceToScorePoints

        int[] pointsNum = new int[201];
        public void StartItarationChenceToScorePoints()
        {
            int iterations = 0;
            bool iterationsON = true;
            int sumRulls = 0;
            int quantity1M = 1;
            int newStop = quantity1M * 1000000;
            int iterations100S = 0;

            while (iterationsON)
            {
                iterations++;
                iterations100S++;
                sumRulls += OneRullMax(true);

                if (iterations100S == 100000)
                {
                    Console.WriteLine(iterations);
                    iterations100S = 0;
                }
                if (iterations == newStop)
                {
                    WritePontsName(iterations, sumRulls);
                    iterationsON = ReadNewInerations(ref quantity1M);
                    newStop = iterations + (quantity1M * 1000000);
                }
            }
        }
        public int OneRullMax()
        {
            return OneRullMax(false);
        }
        public int OneRullMax(bool Check)
        {
            int[] rull = new int[5];
            int rullSumm = 0;

            int rullPointsOne = 0;
            string reRullPOne = "11111";

            //Console.WriteLine("/// new rull ///");

            while (true)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (reRullPOne[i] == '1') rull[i] = rand.Next(1, 7);
                }
                string rullS = "" + rull[0] + rull[1] + rull[2] + rull[3] + rull[4];
                rullPointsOne = Points[rullS];
                reRullPOne = RullRebut[rullS];

                if (rullPointsOne > 0)
                {
                    rullSumm += rullPointsOne;
                    //Console.WriteLine(rullS);
                    //Console.WriteLine(reRullPOne);
                    //Console.WriteLine(rullSumm);
                }
                else
                {
                    //Console.WriteLine(rullS);
                    //Console.WriteLine("Fall points:" + rullSumm);
                    if (Check) RullCheck(rullSumm);
                    return rullSumm;
                }
            }
        }
        private void RullCheck(int rullPoints)
        {
            for (int i = 0; i < pointsNum.Length; i++)
            {
                if (i * 5 <= rullPoints) pointsNum[i]++;
                else break;
            }
        }
        private void WritePontsName(int iterations, int sumRulls)
        {
            Console.WriteLine(" Iterations chence to score points is finishd ");
            Console.WriteLine(" Iterations = " + iterations);
            Console.WriteLine(" Resaut: ");
            for (int i = 0; i < pointsNum.Length; i++)
            {
                float y = (float)pointsNum[i] / iterations * 100;

                Console.WriteLine(i * 5 + ": " + y);
            }
            double d = (100 - (7.71604938 + ((float)pointsNum[1] / iterations * 100)));
            if (d < 0) d = -d;
            Console.WriteLine("accuracy: " + d);
            Console.WriteLine("pr: " + sumRulls / iterations);
        }
        private bool ReadNewInerations(ref int resout)
        {
            bool answer = true;
            while (true)
            {
                Console.WriteLine("Write how many millions of operations stil to do\nor write 'false' if yor wont to finish");
                string text = Console.ReadLine();
                if (Int32.TryParse(text, out resout)) break;
                if (Boolean.TryParse(text, out answer)) break;
            }
            return answer;
        }


        // Chence other
        public void WriteChenceToScore555OutOf()
        {
            int[] pointsNum = new int[101];
            for (int i = 0; i < pointsNum.Length; i++) pointsNum[i] = 0;

            for (int i = 0; i < pointsNum.Length; i++)
            {
                foreach (int j in Points.Values.ToArray())
                {
                    if (j == i * 5) pointsNum[i]++;
                }
            }

            Console.WriteLine(" Write chence to score 555 points is finished ");
            Console.WriteLine(" Resaut: ");
            for (int i = 1; i < pointsNum.Length; i++)
            {
                Double y = (float)pointsNum[i] / 7776 * 100;
                y = Math.Round(y, 0);

                Console.WriteLine(555 - (i * 5) + ": " + y + " %");
            }
            Console.ReadLine();

        }

        // Chence chaence to score points with or without a layout
        public void ChaenceToScorePointsWithOrWithoutALayout()
        {
            Console.WriteLine("Chaence to score points with or without a layout\n");
            Console.WriteLine("One cube and no layout: " + ((float)2 / 6 * 100) + "%");
            Console.WriteLine("One cube and one layout: " + ((float)3 / 6 * 100) + "%");
            Console.WriteLine("One cube and two layout: " + ((float)4 / 6 * 100) + "%\n");

            Console.WriteLine("Two cube and no layout: " + (EnumerationOfCombinations(2,0) * 100) + "%");
            Console.WriteLine("Two cube and one layout: " + (EnumerationOfCombinations(2, 1) * 100) + "%\n");

            Console.WriteLine("Three cube and no layout: " + (EnumerationOfCombinations(3, 0) * 100) + "%");
            Console.WriteLine("Three cube and one layout: " + (EnumerationOfCombinations(3, 1) * 100) + "%\n");

            Console.WriteLine("Four cube and no layout: " + (EnumerationOfCombinations(3, 0) * 100) + "%\n");
        }
        public double EnumerationOfCombinations(int cube, int layout)
        {
            int numberOfCombination = 6;
            int summOllTrue = 0;
            int[] rull = new int[cube];
            int[] rullRest= new int[5 - cube];
            string fullRull = "";
            for (int i = 0; i < rull.Length; i++) rull[i] = 1;
            if (cube == 2)
            {
                numberOfCombination = 36;
                if (layout == 1)
                {
                    rullRest[0] = 2;
                    rullRest[1] = 2;
                    rullRest[2] = 3;
                }
                else
                {
                    rullRest[0] = 2;
                    rullRest[1] = 3;
                    rullRest[2] = 4;
                }
            }
            if (cube == 3)
            {
                numberOfCombination = 216;
                if (layout == 1)
                {
                    rullRest[0] = 2;
                    rullRest[1] = 2;

                }
                else
                {
                    rullRest[0] = 2;
                    rullRest[1] = 3;
                }
            }
            if (cube == 4)
            {
                numberOfCombination = 1296;
                rullRest[0] = 2;
            }

            while (true)
            {
                for (int i = 0; i < cube - 1; i++)
                {
                    if (rull[i] > 6)
                    {
                        rull[i] = 1;
                        rull[i + 1]++;
                    }
                }
                if (rull[cube - 1] > 6) break;

                if (cube == 2) fullRull = "" + rull[0] + rull[1] + rullRest[0] + rullRest[1] + rullRest[2];
                if (cube == 3) fullRull = "" + rull[0] + rull[1] + rull[2] + rullRest[0] + rullRest[1];
                if (cube == 4) fullRull = "" + rull[0] + rull[1] + rull[2] + rull[3] + rullRest[0];

                if (Points[fullRull] != 0) summOllTrue++;

                rull[0]++;
            }

            return (float)summOllTrue / numberOfCombination;
        }


    }
}
