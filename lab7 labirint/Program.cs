using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7_labirint
{
    class Program
    {
        static int[] start;

        public struct Locatie
        {
            public int Linie, Coloana;
        }

        static void Main(string[] args)
        {
            Locatie start = new Locatie();

            start.Linie = 2;
            start.Coloana = 1;

            Locatie stop = new Locatie();
            stop.Linie = 2;
            stop.Coloana = 7;

            var str = File.ReadAllLines("in.txt");
            int[] vect = str[0].Split(' ').Select(int.Parse).ToArray();
            int[,] mat = new int[vect[0], vect[1]];
            int rCol = 0;
            for (int i = 1; i < vect[0] + 1; i++)
            {
                var tmp = str[i].ToCharArray();
                for (int j = 0; j < tmp.Length; j++)
                {
                    char c = tmp[j];
                    if (c != '-')
                    {
                        if (c != '1')
                        {

                            mat[i - 1, rCol] = Convert.ToInt32(tmp[j].ToString());
                            rCol++;
                        }
                        else
                        {
                            mat[i - 1, rCol] = -1;
                            rCol++;
                        }
                        //Console.WriteLine("Mers");
                    }
                }
                rCol = 0;
            }
            for (int i = 0; i < vect[0]; i++)
            {
                for (int j = 0; j < vect[1]; j++)
                {
                    Console.Write(mat[i, j] + " ");
                }
                Console.WriteLine();
            }
            var result = Lee(mat, start, stop);
            Console.WriteLine("Datele au fost trimite cu succes!");
            foreach (var item in result)
            {
                Console.WriteLine(item.Linie + " " + item.Coloana);
            }


            Console.ReadKey();
        }



        static List<Locatie> Lee(int[,] labirint, Locatie start, Locatie stop)
        {
            int m = labirint.GetLength(0);
            int n = labirint.GetLength(1);
            int[] dLinie = { 0, 0, 1, -1 };
            int[] dColoana = { 1, -1, 0, 0 };
            labirint[start.Linie, start.Coloana] = 1;
            Queue<Locatie> lst = new Queue<Locatie>() { };
            lst.Enqueue(start);
            labirint[start.Linie, start.Coloana] = 1;
            while (lst.Count != 0)
            {
                Locatie curent = lst.Dequeue();
                for (int t = 0; t < dLinie.Length; t++)
                {
                    int linieVecin = curent.Linie + dLinie[t];
                    int coloanaVecin = curent.Coloana + dColoana[t];
                    if (linieVecin >= 0 && linieVecin < m && coloanaVecin >= 0 && coloanaVecin < n && labirint[linieVecin, coloanaVecin] == 0)
                    {
                        labirint[linieVecin, coloanaVecin] = labirint[curent.Linie, curent.Coloana] + 1;
                        lst.Enqueue(new Locatie() { Linie = linieVecin, Coloana = coloanaVecin });
                    }
                }
            }
            List<Locatie> drum = new List<Locatie>();
            int lungimeDrum = labirint[stop.Linie, stop.Coloana];
            int linie = stop.Linie;
            int coloana = stop.Coloana;

            while (!(linie == start.Linie && coloana == start.Coloana))
            {
                drum.Add(new Locatie() { Linie = linie, Coloana = coloana });
                for (int t = 0; t < dLinie.Length; t++)
                {
                    int linieVecin = linie + dLinie[t];
                    int coloanaVecin = coloana + dColoana[t];
                    if (linieVecin >= 0 && linieVecin < m && coloanaVecin >= 0 && coloanaVecin < n && labirint[linieVecin, coloanaVecin] == lungimeDrum - 1)
                    {
                        linie = linieVecin;
                        coloana = coloanaVecin;
                        lungimeDrum--;
                        break;
                    }
                }
            }

            drum.Add(start);
            drum.Reverse();
            return drum;
        }
    }
}
