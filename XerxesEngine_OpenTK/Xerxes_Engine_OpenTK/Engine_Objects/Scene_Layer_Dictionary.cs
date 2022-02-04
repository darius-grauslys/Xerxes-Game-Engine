namespace Xerxes.Xerxes_OpenTK.Engine_Objects
{
    internal sealed class Scene_Layer_Dictionary : Distinct_Handle_Dictionary<Scene_Layer_Handle, Scene_Layer>
    {
        public Scene_Layer_Dictionary(string format = null) 
            : base(format)
        {
        }

        internal Scene_Layer_Handle Internal_Declare__Layer__Scene_Layer_Dictionary
        (
            string stringHandle, 
            Scene_Layer layer
        )
            => Protected_Declare__Element__Distinct_Handle_Dictionary(stringHandle, layer);

        protected override Scene_Layer_Handle Handle_Get__New_Handle__Distinct_Handle_Dictionary
        (
            string internalStringHandle
        )
        {
            return new Scene_Layer_Handle(internalStringHandle, this);
        }

        public Scene_Layer this[Scene_Layer_Handle handle]
            => Protected_Get__Element__Distinct_Handle_Dictionary(handle);
    }
}
