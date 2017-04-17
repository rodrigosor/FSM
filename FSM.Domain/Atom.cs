namespace FSM.Domain
{
    public class Atom
    {
        public int Serial { get; set; }
        public string Name { get; set; }
        public char AlternateLocationIndicator { get; set; }
        public string ResidueName { get; set; }
        public char ChainID { get; set; }
        public int ResidueSequenceNumber { get; set; }
        public int CodeForInsertionOfResidues { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Occupancy { get; set; }
        public double TemperatureFactor { get; set; }
        public string Elemente { get; set; }
        public string Charge { get; set; }
    }
}
