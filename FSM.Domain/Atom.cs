namespace FSM.Domain
{
    public class Atom
    {
        public string RecordName { get; set; }
        public int Serial { get; set; }
        public string AtomName { get; set; }
        public char AlternateLocation { get; set; }
        public string ResidueName { get; set; }
        public char ChainID { get; set; }
        public int ResidueSequenceNumber { get; set; }
        public char ICode { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Occupancy { get; set; }
        public double TemperatureFactor{ get; set; }
        public string ElementSymbol { get; set; }
        public string Charge { get; set; }
    }
}
