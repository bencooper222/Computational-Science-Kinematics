using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace CompuSci
{
    class Kinematics
    {

        public static void euler(double position, double step, double startTime, double totalTime, double velocity, double acceleration, double airCoefficient, double mass, double spring)
        {
            StreamWriter writer = File.CreateText("results.txt");

            double airResistance = 0;
            double hookeAcceleartion = 0;
            Boolean massExistence = false;

            if (mass != 0) // verifies that the mass isn't zero so that we can divide the forces. If it is zero, then the object is a massless particle that can't be 
            {
                massExistence = true;

            }
            for (double i = startTime; i < totalTime; i += step)
            {
                if (i == startTime)
                {
                    position = position; // we don't want it to change the starting position at the starting time
                    velocity = velocity; // ditto^  
                }
                else
                {
                    if (massExistence)
                    {
                        hookeAcceleartion = -spring * position / mass;

                        if (velocity > 0)
                        {
                            airResistance = airCoefficient * Math.Pow(velocity, 2) / mass;
                        }
                        else
                        {
                            airResistance = -airCoefficient * Math.Pow(velocity, 2) / mass;
                        }
                    }
                    position = position + step * velocity;
                    velocity = velocity + step * (acceleration - airResistance + hookeAcceleartion);

                }
                writer.WriteLine(i + "\t" + position + "\t" + velocity + "\t" + (acceleration - airResistance + hookeAcceleartion));


            }
            writer.Close();
        }


        public static int getLevel()
        {
            Console.Write("Which level would you like to do?");
            int level = Console.Read();
            return level;
        }


        static void Main(string[] args)
        {
            double interval = .1;
            double timeStart = 0;
            double timeEnd = 100;
            double position = 0; // set this to wherever you want to start
            double velocity;
            double accelerate;
            double airCoefficient;
            double mass;
            double hooke;
            int level = getLevel();

            switch (level - 48) // no idea why it needs the minus 48
            {
                case 1: // this is the level one
                    velocity = 2;

                    euler(position, interval, timeStart, timeEnd, velocity, 0, 0, 0, 0);
                    break;

                case 2: // this is the level 2
                    velocity = 0;
                    accelerate = 9.8;

                    euler(position, interval, timeStart, timeEnd, velocity, accelerate, 0, 0, 0);
                    break;

                case 3: //this is the level 3
                    velocity = 0;
                    accelerate = 9.8;
                    airCoefficient = .3;
                    mass = 5;

                    euler(position, interval, timeStart, timeEnd, velocity, accelerate, airCoefficient, mass, 0);
                    break;

                case 4: //this is the challenge level
                    velocity = 0;
                    accelerate = 9.8;
                    airCoefficient = .3;
                    mass = 5;
                    hooke = 2;

                    euler(position, interval, timeStart, timeEnd, velocity, accelerate, airCoefficient, mass, hooke);
                    break;
            }

            Console.Write("Press any key to terminate");
            Console.ReadKey(true); // this just lets me see the Console after the rest has finished execution
        }
    }
}

