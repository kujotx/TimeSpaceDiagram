namespace TimeSpaceDiagramControl.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class SignalPlan 
    {
        private int Cycles { get; set; }

        private IList<Segment> _arterials;

        public SignalPlan(IEnumerable<Segment> arterials, int cycles) 
        {
            Cycles = cycles;
            _arterials = arterials.ToList();
        }

        public IEnumerable<Segment> Arterials 
        {
            get 
            {
                return _arterials;
            } 
        }
    }
}