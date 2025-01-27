using System;
using System.Drawing;

class Program
{ 
    public static double runLoop(double positionX, double positionY, double positionZ, double velocityX, double velocityY, double velocityZ, 
                                 double mass, double dragCoefficient, double springCoefficient, double unstretchedLength, double deltaT, double runTime)
    {
        double acceleration = 0;
        double accelerationX = 0;
        double accelerationY = 0;
        double accelerationZ = 0;

        double velocity = 0;

        double airResistanceForceX = 0;
        double airResistanceForceY = 0;
        double airResistanceForceZ = 0;

        double netForceX = 0;
        double netForceY = 0;
        double netForceZ = 0;

        double springForceX = 0;
        double springForceY = 0;
        double springForceZ = 0;

        double gravityForce = -9.8 * mass;

        double time = 0;
        double finalTime = 0;
        Console.WriteLine("Time (s)\t\tX (m)\t\t\tY (m)\t\t\tZ (m)\t\t\t Velocity (m/s)\t\t\t Acceleration (m/s^2)");
        while (time <= runTime)
        {

            velocity = Math.Sqrt(Math.Pow(velocityX, 2) + Math.Pow(velocityZ, 2) + Math.Pow(velocityY, 2));
            acceleration = Math.Sqrt(Math.Pow(accelerationX, 2) + Math.Pow(accelerationY, 2) + Math.Pow(accelerationZ, 2));
            Console.WriteLine($"{Math.Round(time, 3)}\t\t\t{Math.Round(positionX, 4)}\t\t\t{Math.Round(positionY, 4)}\t\t\t{Math.Round(positionZ, 4)}\t\t\t{Math.Round(velocity, 4)}\t\t\t{Math.Round(acceleration, 4)}");

            if (velocityX != 0)
            {
                airResistanceForceX = -1 * dragCoefficient * velocityX * velocityX * Math.Sign(velocityX);
                airResistanceForceY = -1 * dragCoefficient * velocityY * velocityY * Math.Sign(velocityY);
                airResistanceForceZ = -1 * dragCoefficient * velocityZ * velocityZ * Math.Sign(velocityZ);
            }


            //Calculate the distance from the ball's position to the unstretched end of the spring going in the direction of the ball for x in Hooke's Law
            if (positionX != 0 && positionY != 0 && positionZ != 0)
            {
                double magnitude = Math.Sqrt(Math.Pow(positionX, 2) + Math.Pow(positionY, 2) + Math.Pow(positionZ, 2));
                double stretch = magnitude - unstretchedLength;
                double springForce = -springCoefficient * stretch;

                springForceX = springForce * (positionX / magnitude);

                springForceY = springForce * (positionY / magnitude);

                springForceZ = springForce * (positionZ / magnitude);
            }

            /*
            springForceX = -1 * springCoefficient * (Math.Abs(positionX) - Math.Abs(positionX) * unstretchedLength / Math.Sqrt( Math.Pow(positionX, 2) + Math.Pow(positionY, 2) + Math.Pow(positionZ, 2) ) ) ;
            springForceY = -1 * springCoefficient * (Math.Abs(positionY) - Math.Abs(positionY) * unstretchedLength / Math.Sqrt(Math.Pow(positionX, 2) + Math.Pow(positionY, 2) + Math.Pow(positionZ, 2))) ;
            springForceZ = -1 * springCoefficient * (Math.Abs(positionZ) - Math.Abs(positionZ) * unstretchedLength / Math.Sqrt(Math.Pow(positionX, 2) + Math.Pow(positionY, 2) + Math.Pow(positionZ, 2))) ; */


            netForceX = airResistanceForceX + springForceX;
            netForceY = airResistanceForceY + springForceY;
            netForceZ = gravityForce + airResistanceForceZ + springForceZ;

            accelerationX = netForceX / mass;
            accelerationY = netForceY / mass;
            accelerationZ = netForceZ / mass;

            velocityX = velocityX + accelerationX * deltaT;
            velocityY = velocityY + accelerationY * deltaT;
            velocityZ = velocityZ + accelerationZ * deltaT;

            positionX = positionX + velocityX * deltaT;
            positionY = positionY + velocityY * deltaT;
            positionZ = positionZ + velocityZ * deltaT;

            time += deltaT;

            if (velocity > 1)
            {
                finalTime = time;
            }
        }
        return finalTime;
    }
    public static void Level1()
    {
        const double deltaT = 0.001;
        const double angle = 45;
        const double mass = 3;

        double velocity = 4;
        double velocityX = velocity * Math.Cos(angle * Math.PI / 180);
        double velocityY = 0;
        double velocityZ = velocity * Math.Sin(angle * Math.PI / 180);

        double positionX = 0;
        double positionY = 0;
        double positionZ = 0;

        double time = 0;
        runLoop(positionX, positionY, positionZ, velocityX, velocityY, velocityZ, mass, 0, 0, 0, deltaT, 5);

    }

    public static void Level2()
    {
        const double deltaT = 0.001;
        const double angle = 45;
        const double mass = 3;
        const double gravityForce = -9.8 * mass;
        const double dragCoefficient = 0.6;
        double netForceX = gravityForce;
        double netForceZ = 0;

        double acceleration = 0;
        double accelerationX = netForceX / mass;
        double accelerationZ = netForceZ / mass;

        double velocity = 4;
        double velocityX = velocity * Math.Cos(angle * Math.PI / 180);
        double velocityY = 0;
        double velocityZ = velocity * Math.Sin(angle * Math.PI / 180);

        double airResistanceForceX = dragCoefficient * velocityX * velocityX;
        double airResistanceForceZ = dragCoefficient * velocityZ * velocityZ;

        double positionX = 0;
        double positionY = 0;
        double positionZ = 0;

        double time = 0;
        runLoop(positionX, positionY, positionZ, velocityX, velocityY, velocityZ, mass, dragCoefficient, 0, 0, 0.001, 5);

    }

    public static void Level3()
    {
        const double deltaT = 0.01;
        const double mass = 3;
        const double dragCoefficient = 0.6;
        const double springCoefficient = 9;
        const double unstretchedLength = 2;

        double netForceX = 0;
        double netForceY = 0;
        double netForceZ = 0;

        double accelerationX = netForceX / mass;
        double accelerationY = netForceY / mass;
        double accelerationZ = netForceZ / mass;

        double velocityX = 5;
        double velocityY = -1;
        double velocityZ = -3;

        double positionX = -1;
        double positionY = 1;
        double positionZ = -1;

        double finalTime = runLoop(positionX, positionY, positionZ, velocityX, velocityY, velocityZ, mass, dragCoefficient, springCoefficient, unstretchedLength, deltaT, 20);   
        Console.WriteLine($"The final time the ball was moving at velocity > 1 was {Math.Round(finalTime, 5)} seconds.");
    }

    public static void Main(string[] args)
    {
        //Level1();
        //Level2();
        Level3();
    }
}
