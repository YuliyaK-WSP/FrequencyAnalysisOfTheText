using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace FrequencyAnalysisOfTheText
{
    class Program
    {
        //сохраняем кол-во триплетов
        static Dictionary<string, int> triplets = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            
            Console.WriteLine("Введите путь к текстовому файлу:");
            string path = Console.ReadLine();
            watch.Start();
            string text = File.ReadAllText(path);
            Console.WriteLine(text);
            char[] crText = text.ToArray();
            
            
            
            
            for(int i = 0; i < crText.Length-2; i++)
            {
                string triplKey = new string(new char[] { crText[i], crText[i + 1], crText[i + 2] });
                if (!triplets.ContainsKey(triplKey))
                {
                    Thread thread = new Thread(() =>
                    {
                        N(crText, triplKey, crText[i], crText[i + 1], crText[i + 2]);

                    })
                    { IsBackground = true};
                    thread.Start();
                         
             
                    
                }
                
            }
            triplets = triplets.OrderBy(tripl => tripl.Value).ToDictionary(tripl => tripl.Key, tripl => tripl.Value);
            int countRecords;
            countRecords = triplets.Count < 10 ? 0 : triplets.Count - 10;
            
            watch.Stop();

            for (int i = triplets.Count-1; i >= countRecords; i--)
            {
                Console.WriteLine($"Триплет:  {triplets.Keys.ElementAt(i)} повторяется: {triplets.Values.ElementAt(i)}");
                
            }
            Console.WriteLine($"Время работы программы : {watch.ElapsedMilliseconds} /mc");

        }
        static void N(char[] crText,string triplKey, char cr1, char cr2, char cr3)
        {
            int countTrip = 0;
            for (int j = 0; j < crText.Length - 2; j++)
            {
                if (cr1 == crText[j] && cr2 == crText[j + 1] && cr3 == crText[j + 2])
                {
                    countTrip++;
                }

            }

            triplets.Add(triplKey, countTrip);
        }
        
    }
}
