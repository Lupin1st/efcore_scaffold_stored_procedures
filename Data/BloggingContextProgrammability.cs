using System.Data.SqlClient;
using System.Threading.Tasks;
using EFCore.ScaffoldProcedures.EFCoreTypes;
using Microsoft.EntityFrameworkCore;

namespace EFCore.ScaffoldProcedures.Models
{
    public class BloggingContextProgrammability
    {
        private readonly BloggingContext _bloggingContext;

        public BloggingContextProgrammability(BloggingContext bloggingContext)
        {
            _bloggingContext = bloggingContext;
        }

        public async Task<SpGetPostsForBlogResultEntry[]> SpGetPostUrls(int take, OutputParameter<int> overallCount)
        {
            var parameterTake = new SqlParameter
            {
                ParameterName = "Take",
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input,
                Value = take
            };

            var outParameterOverallCount = new SqlParameter
            {
                ParameterName = "OverallCount",
                DbType = overallCount.GetDataTypeInternal(),
                Direction = System.Data.ParameterDirection.Output
            };

            var result = await _bloggingContext.SpGetPostsForBlogResult.FromSql("exec [SP_GET_POST_URLS] @Take, @OverallCount OUTPUT",
                parameterTake,
                outParameterOverallCount)
                .ToArrayAsync(); // ToArray should only be used for stored procedures to ensure that out parameters are set
                                 //, functions should return an IQueryable

            overallCount.SetValueInternal(outParameterOverallCount.Value);
            return result;
        }

        // Generated from EF6 edmx
        //public virtual ObjectResult<SpGetPostsForBlogResult> SpGetPostUrls(int take, ObjectParameter error)
        //{
        //    var windowsUserParameter = take != null ?
        //        new ObjectParameter("Take", take) :
        //        new ObjectParameter("Take", typeof(string));

        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetPostsForBlogResult>("SP_GET_POST_URLS", take, error);
        //}
    }
}
