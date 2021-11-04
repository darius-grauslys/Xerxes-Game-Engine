namespace Xerxes_Engine.Export_OpenTK
{
    public static class Log_Messages__OpenTK
    {
#region CONST INTERNAL INFO MESSAGES
        internal const string
        INFO__GAME__RUN_INVOKED                                     = "The game has begun running.",
        INFO__EVENT_HANDLER__RECURRING_NAME_2                       = "An event handle by the name of:\"{0}\" already exists. Using:\"{1}\" instead.",
        INFO__COMPONENT__ENABLED_ON_PARENT_BIND                     = "Component was enabled when bounded to a non-null parent.",

#endregion
#region CONST INTERNAL VERBOSE MESSAGES
        VERBOSE__RENDER_SERVICE__LOAD_SHADERS                       = "LOADING SHADERS.",
        VERBOSE__RENDER_SERVICE__LOAD_SHADER_1                      = "Loading Shader   - {0}.",

        VERBOSE__GAME__CONTENT_LOADING                              = "LOADING CONTENT.",
        VERBOSE__GAME__SPRITE_LOAD_1                                = "Loading Sprite   - {0}.",
        VERBOSE__GAME__CONTENT_LOADED                               = "FINISHED LOADING CONTENT.",

        VERBOSE__SCENE_MANAGER__LOUSY_LOOKUP_1                      = "Lousy scene lookup:\"{0}\". Consider using Distinct_Handle over string?",

        VERBOSE__SPRITE_SHEET__USING_COUNT_CONSTRAINT_2             = "Utilizing count constraint:{0}. Available sprite count:{1}.",

#endregion
#region CONST INTERNAL WARNING MESSAGES

        WARNING__RECOVERY__ASSET_DIRECTORY_NOT_FOUND                = "Asset Directory not found, attempting to use default directory.",

        WARNING__EVENT_SCHEDULER__CHECK__EVENT_NOT_FOUND_1          = "Event handle:\"{0}\" was not found when seeing if it was active.",
        WARNING__EVENT_SCHEDULER__REMOVE__EVENT_NOT_FOUND_1         = "Event handle:\"{0}\" was not found when trying to remove it.",
        WARNING__EVENT_SCHEDULER__INVOKE__EVENT_NOT_FOUND_1         = "Event handle:\"{0}\" was not found when tyring to invoke it.",

        WARNING__SCENE_MANAGER__LOUSY_LOOKUP_FAILED_1               = "Lousy lookup failed:\"{0}\". Consider using Distinct_Handle over string?",
        WARNING__SCENE_MANAGER__ADDING_SCENE_UNDER_NULL_ALIAS_1     = "Adding scene:\"{0}\" under a null alias! Is this intentional? Use String.Empty instead.",

        WARNING__SPRITE_LIBRARY__RECOVERING_TO_DEFAULT              = "Sprite Library resorted to returning the sprite at index 0.",

        WARNING__SPRITE_SHEET__SUB_DIMENSION_DOES_NOT_DIVIDE_3      = "The given sub-dimension:{0} for parameter {1} does not nicely divide {2}.",
        WARNING__VERTEX_OBJECT_LIBRARY__COUNT_CONSTRAINT_INVALID_2  = "The given sprite-count constraint of:{0} exceeds that which is available:{1}.",

        WARNING__VERTEX_OBJECT__ATYPICAL_INDEXING_2                 = "Modifying vertex object with an atypical index. This might cause unexpected behavior."
                                                                    + "Was given index for modifcation:{0}, but typical generator in index set:{1}.",
        WARNING__COMPONENT__PARENT_IS_NULL                          = "Component was disabled when attached to a null parent.",
        //not used atm:
        WARNING__GAME_OBJECT_COMPONENT__UTILIZED_WHILE_DISABLED_1C  = "Component was not intended to be used while disabled. May cause problems. Action:\"{0}\".",

        WARNING__ANIMATION_RENDER_COMPONENT__BAD_NEGATIVE_SPEED_1   = "An animation node speed of non-default negative value:\"{0}\" was given. Using default.",

#endregion
#region CONST INTERNAL ERROR MESSAGES
        ERROR__GAME__DIRECTORY_NOT_FOUND_1                          = "The Directory:\"{0}\" was not found!",
        ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1                 = "The Recovery Directory:\"{0}\" was not found!",

        ERROR__ASSET_PIPE__FILE_NOT_FOUND_1                         = "The file:\"{0}\" was not found!",

        ERROR__SCENE_MANAGER__SWITCHED_TO_NULL_SCENE_1              = "Attemped to switch to null scene under lousy alias:\"{0}\". Not switching scenes!",
        ERROR__SCENE_MANAGER__CANNOT_ADD_NULL_SCENE_1               = "Attempted to add a null scene under alias:\"{0}\". Returning error scene handle!",

        ERROR__SPRITE_LIBRARY__SPRITE_NOT_FOUND_1                   = "The Sprite:\"{0}\" was not found in the library!",
        ERROR__SPRITE_LIBRARY__SPRITE_ID_NOT_FOUND_1                = "The Sprite ID:\"{0}\" is out of bounds!",

        //not used atm:
        ERROR__GAME_OBJECT__FAILED_TO_ASSOCIATE_COMPONENT_2C        = "Failed to associate component:{0}! Contextual Message:{1}!",
        ERROR__GAME_OBJECT__ASSOCIATED_ANCESTOR_IS_INVALID_1        = "Tried to associate with:{0}, but it is not a Scene Layer! Games Objects can "
                                                                    + "only associate with Scene Layers!",
        
        ERROR__GAME_COMPONENT__FAILED_TO_ASSOCIATE_1                = "Failed to associate to engine object:{0}! Components can only associate to Game Objects!",
        //not used atm:
        ERROR__GAME_OBJECT_COMPONENT__NOT_ASSOCIATED_TO_ROOT_1C     = "This component was utilized while not rooted, and depends on a rooted enviroment!"
                                                                    + "Contextual Message:{0}!",
        //not used atm:
        ERROR__GAME_OBJECT_COMPONENT__UTILIZED_WHILE_DISABLED_1C    = "This component was utilized while disabled, and only functions while enabled!"
                                                                    + "Contextual Message:{0}!",

        ERROR__STATE_MACHINE__PANIC_ON_STATE_ENTRY_1                = "State Machine Panicked! Current state:{0}!",
        ERROR__STATE_MACHINE__FAILED_TO_DEFINE_FLOW_1C              = "Failed to define State Flow! Contextual Message:{0}!",
        ERROR__STATE_MACHINE__FAILED_TO_REQUEST_STATE_1C            = "Failed to request State Transition! Contextual Message:{0}!",

        ERROR__TYPED_STATE_MACHINE__REPETATIVE_KEY_1                = "Another state of an equivalent type has already been registered! Tried to register:{0}!",
        ERROR__TYPED_STATE_MACHINE__TYPE_NOT_PRESENT_1              = "The given type:{0} is not present!",

        ERROR__VERTEX_OBJECT__INVALID_SUB_LENGTH_2                  = "The provided sub-length:{0} has an invalid value:{1}!",
        ERROR__VERTEX_OBJECT__INVALID_MODIFICATION_INDEX_2          = "Modification of the Vertex Object was out of bounds! The given modification index:{0}"
                                                                    + "is less than 0 or greater than the current size:{1}!",
        ERROR__VERTEX_OBJECT__MODIFICATION_OUT_OF_BOUNDS_3          = "Modification of this Vertex Object is out of bounds! The given modication index:{0} "
                                                                    + "plus the length of the modification:{1} exceeds the current size:{2}!",
        ERROR__VERTEX_OBJECT__MODIFICATION_METHOD_IS_INVALID_1      = "The given modification method for vertex modification - {0} is invalid!",

        ERROR__ANIMATION__NODE_DEFINITION__OUT_OF_BOUNDS_2          = "Attempted to define node:\"{0}\" when total node count is:\"{0}\"!";
#endregion
    }
}
