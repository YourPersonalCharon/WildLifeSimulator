using System;
using System.Collections.Generic;
using System.Text;

namespace WildLife
{
    public struct Position
    {
        public int X
        { get; set; }
        public int Y
        { get; set; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    public class Map
    {
        public int Width { get; }
        public int Height { get; }

        public List<Animal>[,] Tilemap;
        public List<Animal> Population = new List<Animal>() { };

        public void Populate(int number)
        {
            Random rand = new Random();
            for (int i = 0; i < number; i++)
            {

                if (rand.Next() > (Int32.MaxValue / 2))
                {
                    Population.Add(new Predator(this));
                }
                else
                {
                    Population.Add(new Pray(this));
                }
            }

            Population.Sort((Animal a, Animal b) => a.Position.X - b.Position.X == 0 ?
                                                       a.Position.Y - b.Position.Y : a.Position.X - b.Position.X);
        }
        public void Populate(int number, double coeff)// higher coeff - more predators. < 1
        {
            Random rand = new Random();
            for(int i = 0; i < number; i++)
            {
                if (rand.NextDouble() < coeff)
                {
                    Population.Add(new Predator(this));
                }
                else
                {
                    Population.Add(new Pray(this));
                }
            }
        }

        public void Populate<T>(int number, int x, int y) where T : Animal //TODO Фабрика
        {
            for (int i = 0; i < number; i++)
            {
                if (typeof(T) == typeof(Predator))
                {
                    Population.Add(new Predator(this, x, y));
                }
                else if (typeof(T) == typeof(Pray))
                {
                    Population.Add(new Pray(this, x, y));
                }
            }

            Population.Sort((Animal a, Animal b) => a.Position.X - b.Position.X == 0 ?
                                                       a.Position.Y - b.Position.Y : a.Position.X - b.Position.X);
        }
    
        public Map(int width, int height)
        {
            Width = width;
            Height = height;

            Tilemap = new List<Animal>[Width, Height];
        }
    }
}
