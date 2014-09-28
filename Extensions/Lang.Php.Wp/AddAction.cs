using System;
using System.Collections.Generic;
using System.Text;

namespace Lang.Php.Wp
{
    public class AddAction
    {
        #region 0 arguments

        /// <summary>
        /// triggered before any other hook when a user access the admin area. This hook doesn't provide any parameters, so it can only be used to callback a specified function. 
        /// </summary>
        /// <param name="function_to_add"></param>
        /// <param name="priority"></param>
        [HookAttribute(Hooks.AdminInit)]
        public static void AdminInit(Action function_to_add, int priority = 10)
        {
            throw new MockMethodException();
        }


        /// <summary>
        /// Runs after the basic admin panel menu structure is in place. 
        /// </summary>
        /// <param name="function_to_add"></param>
        /// <param name="priority"></param>
        [HookAttribute(Hooks.AdminMenu)]
        public static void AdminMenu(Action function_to_add, int priority = 10)
        {
            throw new MockMethodException();
        }

    
        [HookAttribute(Hooks.AdminHead)]
        public static void AdminHead(Action function_to_add, int priority = 10)
        {
            throw new MockMethodException();
        }


        [HookAttribute(Hooks.WpHead)]
        public static void WpHead(Action function_to_add, int priority = 10)
        {
            throw new MockMethodException();
        }


        /// <summary>
        /// This hook is fired once WP, all plugins, and the theme are fully loaded and instantiated. 
        /// </summary>
        /// <param name="functionToAdd"></param>
        /// <param name="priority"></param>
        [HookAttribute(Hooks.WpLoaded)]
        public static void WpLoaded(Action functionToAdd, int priority = 10)
        {
            throw new MockMethodException();
        }

        [HookAttribute(Hooks.AdminEnqueueScripts)]
        public static void AdminEnqueueScripts(Action functionToAdd, int priority = 10)
        {
            throw new MockMethodException();
        }



        
        #endregion
        #region 1 arg
        /// <summary>
        /// Runs whenever a post or page is created or updated, which could be from an import, post/page edit form, xmlrpc, or post by email. 
        /// Action function arguments: post ID and post object.
        /// </summary>
        [HookAttribute(Hooks.SavePost)]
        public static void SavePost(Action<int> function_to_add, int priority = 10)
        {
            throw new MockMethodException();
        }


        [HookAttribute(Hooks.SavePage)]
        public static void SavePage(Action<int> function_to_add, int priority = 10)
        {
            throw new MockMethodException();
        }
        
        #endregion
    }
}
