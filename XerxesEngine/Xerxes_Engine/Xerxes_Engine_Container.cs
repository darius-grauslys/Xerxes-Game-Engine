using System;

namespace Xerxes_Engine
{
    public class Xerxes_Engine_Container : Xerxes_Engine_Object
    {
        internal event Action<Event_Argument_Resize_2D> Xerxes_Engine_Container__RESIZE_SUBSCRIPTION__Internal;

        internal Xerxes_Engine_Container
        (
            Xerxes_Engine_Object_Association_Type hierarchyType
        )
        : base
        (
            hierarchyType
        )
        {

        }

        internal virtual void Internal_Resize__2D__Xerxes_Engine_Container
        (
            Event_Argument_Resize_2D e
        )
        {
            if(Xerxes_Engine_Object__Is_Disabled__Internal)
                return;

            Handle__Resize_2D__Xerxes_Engine_Container(e);

            Xerxes_Engine_Container__RESIZE_SUBSCRIPTION__Internal?.Invoke(e);
        }
        protected virtual void Handle__Resize_2D__Xerxes_Engine_Container(Event_Argument_Resize_2D e) { }




        internal static bool Internal_Associate__Containers
        (
            Xerxes_Engine_Container thisContainer,
            Xerxes_Engine_Container toThisContainer
        )
        {
            bool success = Internal_Associate__Objects
            (
                thisContainer,
                toThisContainer,
                Private_Associate__Containers
            );

            return success;
        }

        private static void Private_Associate__Containers
        (
            Xerxes_Engine_Container thisContainer,
            Xerxes_Engine_Container toThisContainer
        )
        {
            toThisContainer.Xerxes_Engine_Container__RESIZE_SUBSCRIPTION__Internal
                += thisContainer.Internal_Resize__2D__Xerxes_Engine_Container;
        }
    }
}
