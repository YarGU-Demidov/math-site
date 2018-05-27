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

    public interface IPostsFacade : IFacade
    {
        Task<int> GetPostPagesCountAsync(string postTypeAlias, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState);
        Task<int> GetPostPagesCountAsync(Guid? categoryId, string postTypeAlias, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState);

        Task<Post> GetPostByUrlAndTypeAsync(Guid currentUserId, string url, string postTypeAlias);
        Task<Post> GetPostAsync(Guid id);
        Task<PostType> GetPostTypeAsync(string alias);

        Task<IEnumerable<Post>> GetPostsAsync(Guid? categoryId, string postTypeAlias, int page, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState, IEnumerable<Category> excludedCategories);
        Task<IEnumerable<Post>> GetPostsAsync(Guid? categoryId, string postTypeAlias, int page, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState);
        Task<IEnumerable<Post>> GetPostsAsync(string postTypeAlias, int page, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState, IEnumerable<Category> excludedCategories);
        Task<IEnumerable<Post>> GetPostsAsync(string postTypeAlias, int page, int perPage, RemovedStateRequest state, PublishStateRequest publishState, FrontPageStateRequest frontPageState);

        Task<Guid> CreatePostAsync(Post post);
        Task<Guid> UpdatePostAsync(Post post);
        Task DeletePostAsync(Guid id);
    }
}