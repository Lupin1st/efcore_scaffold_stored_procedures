using Microsoft.EntityFrameworkCore;

namespace EFCore.ScaffoldProcedures.Models
{
    public partial class BloggingContext
    {
        private BloggingContextProgrammability _programmability;

        public BloggingContextProgrammability Programmability
        {
            get
            {
                if (_programmability == null)
                    _programmability = new BloggingContextProgrammability(this);

                return _programmability;
            }
        }

        // Procedure types:
        // for us there is no reason to add them to the context. This could habben somwhere else to keep the context clean.
        public virtual DbQuery<SpGetPostsForBlogResultEntry> SpGetPostsForBlogResult { get; set; }
    }
}