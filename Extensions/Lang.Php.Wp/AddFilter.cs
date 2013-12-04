using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Wp
{
    public class AddFilter
    {
        
        /// <summary>
        /// Hook a function to a specific filter action
        /// <see cref="http://codex.wordpress.org/Function_Reference/add_filter">Codex</see>
        /// </summary>
        /// <param name="tag">The name of the existing Filter to Hook the function_to_add argument to.</param>
        /// <param name="function_to_add">(callback) (required) The name of the function to be called when the custom Filter is applied. </param>
        /// <param name="priority">(integer) (optional) Used to specify the order in which the functions associated with a particular action are executed. Lower numbers correspond with earlier execution, and functions with the same priority are executed in the order in which they were added to the filter. </param>
        /// <param name="accepted_args">(integer) (optional) The number of arguments the function(s) accept(s). In WordPress 1.5.1 and newer hooked functions can take extra arguments that are set when the matching apply_filters() call is run. </param>
        public static bool TheContent( Func<string, string> function_to_add, int priority = 10, int accepted_args = 1)
        {
            throw new MockMethodException();
        }

        /// <summary>
        /// Hook a function to a specific filter action
        /// <see cref="http://codex.wordpress.org/Function_Reference/add_filter">Codex</see>
        /// </summary>
        /// <param name="tag">The name of the existing Filter to Hook the function_to_add argument to.</param>
        /// <param name="function_to_add">(callback) (required) The name of the function to be called when the custom Filter is applied. </param>
        /// <param name="priority">(integer) (optional) Used to specify the order in which the functions associated with a particular action are executed. Lower numbers correspond with earlier execution, and functions with the same priority are executed in the order in which they were added to the filter. </param>
        /// <param name="accepted_args">(integer) (optional) The number of arguments the function(s) accept(s). In WordPress 1.5.1 and newer hooked functions can take extra arguments that are set when the matching apply_filters() call is run. </param>
        public static bool TheTitle(Func<string, string> function_to_add, int priority = 10, int accepted_args = 1)
        {
            throw new MockMethodException();
        }
        
    }
}
