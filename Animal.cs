using System;
using System.Collections.Generic;

namespace WildLife
{
    public class Animal
    {
        public static void Breed<T>(Map map) where T : Animal
        {
            List<T> animals = new List<T>();
            foreach (var animalAlive in map.Population)
            {
                if (animalAlive is T species)
                {
                    animals.Add(species);
                }
            }

            Dictionary<Position, List<T>> positions = new Dictionary<Position, List<T>>();
            foreach (var animal in animals)
            {
                if (positions.ContainsKey(animal.Position))
                {
                    positions[animal.Position].Add(animal);
                }
                else
                {
                    positions.Add(animal.Position, new List<T> { animal });
                }
            }
            foreach (var pos in positions)
            {
                if (pos.Value.Count != 1)
                {
                    var animalsOnTile = pos.Value;
                    int males = 0;
                    int females = 0;
                    foreach (var animal in animalsOnTile)
                    {
                        if (animal.Sex)
                        {
                            males++;
                        }
                        else
                        {
                            females++;
                        }
                    }
                    map.Populate<T>((males < females ? males : females), pos.Key.X, pos.Key.Y);
                }
            }
        }
        public Position Position;
        public bool Sex { get; } //true = m, false = f;
        public Map Home { get; set; }

        public virtual void Move()
        {
            Random rand = new Random();
            Position oldpos = Position;

            Position.X += rand.Next(Position.X == 0 ? 0 : -1,
                Position.X == Home.Width - 1 ? 1 : 2);
            Position.Y += rand.Next(Position.Y == 0 ? 0 : -1,
                Position.Y == Home.Height - 1 ? 1 : 2);

            Verbose.Movement(this,oldpos);
        }

        public void Die()
        {
            Home.Population.Remove(this);
            Verbose.Death(this);
        }
        protected Animal(Map map)
        {
            Random rand = new Random();
            Position.X = rand.Next(map.Width);
            Position.Y = rand.Next(map.Height);
            Sex = rand.Next() > (Int32.MaxValue / 2);
            Home = map;
            Verbose.Birth(this);
        }
        public Animal(Map map, int x, int y)
        {
            Random rand = new Random();
            Position.X = x;
            Position.Y = y;
            Sex = rand.Next() > (Int32.MaxValue / 2);
            Home = map;
            Verbose.Birth(this);
        }

    }

    public class Predator : Animal
    {
        public int Energy { get; set; }
        public void Eat()
        {
            List<Animal> tile = Home.Tilemap[Position.X,Position.Y];
            Animal pray = tile.Find(x => x is Pray);
            if (pray != null)
            {
                pray.Die();
                Energy = 2;
            }
        }

        public override void Move()
        {
            base.Move();
            Energy -= 1;
        }
        public Predator(Map map) : base(map)
        {
            Energy = 2;
        }
        public Predator(Map map, int x, int y) : base(map, x, y)
        {
            Energy = 1;
        }
    }

    public class Pray : Animal
    {
        public Pray(Map map) : base(map)
        {
        }
        public Pray(Map map, int x, int y) : base(map, x, y)
        {

        }
    }

}
