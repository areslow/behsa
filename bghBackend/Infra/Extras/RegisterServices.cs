using bghBackend.Application.AppUser;
using bghBackend.Application.Auth;
using bghBackend.Application.Categories;
using bghBackend.Application.Comments;
using bghBackend.Application.CouncilRequests;
using bghBackend.Application.Feeds;
using bghBackend.Application.Posts;
using bghBackend.Application.Products;
using bghBackend.Application.Replies;
using bghBackend.Domain;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Services;
using bghBackend.Infra.Services.Categories;
using bghBackend.Infra.Services.Comments;
using bghBackend.Infra.Services.CouncilRequests;
using bghBackend.Infra.Services.Feeds;
using bghBackend.Infra.Services.Posts;
using bghBackend.Infra.Services.Products;
using bghBackend.Infra.Services.Replies;
using bghBackend.Infra.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// IOC container ... dependency injection
namespace bghBackend.Infra.Extras
{
    public static class RegisterServices
    {
        // this section is included in program.cs
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            // services.AddSingleton<IService, Service>(); >>> define this for every service that is required
            //or AddTransient
            services.AddTransient<ICommentCommands, CommentCommands>();
            services.AddTransient<ICommentQueries, CommentQueries>();

            services.AddTransient<ICouncilRequestCommands, CouncilRequestCommands>();
            services.AddTransient<ICouncilRequestQueries, CouncilRequestQueries>();

            services.AddTransient<IPostCommands, PostCommands>();
            services.AddTransient<IPostQueries, PostQueries>();

            services.AddTransient<IReplyCommands, ReplyCommands>();
            services.AddTransient<IReplyQueries, ReplyQueries>();

            services.AddTransient<IUserCommands, UserCommands>();
            services.AddTransient<IUserQueries, UserQueries>();

            services.AddTransient<IProductQueries, ProductQueries>();
            services.AddTransient<IProductCommands, ProductCommands>();

            services.AddTransient<ICategoryQueries, CategoryQueries>();
            services.AddTransient<ICategoryCommands, CategoryCommands>();

            services.AddTransient<IAuthManager, AuthManager>();

            services.AddTransient<IFeeds, Feeds>();


            //// context ....  Register appuser and roles to services



            return services;
        }
    }
}
