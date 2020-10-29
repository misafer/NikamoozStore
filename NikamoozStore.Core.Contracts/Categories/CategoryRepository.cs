using NikamoozStore.Core.Domain.Categories;

using System.Collections.Generic;

namespace NikamoozStore.Core.Contracts.Categories
{
    public interface CategoryRepository
    {
        List<Category> GetAll();
    }
}
