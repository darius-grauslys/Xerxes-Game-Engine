using OpenTK;

namespace isometricgame.GameEngine.UI
{
    public class UI_Anchor_Builder
    {
        private UI_Anchor UI_Anchor_Builder__Constructed_Anchor { get; set; }

        private UI_Anchor_Position_Type UI_Anchor_Builder__Last_Entered__Position_Type { get; set; }
        private UI_Anchor_Offset_Type UI_Anchor_Builder__Last_Entered__Offset_Type { get; set; }
        private Vector3 UI_Anchor_Builder__Last_Entered__Offset_Vector { get; set; }
        
        private int UI_Anchor_Builder__Last_Entered__Major_Sort { get; set; }
        private int UI_Anchor_Builder__Last_Entered__Minor_Sort { get; set; }
        
        public UI_Anchor_Builder Set__Target_Anchor_Position__UI_Anchor_Builder(UI_Anchor_Position_Type targetAnchorPoint)
        {
            UI_Anchor_Builder__Constructed_Anchor.UI_Anchor__Target_Anchor_Point =
                UI_Anchor_Builder__Last_Entered__Position_Type = targetAnchorPoint;

            return this;
        }

        public UI_Anchor_Builder Set__Offset_Type__UI_Anchor_Builder(UI_Anchor_Offset_Type offsetType)
        {
            UI_Anchor_Builder__Constructed_Anchor.UI_Anchor__Offset_Type__UI_Anchor =
                UI_Anchor_Builder__Last_Entered__Offset_Type = offsetType;

            return this;
        }
        
        public UI_Anchor_Builder Set__Offset_Vector__UI_Anchor_Builder(Vector3 offsetVector)
        {
            UI_Anchor_Builder__Constructed_Anchor.UI_Anchor__Offset_Vector__UI_Anchor =
                UI_Anchor_Builder__Last_Entered__Offset_Vector = offsetVector;

            return this;
        }
        
        #region Set__Sort_Style
        
        public UI_Anchor_Builder Set__Sort_Style__UI_Anchor_Builder()
        {
            Private_Set__Sort_Style__UI_Anchor_Builder();

            return this;
        }
        
        public UI_Anchor_Builder Set__Sort_Style__UI_Anchor_Builder
        (
            UI_Horizontal_Anchor_Sort_Type majorOnly
        )
        {
            Private_Set__Sort_Style__UI_Anchor_Builder((int) majorOnly);

            return this;
        }
        
        public UI_Anchor_Builder Set__Sort_Style__UI_Anchor_Builder
        (
            UI_Vertical_Anchor_Sort_Type majorOnly
        )
        {
            Private_Set__Sort_Style__UI_Anchor_Builder((int) majorOnly);

            return this;
        }
        
        public UI_Anchor_Builder Set__Sort_Style__UI_Anchor_Builder
        (
            UI_Horizontal_Anchor_Sort_Type major,
            UI_Vertical_Anchor_Sort_Type minor
        )
        {
            Private_Set__Sort_Style__UI_Anchor_Builder((int) major, (int) minor);

            return this;
        }
        
        public UI_Anchor_Builder Set__Sort_Style__UI_Anchor_Builder
        (
            UI_Vertical_Anchor_Sort_Type major,
            UI_Horizontal_Anchor_Sort_Type minor
        )
        {
            Private_Set__Sort_Style__UI_Anchor_Builder((int) major, (int) minor);

            return this;
        }

        private void Private_Set__Sort_Style__UI_Anchor_Builder(int? major = null, int? minor = null)
        {
            UI_Anchor_Builder__Constructed_Anchor.UI_Anchor__Sort_Style =
                new UI_Anchor_Sort_Style
                (
                    UI_Anchor_Builder__Last_Entered__Major_Sort = (major ?? UI_Anchor_Builder__Last_Entered__Major_Sort),
                    UI_Anchor_Builder__Last_Entered__Minor_Sort = (minor ?? UI_Anchor_Builder__Last_Entered__Minor_Sort)
                );
        }
        
        #endregion
        
        public UI_Anchor Conclude__UI_Anchor_Builder()
        {
            UI_Anchor returningAnchor = UI_Anchor_Builder__Constructed_Anchor;

            UI_Anchor_Builder__Constructed_Anchor = new UI_Anchor()
            {
                UI_Anchor__Target_Anchor_Point = UI_Anchor_Builder__Last_Entered__Position_Type,
                UI_Anchor__Offset_Type__UI_Anchor = UI_Anchor_Builder__Last_Entered__Offset_Type,
                UI_Anchor__Offset_Vector__UI_Anchor = UI_Anchor_Builder__Last_Entered__Offset_Vector
            };

            Set__Sort_Style__UI_Anchor_Builder();
            
            return returningAnchor;
        }
    }
}