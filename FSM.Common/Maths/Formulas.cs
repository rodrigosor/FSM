using FSM.Domain;
using System;

namespace FSM.Common.Maths
{
    public static class Formulas
    {
        public static double EuclideanDistance(Atom atom, Atom hetatom)
        {
            return Math.Sqrt(
                    Math.Pow((atom.X - hetatom.X), 2) + Math.Pow((atom.Y - hetatom.Y), 2) + Math.Pow((atom.Z - hetatom.Z), 2)
                );
        }
    }
}
