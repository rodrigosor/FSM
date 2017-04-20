namespace FSM.Domain
{
    public class Atom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AtomType Type { get; set; }
        public string Residue { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
