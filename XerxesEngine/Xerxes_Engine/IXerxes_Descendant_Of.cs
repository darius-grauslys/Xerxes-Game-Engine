namespace Xerxes_Engine
{
    /// <summary>
    /// This is used by Xerxes to enforce
    /// legal associations. This is why you
    /// cannot associate a Scene to a Game_Object
    /// because Scene is IXerxes_Descendant_Of[Game]
    /// and not IXerxes_Descendant_Of[Game_Object]
    /// </summary>
    public interface IXerxes_Descendant_Of<T> where T : Xerxes_Object<T>
    {}
}
