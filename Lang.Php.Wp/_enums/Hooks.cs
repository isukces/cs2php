using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lang.Php;

namespace Lang.Php.Wp 
{
    public enum Hooks
    {
        /// <summary>
        /// triggered before any other hook when a user access the admin area. This hook doesn't provide any parameters, so it can only be used to callback a specified function. 
        /// </summary>
        [RenderValue("'admin_init'")]
        admin_init,
        /// <summary>
        /// Runs after the basic admin panel menu structure is in place. 
        /// </summary>
        [RenderValue("'admin_menu'")]
        admin_menu,
        /// <summary>
        /// Runs whenever a post or page is created or updated, which could be from an import, post/page edit form, xmlrpc, or post by email. 
        /// Action function arguments: post ID and post object.
        /// </summary>
        [RenderValue("'save_post'")]
        save_post,
        /// <summary>
        /// ??????
        /// </summary>
        [RenderValue("'save_page'")]
        save_page,
        /// <summary>
        /// ?????????
        /// </summary>
        [RenderValue("'admin_head'")]
        admin_head,
        /// <summary>
        /// Runs when the template calls the wp_head() function. 
        /// This hook is generally placed near the top of a page template between <head> and </head>. 
        /// This hook does not take any parameters. 
        /// </summary>
        [RenderValue("'wp_head'")]
        wp_head,
        /// <summary>
        /// This hook is fired once WP, all plugins, and the theme are fully loaded and instantiated. 
        /// </summary>
        [RenderValue("'wp_loaded'")]
        wp_loaded,
        /// <summary>
        /// is the first action hooked into the admin scripts actions. This hook doesn't provide any parameters, so it can only be used to callback a specified function. 
        /// </summary>
        admin_enqueue_scripts
    }
}
