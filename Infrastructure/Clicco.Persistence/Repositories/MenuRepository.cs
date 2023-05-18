using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Model;
using Clicco.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Clicco.Infrastructure.Repositories
{
    public class MenuRepository : GenericRepository<Menu, CliccoContext>, IMenuRepository
    {
        public string GetExactSlugUrlByCategoryId(int categoryId)
        {
            List<string> uris = new();
            using var context = new CliccoContext();

            var category = context.Categories.Include(x => x.Parent).FirstOrDefault(x => x.Id == categoryId);
            if (category != null)
            {
                uris.Add(category.SlugUrl);

                var parent = category.Parent;
                while (parent != null)
                {
                    uris.Add(parent.SlugUrl);
                    parent = parent.Parent;
                }
            }
            //Reverse exist list for correct url
            uris.Reverse();

            return uris.ConcatUrls();
        }
    }
}
