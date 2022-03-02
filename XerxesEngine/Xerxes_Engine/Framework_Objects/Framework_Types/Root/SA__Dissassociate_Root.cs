namespace Xerxes
{
    public sealed class SA__Dissassociate_Root : 
        Streamline_Argument
    {
        public Root_Dissassociation_Event Dissassociate_Root__EVENT { get; }

        internal SA__Dissassociate_Root
        (Root_Dissassociation_Event e)
        {
            Dissassociate_Root__EVENT = e;
        }
    }
}
