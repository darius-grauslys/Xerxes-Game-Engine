using System;

namespace Xerxes
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
    public class Xerxes_Export<SA__Configure, SA__Associate, SA__Dissassociate> :
    Xerxes_Export_Base
    where SA__Configure : SA__Configure_Root 
    where SA__Associate : SA__Associate_Root
    where SA__Dissassociate : SA__Dissassociate_Root
    {
        protected Xerxes_Export()
        {
        }

        internal override void Internal_Handle_Root__Exportline_Dictionary__Xerxes_Export_Base()
        {
            Declare__Receiving
                <SA__Configure>
                (
                    Handle__Configure_Root__Xerxes_Export
                );
            Declare__Receiving
                <SA__Associate>
                (
                    Handle__Associate_Root__Xerxes_Export
                );
            Declare__Receiving
                <SA__Dissassociate>
                (
                    Handle__Dissassociate_Root__Xerxes_Export
                );
        }

        protected virtual void Handle__Configure_Root__Xerxes_Export
        (
            SA__Configure_Root e
        )
        {

        }

        protected virtual void Handle__Associate_Root__Xerxes_Export
        (
            SA__Associate e
        )
        {
            
        }

        protected virtual void Handle__Dissassociate_Root__Xerxes_Export
        (
            SA__Dissassociate e
        )
        {
            
        }

        protected bool Declare__Receiving<T>
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
            object export,
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
