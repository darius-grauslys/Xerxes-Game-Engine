
using System.Collections.Generic;

namespace Xerxes
{
    public abstract class Xerxes_Genealogy
    {
        internal Xerxes_Object_Base Genealogy__Enclosing_Object__Internal { get; set; }

        internal List<Xerxes_Genealogy> Genealogy__DESCENDANT_GENOLOGIES__Internal { get; }

        public Xerxes_Genealogy()
        {
            Genealogy__DESCENDANT_GENOLOGIES__Internal =
                new List<Xerxes_Genealogy>();
        }

        protected abstract internal void Handle_Linking__Genealogy();

        protected TGroup Protected_Link__Genealogy_Group__Genealogy<TThis, TGroup>()
        where TGroup : Xerxes_Genealogy_Group<TThis>, new()
        where TThis : Xerxes_Genealogy
        {
            if (!(this is TThis))
            {
                //TODO: const log
                Log.Write__Error__Log($"The Genealogy Group {typeof(TGroup)} is not compatable with the Genealogy, {GetType()}!", this);
                return null;
            }
    
            TGroup group = new TGroup();

            group.Genealogy_Group__Enclosing_Genealogy__Internal =
                this as TThis;

            group.Genealogy_Group__Enclosing_Object__Internal =
                Genealogy__Enclosing_Object__Internal;

            group.Handle_Linking__Genealogy_Group();

            return group;
        }
    }



    public abstract class Xerxes_Genealogy
    <
        TThis,
        TStreamlines
    > :
    Xerxes_Genealogy
    where TThis :
    Xerxes_Genealogy
    <
        TThis,
        TStreamlines
    >
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis
    >, new()
    {
        protected internal TStreamlines Genealogy__Streamlines__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy()
        {
            Genealogy__Streamlines__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TStreamlines
                >();
        }
    }

    public abstract class Xerxes_Genealogy
    <
        TThis,
        TStreamlines,
        TAssociations
    > :
    Xerxes_Genealogy
    where TThis :
    Xerxes_Genealogy
    <
        TThis,
        TStreamlines,
        TAssociations
    >
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis
    >, new()
    where TAssociations :
    Xerxes_Genealogy_Group__Associations
    <
        TThis
    >, new()
    {
        protected internal TAssociations Genealogy__Associations__Protected { get; private set; }
        protected internal TStreamlines Genealogy__Streamlines__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy()
        {
            Genealogy__Streamlines__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TStreamlines
                >();
            Genealogy__Associations__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TAssociations
                >();
        }
    }

    public abstract class Xerxes_Genealogy
    <
        TThis,
        TStreamlines,
        TAssociations,
        TEndpoints
    > :
    Xerxes_Genealogy
    where TThis :
    Xerxes_Genealogy
    <
        TThis,
        TStreamlines,
        TAssociations,
        TEndpoints
    >
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis
    >, new()
    where TAssociations :
    Xerxes_Genealogy_Group__Associations
    <
        TThis
    >, new()
    where TEndpoints :
    Xerxes_Genealogy_Group__Endpoints
    <
        TThis
    >, new()
    {
        protected internal TEndpoints Genealogy__Endpoints__Protected { get; private set; }
        protected internal TAssociations Genealogy__Associations__Protected { get; private set; }
        protected internal TStreamlines Genealogy__Streamlines__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy()
        {
            Genealogy__Streamlines__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TStreamlines
                >();
            Genealogy__Associations__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TAssociations
                >();
            Genealogy__Endpoints__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TEndpoints
                >();
        }
    }
}
