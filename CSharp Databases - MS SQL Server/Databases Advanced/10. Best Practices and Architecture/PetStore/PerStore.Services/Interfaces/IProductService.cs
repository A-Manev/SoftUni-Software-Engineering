namespace PetStore.Services.Interfaces
{
    using System.Collections.Generic;

    using PetStore.ServiceModels.Products.InputModels;
    using PetStore.ServiceModels.Products.OutputModels;

    public interface IProductService
    {
        void AddProduct(AddProductInputServiceModel model);

        ICollection<ListAllProductsServiceModel> GetAll();

        ICollection<ListAllProductsByProductTypeServiceModel> ListAllProductType(string type);

        ICollection<ListAllProductsByNameServiceModel> SearchAllByName(string searchString, bool caseSensitive);

        bool RemoveById(string id);

        bool RemoveByName(string name);

        void EditProduct(string id, EditProductInputServiceModel model);
    }
}
