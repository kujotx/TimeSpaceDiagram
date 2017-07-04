namespace TimeSpaceDiagramControl.Controls
{
    using System.Windows.Media;
    using System.Diagnostics;

    /// <summary>
    /// ColorOffset tell the <see cref="Cycle"/> which color to apply at each time offset
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ColorOffset
    {
        public ColorOffset(Color color, decimal offset)
        {
            this.Color = color;
            this.Offset = offset;
        }

        /// <summary>
        /// Signal color to apply
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Time-difference from the master clock
        /// </summary>
        public decimal Offset { get; set; }

        protected bool Equals(ColorOffset other)
        {
            return Color.Equals(other.Color) && Offset == other.Offset;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ColorOffset)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Color.GetHashCode() * 397) ^ Offset.GetHashCode();
            }
        }

        public string DebuggerDisplay
        {
            get
            {
                return string.Format("Color: {0}, Offset: {1}", Color.ToString(), Offset.ToString());
            }
        }
    }
}