using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WildLife
{
    class Verbose
    {
        public static void DrawMap(Map map)
        {
            Console.WriteLine("Predators/Prays");
            for (int y = 0; y < map.Height; y++)
            {
                for(int x = 0; x < map.Width; x++)
                {
                    var tile = map.Tilemap[x, y];
                    if (tile is null)
                    {
                        Console.Write($"0\t");
                    }
                    else {
                        int prays = 0;
                        int predators = 0;
                        foreach(Animal animal in tile)
                        {
                            if(animal is Predator)
                            {
                                predators++;
                            }
                            else
                            {
                                prays++;
                            }
                        }
                        Console.Write($"{predators}/{prays}\t");
                    }
                }
                Console.WriteLine();
            }
        }
        public static void AnimalsAlive(Map map)
        {
            foreach (var animal in map.Population)
            {
                Debug.WriteLine($"[{animal.Position.X},{animal.Position.Y}] {animal.GetType()}");
            }
        }
        public static void PopulationCount(Map map)
        {
            int predators=0;
            int prays = 0;
            foreach(Animal animal in map.Population)
            {
                if (animal is Predator) { predators++; } else { prays++; }
            }
            Console.WriteLine($"Population: {predators}/{prays}");
        }
        internal static void TurnCounter(int turn)
        {
            Debug.WriteLine($"Turn: {turn}");
        }
        public static void Birth(Animal animal)
        {
            string sex = animal.Sex ? "male" : "female";
            Console.WriteLine($"{animal.GetType()}, {sex} is born at [{animal.Position.X},{animal.Position.Y}]");
        }
        public static void Death(Animal animal)
        {
            Console.WriteLine($"{animal.GetType()} died at [{animal.Position.X}, {animal.Position.Y}]");
        }
        public static void Movement(Animal animal, Position oldpos)
        {
            Console.WriteLine($"{animal.GetType()} [{animal.Position.X}, {animal.Position.Y}]"+
                $" -> [{oldpos.X},{oldpos.X}]");
        }
    }
}
