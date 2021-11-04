namespace Xerxes_Engine.Export_OpenTK
{
    internal class Sprite_Dictionary : Distinct_Handle_Dictionary<Sprite_Handle, Sprite>
    {
        private const string _SPRITE_DICTIONARY__DEFAULT_HANDLE = "sprite";

        public Sprite_Dictionary(string format = null) 
            : base(format)
        {
        }

        internal Sprite Internal_Get__Sprite__Sprite_Dictionary(Sprite_Handle handle)
            => Protected_Get__Element__Distinct_Handle_Dictionary(handle);

        internal Sprite_Handle Internal_Declare__Sprite__Sprite_Dictionary(Sprite sprite, string handle=null)
            => Protected_Declare__Element__Distinct_Handle_Dictionary(handle ?? _SPRITE_DICTIONARY__DEFAULT_HANDLE, sprite);

        protected override Sprite_Handle Handle_Get__New_Handle__Distinct_Handle_Dictionary(string internalStringHandle)
        {
            return new Sprite_Handle(internalStringHandle, this);
        }
    }
}
