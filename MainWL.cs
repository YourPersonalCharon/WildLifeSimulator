using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WildLife
{ 
    class MainWL
    {
        static Map InitializeMap(int height, int width, int population, double coefficient = 0.5)
        {
            Map map = new Map(width, height);
            map.Populate(population, coefficient);

            TurnsManager.GenerateTilemap(map);

            Verbose.TurnCounter(0);
            Verbose.AnimalsAlive(map);
            Verbose.DrawMap(map);
            Verbose.PopulationCount(map);
            Console.WriteLine("");
            return map;
        }

        static void Main(string[] args)
        {
            int width = 5;
            int height = 5;
            int population = 70;
            double coefficient = 0.3; // higher coeff - more predators 0<=x<1

            int turns = 100;

            Map map = InitializeMap(width, height, population, coefficient);
            TurnsManager.Run(map, turns ,true);
        }
    }
}