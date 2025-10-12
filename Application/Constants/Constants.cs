using System;
using System.IO;

namespace MatchaLatteReviews.Application.Constants
{
    public static class Constants
    {
        public static readonly string ProjectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\.."));
    }
}
