
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
        TStreamlines
    > :
    Xerxes_Genology
    where TThis :
    Xerxes_Genology
    <
        TThis,
        TStreamlines
    >
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TThis
    >, new()
    {
        protected internal TStreamlines Genology__STREAMLINES__Protected { get; }

        public Xerxes_Genology()
        {
            Genology__STREAMLINES__Protected =
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
        TAssociations
    > :
    Xerxes_Genology
    where TThis :
    Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TAssociations
    >
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TThis
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TThis
    >, new()
    {
        protected internal TAssociations Genology__ASSOCIATIONS__Protected { get; }
        protected internal TStreamlines Genology__STREAMLINES__Protected { get; }

        public Xerxes_Genology()
        {
            Genology__STREAMLINES__Protected =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TStreamlines
                >();
            Genology__ASSOCIATIONS__Protected =
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
        TAssociations,
        TEndpoints
    > :
    Xerxes_Genology
    where TThis :
    Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TAssociations,
        TEndpoints
    >
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TThis
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TThis
    >, new()
    where TEndpoints :
    Xerxes_Genology_Group__Endpoints
    <
        TThis
    >, new()
    {
        protected internal TEndpoints Genology__ENDPOINTS__Protected { get;  }
        protected internal TAssociations Genology__ASSOCIATIONS__Protected { get; }
        protected internal TStreamlines Genology__STREAMLINES__Protected { get; }

        public Xerxes_Genology()
        {
            Genology__STREAMLINES__Protected =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TStreamlines
                >();
            Genology__ASSOCIATIONS__Protected =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TAssociations
                >();
            Genology__ENDPOINTS__Protected =
                Protected_Link__Genology_Group__Genology
                <
                    TThis,
                    TEndpoints
                >();
        }
    }
}
