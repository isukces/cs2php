using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Wp 
{
    public enum Permissions
    {
        [RenderValue("'edit_pages'")]
        EditPages,
        [RenderValue("'edit_posts'")]
        EditPosts
    }
}
