
namespace Xerxes
{
    /// <summary>
    /// Using a TGenology of Xerxes_Genology__Generic
    /// this group constructs a Xerxes_Mediation_Wrapper to
    /// encapsulate XTarget. It then offers protected
    /// functionality for establishing SA__Mediate`[SA, XTarget]
    /// through the Xerxes_Mediation_Wrapper.
    /// </summary>
    public abstract class Xerxes_Genology_Group__Mediator
    <
        TThis,
        TGenology,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream,
        TParent,
        XBase,
        XTarget
    >:
    Xerxes_Genology_Group__Child
    <
        TGenology,
        TParent
    >
    where TThis :
    Xerxes_Genology_Group__Mediator
    <
        TThis,
        TGenology,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream,
        TParent,
        XBase,
        XTarget
    >
    where TGenology :
    Xerxes_Genology__Generic
    <
        TGenology,
        XBase,
        TParent,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GStreamlines :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        GStreamlines,
        TGenology,
        XBase,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        GAscending_Stream,
        TGenology,
        GStreamlines
    >, new()
    where GDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        GDescending_Stream,
        TGenology,
        GStreamlines
    >, new()
    where TParent :
    Xerxes_Genology_Group__Mediated_Associations
    <
        TParent,
        TGenology,
        XBase,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where XBase :
    Xerxes_Object_Base
    <
        XBase,
        TGenology
    >, new()
    where XTarget :
    Xerxes_Object_Base, new()
    {
        internal Xerxes_Mediation_Wrapper<XTarget> Internal_Mediator__Mediation_Wrapper { get; set; }

        /// <summary>
        /// To be invoked only after Genology Linking.
        /// Creates a SA__Mediate reciever on the
        /// Mediation_Wrapper and a SA__Mediate extender
        /// on the genology object.
        /// </summary>
        protected void Protected_Mediate__Mediator<SA>()
        where SA :
        Streamline_Argument
        {
            Internal_Mediator__Mediation_Wrapper
                .Internal_Mediate__Mediation_Wrapper<SA>();

            Genology_Group__Enclosing_Genology
                .Declare__Streamlines
                    .With__Descendants
                        .Extending<SA__Mediate<SA, XBase>>();
        }

        /// <summary>
        /// Constructs the wrapper, then
        /// associates the wrapper instance underneath
        /// the genology's base object.
        /// </summary>
        protected internal override void Handle_Linking__Genology_Group()
        {
            Internal_Mediator__Mediation_Wrapper =
                new Xerxes_Mediation_Wrapper<XTarget>();

            Genology_Group__Enclosing_Genology
                .Declare__Associations
                    .Internal_Associate__Instance__Associations(Internal_Mediator__Mediation_Wrapper);
        }
    }
}
