
using System.Collections.Generic;

namespace Xerxes
{
    public abstract class Xerxes_Genology
    {
        internal Xerxes_Object_Base Genology__Enclosing_Object__Internal { get; set; }

        internal List<Xerxes_Genology> Genology__DESCENDANT_GENOLOGIES__Internal { get; }

        public Xerxes_Genology()
        {
            Genology__DESCENDANT_GENOLOGIES__Internal =
                new List<Xerxes_Genology>();
        }

        protected TGroup Protected_Link__Genology_Group__Genology<TThis, TGroup>()
        where TGroup : Xerxes_Genology_Group<TThis>, new()
        where TThis : Xerxes_Genology
        {
            if (!(this is TThis))
            {
                //TODO: const log
                Log.Write__Error__Log($"The Genology Group {typeof(TGroup)} is not compatable with the Genology, {GetType()}!", this);
                return null;
            }
    
            TGroup group = new TGroup();

            group.Genology_Group__Enclosing_Genology__Internal =
                this as TThis;

            group.Handle_Linking__Genology_Group();

            return group;
        }
    }



    public abstract class Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TDescending_Stream
    > :
    Xerxes_Genology
    where TThis :
    Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TDescending_Stream
    >
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TThis,
        TDescending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TThis,
        TStreamlines
    >, new()
    {
        public TStreamlines Declare__Streamlines { get; private set; }

        public Xerxes_Genology()
        {
            Declare__Streamlines =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TStreamlines
                >();
        }
    }

    public abstract class Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream
    > :
    Xerxes_Genology
    where TThis :
    Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream
    >
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TThis,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TThis,
        TStreamlines
    >, new()
    where TAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TAscending_Stream,
        TThis,
        TStreamlines
    >, new()
    {
        public TStreamlines Declare__Streamlines { get; private set; }

        public Xerxes_Genology()
        {
            Declare__Streamlines =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TStreamlines
                >();
        }
    }

    public abstract class Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations
    > :
    Xerxes_Genology
    where TThis :
    Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations
    >
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TThis,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TThis,
        TStreamlines
    >, new()
    where TAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TAscending_Stream,
        TThis,
        TStreamlines
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TAssociations,
        TThis
    >, new()
    {
        public TAssociations Declare__Associations { get; private set; }
        public TStreamlines Declare__Streamlines { get; private set; }

        public Xerxes_Genology()
        {
            Declare__Streamlines =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TStreamlines
                >();
            Declare__Associations =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TAssociations
                >();
        }
    }

    public abstract class Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations,
        TEndpoints
    > :
    Xerxes_Genology
    where TThis :
    Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations,
        TEndpoints
    >
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TThis,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TThis,
        TStreamlines
    >, new()
    where TAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TAscending_Stream,
        TThis,
        TStreamlines
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TAssociations,
        TThis
    >, new()
    where TEndpoints :
    Xerxes_Genology_Group__Endpoints
    <
        TEndpoints,
        TThis
    >, new()
    {
        public TEndpoints Declare__Endpoints { get; private set; }
        public TAssociations Declare__Associations { get; private set; }
        public TStreamlines Declare__Streamlines { get; private set; }

        public Xerxes_Genology()
        {
            Declare__Streamlines =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TStreamlines
                >();
            Declare__Associations =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TAssociations
                >();
            Declare__Endpoints =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TEndpoints
                >();
        }
    }
}
