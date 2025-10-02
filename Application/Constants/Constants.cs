using System;
using System.IO;

namespace MatchaLatteReviews.Application.Constants
{
    public static class Constants
    {
        public static readonly string ProjectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
    }
}
