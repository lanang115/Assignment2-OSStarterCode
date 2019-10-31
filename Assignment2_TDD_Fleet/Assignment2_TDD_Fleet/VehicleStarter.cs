﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet
{
    public class VehicleStarter
    {

        /** Main entry point to the program
         * @param args the command line arguments
         */
        public static void main(string[] args)
        {
            Vehicle v = new Vehicle("Ford", "T812", 2014);

            // Vehicle sample distance
            v.addFuel(new Random().NextDouble() * 10, 1.3);

            v.printDetails();
            Console.WriteLine("\n\n");
        }

    }
}