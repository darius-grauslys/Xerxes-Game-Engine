using System;

namespace Xerxes_Engine
{
    /// <summary>
    /// Defines a receiving endpoint for streamlines
    /// above the root on the heirarchy. This develops
    /// a closing context for the game.
    ///
    /// Exports are receiver-only and are not typical
    /// Xerxes_Objects. They are used for interfacing to
    /// 3rd party libraries such as OpenTK.
    /// </summary>
    public class Xerxes_Export<A,D> :
    Xerxes_Export_Base
    where A : SA__Associate_Root
    where D : SA__Dissassociate_Root
    {
        protected Xerxes_Export()
        {
        }

        internal override void Internal_Handle_Root__Exportline_Dictionary__Xerxes_Export_Base()
        {
            Protected_Declare__Catch__Xerxes_Export
                <A>
                (
                    Handle__Associate_Game__Xerxes_Export
                );
            Protected_Declare__Catch__Xerxes_Export
                <D>
                (
                    Handle__Dissassociate_Game__Xerxes_Export
                );
        }

        protected virtual void Handle__Associate_Game__Xerxes_Export
        (
            A e
        )
        {
            
        }

        protected virtual void Handle__Dissassociate_Game__Xerxes_Export
        (
            D e
        )
        {
            
        }

        protected bool Protected_Declare__Catch__Xerxes_Export<T>
        (
            Action<T> listener
        ) where T : Streamline_Argument
        {
            Streamline<T> exportline =
                Private_Ensure__Exportline__Xerxes_Export<T>();

            if (exportline == null)
                return false;

            bool success =
                exportline
                .Internal_Subscribe__Streamline_Base
                (
                    listener
                );

            return success;
        }

        private Streamline<T> Private_Ensure__Exportline__Xerxes_Export<T>
        () where T : Streamline_Argument
        {
            if (Xerxes_Export__Game_Exportline_Dictionary__Internal_REFERENCE == null)
            {
                Private_Log_Error__Declaring_But_Not_Rooted
                (
                    this,
                    typeof(T)
                );
                return null;
            }

            Streamline<T> exportline;

            bool hasExportline =
                Xerxes_Export__Game_Exportline_Dictionary__Internal_REFERENCE
                .Internal_Check_If__Type_Exists__Streamline_Dictionary<T>();

            if (hasExportline)
            {
                exportline =
                    Xerxes_Export__Game_Exportline_Dictionary__Internal_REFERENCE
                    .Internal_Get__Streamline__Streamline_Dictionary<T>();

                return exportline;
            }

            exportline = new Streamline<T>();

            Xerxes_Export__Game_Exportline_Dictionary__Internal_REFERENCE
                .Internal_Declare__Streamline__Streamline_Dictionary
                (exportline);

            return exportline;
        }

#region Static Logging
        private static void Private_Log_Error__Declaring_But_Not_Rooted
        (
            Xerxes_Export<A,D> export,
            Type exportlineType
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_EXPORT__DECLARED_BUT_NOT_ROOTED_1,
                export,
                exportlineType
            );
        }
#endregion
    }
}
