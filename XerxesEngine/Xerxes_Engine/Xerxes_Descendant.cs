namespace Xerxes_Engine
{
    public class Xerxes_Descendant<T,Y> : Xerxes_Object where T : Xerxes_Object, new() where Y : Xerxes_Descendant<T,Y>, new() 
    {
        protected T Xerxes_Descendant__Parent__Protected { get; private set; }

        public Xerxes_Descendant() 
        {
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Associate_Game>
                (
                    Private_Handle__Associate_To_Game__Xerxes_Descendant
                );
        }

        private void Private_Handle__Associate_To_Game__Xerxes_Descendant
        (
            Streamline_Argument_Associate_Game e
        )
        {
            Xerxes_Engine_Object__Root__Internal = e.Streamline_Argument_Associate_Game__GAME;
        }

        internal override void Internal_Handle__Associated_As_Descendant__Xerxes_Engine_Object
        (
            Xerxes_Object ancestorAssociation
        )
        {
            Xerxes_Descendant__Parent__Protected = 
                Internal_Get__Parent_As__Xerxes_Engine_Object<T>();
        }

        internal override bool Internal_Handle__Associate_As_Descendant__Xerxes_Engine_Object
        (
            Xerxes_Object ancestorAssociation
        )
        {
            return ancestorAssociation is T;
        }

        public virtual bool Associate__Xerxes_Descendant<H>
        (
            H descendant
        ) where H : Xerxes_Descendant<Y,H>, new()
        {
            return Xerxes_Object
                .Internal_Associate__Objects
                (
                    descendant,
                    this
                );
        }
    }
}
