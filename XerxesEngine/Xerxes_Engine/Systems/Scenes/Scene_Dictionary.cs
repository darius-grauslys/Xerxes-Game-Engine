namespace Xerxes_Engine.Systems.Scenes
{
    internal class Scene_Dictionary : Distinct_Dictionary<Scene_Handle, Scene>
    {
        internal Scene_Handle Internal_Add__Scene__Scene_Dictionary(string entry, Scene scene)
            => Protected_Declare__Element__Distinct_Dictionary(entry, scene);

        internal Scene Internal_Get__Scene__Scene_Dictionary(Scene_Handle handle)
            => Protected_Get__Element__Distinct_Dictionary(handle);

        internal Scene Internal_Get__Scene__Scene_Dictionary(string lousyHandle)
            => Protected_Get__Element__Distinct_Dictionary(lousyHandle);

        protected override Scene_Handle Handle_Get__New_Handle__Distinct_Dictionary
        (
            string internalStringHandle
        )
            => new Scene_Handle(internalStringHandle, this);
    }
}
