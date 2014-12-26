﻿using System.Collections.Generic;
using SharpOrrery;
using UnityEngine;

namespace SharpOrrery
{
    public static class Gravity
    {
        public static void calculateGForces(List<CelestialBody> bodies)
        {
            var workVect = new Vector3d();

            for (int i = 0; i < bodies.Count; i++)
            {
                if (i == 0)
                    bodies[i].force.x = bodies[i].force.y = bodies[i].force.z = 0;

                for (int j = i + 1; j < bodies.Count; j++)
                {
                    if (i == 0)
                        bodies[j].force.x = bodies[j].force.y = bodies[j].force.z = 0;
                    if ((bodies[i].mass == 1 && bodies[j].mass == 1) || (bodies[i].calculateFromElements && bodies[j].calculateFromElements))
                        continue;

                    workVect = getGForceBetween(bodies[i].mass, bodies[j].mass, bodies[i].position, bodies[j].position);
                    //add forces (for the first body, it is the reciprocal of the calculated force)
                    bodies[i].force -= (workVect);
                    bodies[j].force += (workVect);
                }
            }
        }

        /**
			Get the gravitational force in Newtons between two bodies (their distance in m, mass in kg)
			*/

        private static Vector3d getGForceBetween(double mass1, double mass2, Vector3d pos1, Vector3d pos2)
        {
            var workVect = new Vector3d();
            workVect = (pos1) - (pos2); //vector is between positions of body A and body B
            var dstSquared = (double) workVect.sqrMagnitude;
            double massPrd = mass1*mass2;
            double Fg = SO.G*(massPrd/dstSquared); //in newtons (1 N = 1 kg*m / s^2)
            workVect.Normalize();
            workVect *= Fg; //vector is now force of attraction in newtons/**/
            return workVect;
        }
    }
}