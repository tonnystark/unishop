using AutoMapper;
using UniShop.Model.Models;
using UniShop.Web.Models;

namespace UniShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
                cfg.CreateMap<PostCategory,
                    PostCategoryViewModel>();
            });
        }
    }
}