using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Wp
{
    [Skip]
    public class Wp
    {

        [GlobalVariable("wpdb")]
        public static WpDb wpdb;
        #region Static Methods

        // Public Methods 

        [DirectCall("add_action")]
        public static bool add_action(Hooks hook, Action function_to_add, int priority = 10, int accepted_args = 1)
        {
            // add_action( $hook, $function_to_add, $priority, $accepted_args );
            // http://codex.wordpress.org/Function_Reference/add_action
            throw new MockMethodException();
        }

        [DirectCall("add_action")]
        public static bool add_action(Hooks hook, Func<string, string> function_to_add, int priority = 10, int accepted_args = 1)
        {
            // add_action( $hook, $function_to_add, $priority, $accepted_args );
            // http://codex.wordpress.org/Function_Reference/add_action
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
        [DirectCall("add_filter")]
        public static bool add_filter(WpTags tag, Func<string, string> function_to_add, int priority = 10, int accepted_args = 1)
        {
            throw new MockMethodException();
        }

        [DirectCall("add_menu_page")]
        public static void add_menu_page(string page_title, string menu_title, string capability, string menu_slug,
            Action function = null, string icon_url = null, int position = 0)
        {
            throw new MockMethodException();
        }

        /// <summary>
        /// The add_meta_box() function was introduced in Version 2.5. 
        /// It allows plugin developers to add meta boxes to the administrative interface. 
        /// This function should be called from the 'add_meta_boxes' action. 
        /// This action was introduced in Version 3.0; in prior versions, use 'admin_init' instead. 
        /// </summary>
        /// <param name="id">HTML 'id' attribute of the edit screen section </param>
        /// <param name="title">Title of the edit screen section, visible to user </param>
        /// <param name="callback">Function that prints out the HTML for the edit screen section. The function name as a string, or, within a class, an array to call one of the class's methods. The callback can accept up to two arguments, see Callback args.</param>
        /// <param name="post_type">The type of Write screen on which to show the edit screen section ('post', 'page', 'dashboard', 'link', 'attachment' or 'custom_post_type' where custom_post_type is the custom post type slug) </param>
        /// <param name="context">The part of the page where the edit screen section should be shown ('normal', 'advanced', or 'side'). (Note that 'side' doesn't exist before 2.7) </param>
        /// <param name="priority">The priority within the context where the boxes should show</param>
        /// <param name="callback_args">Arguments to pass into your callback function. The callback will receive the $post object and whatever parameters are passed through this variable</param>
        /// <returns></returns>
        [DirectCall("add_meta_box")]
        public static bool add_meta_box(string id, string title, Action<PostInfo> callback, PostTypes post_type, PartOfPage context = PartOfPage.advanced, Priority priority = Priority.Default, string callback_args = null)
        {
            // http://codex.wordpress.org/Function_Reference/add_meta_box
            throw new MockMethodException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post_id">The ID of the post to which a custom field should be added</param>
        /// <param name="meta_key">The key of the custom field which should be added</param>
        /// <param name="meta_value">The value of the custom field which should be added. If an array is given, it will be serialized into a string</param>
        /// <param name="unique">Whether or not you want the key to stay unique. When set to true, the custom field will not be added if the given key already exists among custom fields of the specified post.</param>
        /// <returns>Boolean true, except if the $unique argument was set to true and a custom field with the given key already exists, in which case false is returned.</returns>
        [DirectCall("add_post_meta")]
        public static bool AddPostMeta(int post_id, string meta_key, object meta_value, bool unique = false)
        {
            throw new NotImplementedException();
        }

        [DirectCall("current_user_can")]
        public static bool current_user_can(Permissions capability, int postId)
        {
            throw new NotImplementedException();
        }

        [DirectCall("esc_attr")]
        public static string esc_attr(object text)
        {
            throw new NotImplementedException();
        }

        [DirectCall("esc_html")]
        public static string esc_html(object text)
        {
            throw new NotImplementedException();
        }


        [DirectCall("wp_title")]
        public static string WpTitle(string sep = "&raquo;", bool display = true, string seplocation = "right")
        {
            throw new MockMethodException();
        }
        [DirectCall("is_home")]
        public static bool IsHome()
        {
            throw new MockMethodException();
        }
        [DirectCall("is_front_page")]
        public static bool IsFrontPage()
        {
            throw new MockMethodException();
        }


     


        [DirectCall("get_bloginfo")]
        public static string GetBloginfo(BlogInfoShows show = BlogInfoShows.Name, BlogInfoFilters filter = BlogInfoFilters.Raw)
        {
            throw new MockMethodException();
            // <?php $bloginfo = get_bloginfo( $show, $filter ); ?> 
        }

        /// <summary>
        /// This function returns the values of the custom fields with the specified key from the specified post. It is a wrapper for get_metadata('post'). To return all of the custom fields, see get_post_custom(). See also update_post_meta(), delete_post_meta() and add_post_meta(). 
        /// Desctiption has been taken from http://codex.wordpress.org/Function_Reference/get_post_meta
        /// </summary>
        /// <param name="post_id">(integer) (required) The ID of the post from which you want the data. Use get_the_ID() while in The Loop to get the post's ID, or use your sub-loop's post object ID property (eg $my_post_object->ID). You may also use the global $post object's ID property (eg $post->ID), but this may not always be what you intend. </param>
        /// <param name="key">A string containing the name of the meta value you want. </param>
        /// <param name="single">If set to true then the function will return a single result, as a string. If false, or not set, then the function returns an array of the custom fields. This may not be intuitive in the context of serialized arrays. If you fetch a serialized array with this method you want $single to be true to actually get an unserialized array back. If you pass in false, or leave it out, you will have an array of one, and the value at index 0 will be the serialized string. </param>
        [DirectCall("get_post_meta")]
        public static Dictionary<string, object> get_post_meta(int post_id, string key = null, bool single = false)
        {
            throw new MockMethodException();
        }

        [DirectCall("plugin_dir_url")]
        public static string PluginDirUrl(string fn)
        {
            throw new MockMethodException();
        }

        /// <summary>
        /// Retrieves the absolute URL to the plugins directory (without the trailing slash) or, 
        /// when using the $path argument, to a specific file under that directory. 
        /// You can either specify the $path argument as a hardcoded path relative to the plugins directory, 
        /// or conveniently pass __FILE__ as the second argument to make the $path relative to the parent 
        /// directory of the current PHP script file. 
        /// </summary>
        /// <param name="path">Path to the plugin file of which URL you want to retrieve, relative to the plugins directory or to $plugin if specified. </param>
        /// <param name="plugin"> Path under the plugins directory of which parent directory you want the $path to be relative to. </param>
        /// <returns></returns>
        [DirectCall("plugins_url")]
        public static string PluginsUrl(string path = "", string plugin = "")
        {
            throw new MockMethodException();
        }

        [DirectCall("update_option")]
        public static void update_option(string option, int newvalue)
        {
            throw new MockMethodException();
        }

        [DirectCall("update_option")]
        public static void update_option(string option, string newvalue)
        {
            throw new MockMethodException();
        }

        [DirectCall("update_option")]
        public static void update_option(string option, object newvalue)
        {
            throw new MockMethodException();
        }

        /// <summary>
        /// Use the function update_option() to update a named option/value pair to the options database table. The $option (option name) value is escaped with $wpdb->escape before the INSERT statement. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option">Name of the option to update. A list of valid default options to update can be found at the Option Reference. </param>
        /// <param name="newvalue">The NEW value for this option name. This value can be an integer, string, array, or object</param>
        [DirectCall("update_option")]
        public static void update_option<T>(string option, Dictionary<string, T> newvalue)
        {
            throw new MockMethodException();
        }

        /// <summary>
        /// The function update_post_meta() updates the value of an existing meta key (custom field) for the specified post.
        /// </summary>
        /// <param name="post_id">The ID of the post to which a custom field should be added</param>
        /// <param name="meta_key">The key of the custom field which should be added</param>
        /// <param name="meta_value">The value of the custom field which should be added. If an array is given, it will be serialized into a string</param>
        /// <param name="unique">Whether or not you want the key to stay unique. When set to true, the custom field will not be added if the given key already exists among custom fields of the specified post.</param>
        /// <returns>Returns meta_id if the meta doesn't exist, otherwise returns true on success and false on failure. NOTE: If the meta_value passed to this function is the same as the value that is already in the database, this function returns false. </returns>
        [DirectCall("update_post_meta")]
        public static object UpdatePostMeta(int post_id, string meta_key, object meta_value, bool unique = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle">Name used as a handle for the stylesheet. As a special case, if the string contains a '?' character, the preceding part of the string refers to the registered handle, and the succeeding part is appended to the URL as a query string. </param>
        /// <param name="src">URL to the stylesheet. Example: 'http://example.com/css/mystyle.css'. This parameter is only required when WordPress does not already know about this style. You should never hardcode URLs to local styles, use plugins_url (for Plugins) and get_template_directory_uri (for Themes) to get a proper URL. Remote assets can be specified with a protocol-agnostic URL, i.e. '//otherdomain.com/css/theirstyle.css'. </param>
        /// <param name="deps">Array of handles of any stylesheet that this stylesheet depends on; stylesheets that must be loaded before this stylesheet. false if there are no dependencies. </param>
        /// <param name="ver">String specifying the stylesheet version number, if it has one. This parameter is used to ensure that the correct version is sent to the client regardless of caching, and so should be included if a version number is available and makes sense for the stylesheet. </param>
        /// <param name="media"></param>
        [DirectCall("wp_enqueue_style")]
        public static void WpEnqueueStyle(string handle, string src = null, string[] deps = null, string ver = "", CssMediaTypes media = CssMediaTypes.All)
        {

        }

        /// <summary>
        /// A safe way to register a CSS style file for later use with wp_enqueue_style(). 
        /// </summary>
        /// <param name="handle">Name of the stylesheet (which should be unique as it is used to identify the script in the whole system)</param>
        /// <param name="src">URL to the stylesheet. Example: 'http://example.com/css/mystyle.css'. You should never hardcode URLs to local styles, use plugins_url()(for Plugins) and get_template_directory_uri() (for Themes) to get a proper URL. Remote assets can be specified with a protocol-agnostic URL, i.e. '//otherdomain.com/css/theirstyle.css'.</param>
        /// <param name="deps">Array of handles of any stylesheets that this stylesheet depends on. Dependent stylesheets will be loaded before this stylesheet. </param>
        /// <param name="ver">String specifying the stylesheet version number, if it has one. This parameter is used to ensure that the correct version is sent to the client regardless of caching, and so should be included if a version number is available and makes sense for the stylesheet. The version is appended to the stylesheet URL as a query string, such as ?ver=3.5.1. By default, or if false, the WordPress version string is used. If null nothing is appended to the URL. </param>
        /// <param name="media">String specifying the media for which this stylesheet has been defined. Examples: 'all', 'screen', 'handheld', 'print'. See this list for the full range of valid CSS-media-types. </param>
        [DirectCall("wp_register_style")]
        public static void WpRegisterStyle(string handle, string src, string[] deps, string ver, CssMediaTypes media = CssMediaTypes.All)
        {

        }

        /// <summary>
        /// A safe way to register a CSS style file for later use with wp_enqueue_style(). 
        /// </summary>
        /// <param name="handle">Name of the stylesheet (which should be unique as it is used to identify the script in the whole system)</param>
        /// <param name="src">URL to the stylesheet. Example: 'http://example.com/css/mystyle.css'. You should never hardcode URLs to local styles, use plugins_url()(for Plugins) and get_template_directory_uri() (for Themes) to get a proper URL. Remote assets can be specified with a protocol-agnostic URL, i.e. '//otherdomain.com/css/theirstyle.css'.</param>
        /// <param name="deps">Array of handles of any stylesheets that this stylesheet depends on. Dependent stylesheets will be loaded before this stylesheet. </param>
        /// <param name="ver">String specifying the stylesheet version number, if it has one. This parameter is used to ensure that the correct version is sent to the client regardless of caching, and so should be included if a version number is available and makes sense for the stylesheet. The version is appended to the stylesheet URL as a query string, such as ?ver=3.5.1. By default, or if false, the WordPress version string is used. If null nothing is appended to the URL. </param>
        /// <param name="media">String specifying the media for which this stylesheet has been defined. Examples: 'all', 'screen', 'handheld', 'print'. See this list for the full range of valid CSS-media-types. </param>
        [DirectCall("wp_register_style")]
        public static void WpRegisterStyle(string handle, string src, string[] deps, bool ver, CssMediaTypes media = CssMediaTypes.All)
        {

        }

        #endregion Static Methods


        /// <summary>
        /// Adds a hook for a shortcode tag. 
        /// </summary>
        /// <param name="tag">Shortcode tag to be searched in post content </param>
        /// <param name="func">Hook to run when shortcode is found</param>
        [DirectCall("add_shortcode")]
        public static string AddShortcode(string tag, Func<Dictionary<string, string>, string, string> func)
        {
            throw new MockMethodException();
        }

        #region Static Properties

        public static bool DoingAutosave;

        public static string HookSuffix;

        #endregion Static Properties

        /// <summary>
        /// A safe way of getting values for a named option from the options database table. 
        /// If the desired option does not exist, or no value is associated with it, FALSE will be returned.
        /// </summary>
        /// <param name="option">Name of the option to retrieve. A concise list of valid options is below, but a more complete one can be found at the Option Reference.</param>
        /// <param name="_default">The default value to return if no value is returned (ie. the option is not in the database)</param>
        /// <returns></returns>
        [DirectCall("get_option")]
        public static Falsable<T> get_option<T>(string option, T _default = default(T))
        {
            throw new MockMethodException();
            // return _default;
        }
    }
}
