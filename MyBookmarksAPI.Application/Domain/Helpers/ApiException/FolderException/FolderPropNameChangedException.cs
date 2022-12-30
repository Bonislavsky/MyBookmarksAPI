using System;
namespace MyBookmarksAPI.Domain.Helpers.ApiException.FolderException
{
    public class FolderPropNameChangedException : Exception
    {
        public FolderPropNameChangedException(string message) : base(message)
        {

        }
    }
}
