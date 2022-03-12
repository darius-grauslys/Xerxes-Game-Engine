
namespace Xerxes
{
    public class Xerxes_Object :
    Xerxes_Object<Xerxes_Genology__Standard>
    {
    }

    /// <summary>
    /// Represents a type in Xerxes_Engine that depends on the
    /// Update/Render control flow. All internalized logging messages
    /// are related to such objects - or systems.
    ///
    /// Xerxes_Object requires a self reference to T. Any other type
    /// will cause a critical error.
    ///
    /// Calls to Update and Render are internalized. Exposure to
    /// handling these calls are given via protected virtual definitions.
    /// </summary>
    public class Xerxes_Object<TGenology> : 
    Xerxes_Object_Base 
    where TGenology : Xerxes_Genology, new()
    {
        protected internal TGenology Genology { get; }

        public Xerxes_Object()
        {
            Genology =
                new TGenology();

            Xerxes_Object_Base__Genology__Internal =
                Genology;
        }
    }
}
