using System;
using System.Collections.Generic;
using System.Text;

namespace WildLife
{
    class Phases
    {
        public delegate void PhasesDel(Map map);

        public static List<PhasesDel> PhasesList = new List<PhasesDel>()
        {
            MovingPhase,
            Verbose.DrawMap,
            EatingPhase,
            HungerPhase,
            BreedingPhase,
            TurnsManager.GenerateTilemap
        };

        public static void HungerPhase(Map map)
        {
            Animal[] population = new Animal[map.Population.Count];
            map.Population.CopyTo(population);
            foreach (var animal in population)
            {
                if(animal is Predator pr && pr.Energy==0) {
                    pr.Die();
                } 
            }
        }
        public static void MovingPhase(Map map)
        {
            foreach(Animal animal in map.Population)
            {
                animal.Move();
            }
            TurnsManager.GenerateTilemap(map);
        }
        public static void EatingPhase(Map map)
        {
            Animal[] population = new Animal[map.Population.Count];
            map.Population.CopyTo(population);
            foreach (var animal in population)
            {
                if (animal is Predator predator)
                {
                    predator.Eat();
                }
            }
        }
        public static void BreedingPhase(Map map)
        {
            Animal.Breed<Predator>(map);
            Animal.Breed<Pray>(map);
        }
    }
    class TurnsManager
    {
        
        public static void Run(Map map, int turns, bool debugMode=false)
        {
            for(int i = 0; i < turns; i++)
            {
                Console.WriteLine($"turn: {i+1}");
                foreach (var phase in Phases.PhasesList)
                {
                    phase.Invoke(map);
                    Verbose.TurnCounter(i + 1);
                    Verbose.DrawMap(map);
                    Verbose.PopulationCount(map);

                }
                if (debugMode)
                {
                    Verbose.AnimalsAlive(map);
                    Console.WriteLine("_________________");
                }
            }
        }
        public static void GenerateTilemap(Map map)
        {
            Array.Clear(map.Tilemap, 0, map.Tilemap.Length);
            foreach (Animal animal in map.Population)
            {
                (map.Tilemap[animal.Position.X, animal.Position.Y] ??= new List<Animal>()).Add(animal);
            }
        }
    }
}
