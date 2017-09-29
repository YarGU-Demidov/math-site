using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathSite.Common;
using MathSite.Entities;

namespace MathSite.Facades.Posts
{
    public enum PublishStateRequest
    {
        AllPublishStates,
        Published,
        Unpubished
    }

    public enum FrontPageStateRequest
    {
        AllVisibilityStates,
        Visible,
        Invisible
    }

    public interface IPostsFacade
    {
        Task<int> GetPostPagesCountAsync(string postTypeAlias, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState, bool cache);

        Task<Post> GetPostByUrlAndTypeAsync(Guid currentUserId, string url, string postTypeAlias, bool cache);

        Task<IEnumerable<Post>> GetPostsAsync(string postTypeAlias, int page, bool cache);
        Task<IEnumerable<Post>> GetPostsAsync(string postTypeAlias, int page, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState, bool cache);

        Task<Guid> CreatePostAsync(Post post, PostSeoSetting seoSettings, PostSetting settings = null);
    }
}