namespace Xerxes
{
    public class Streamline_Argument
    {
        /// <summary>
        /// A unique object reference that corresponds to the
        /// Xerxes_Object which sends this streamline_argument - without
        /// passing the Xerxes_Object as a reference.
        /// </summary>
        public object Streamline_Argument__Origin_Identifier { get; internal set; }
        public bool Streamline_Argument__Consumed { get; private set; }

        protected Streamline_Argument() {}

        public Streamline_Argument SA__Consume()
        {
            Streamline_Argument__Consumed = true;
            return this;
        }
    }
}
