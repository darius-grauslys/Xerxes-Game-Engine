
namespace Xerxes
{
    public class Xerxes_Switch<TGenology> :
    Xerxes_Switch<Xerxes_Switch<TGenology>, TGenology>
    where TGenology :
    Xerxes_Genology
    <
        TGenology, 
        Xerxes_Switch<TGenology>
    >, new()
    {
        public Xerxes_Switch()
        {

        }
    }

    public class Xerxes_Switch<TThis, TGenology> :
    Xerxes_Object<TThis, TGenology>
    where TThis :
    Xerxes_Switch
    <
        TThis,
        TGenology
    >, new()
    where TGenology :
    Xerxes_Genology
    <
        TGenology,
        TThis
    >, new()
    {
        internal Switch_Dictionary Switch__Switch_Table__Internal { get; set; }

        public Xerxes_Switch()
        {
            Switch__Switch_Table__Internal =
                new Switch_Dictionary();
        }
    }
}
