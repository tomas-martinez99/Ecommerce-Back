using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorage;

        public ProductService(IProductRepository repo, IUnitOfWork unitOfWork, IImageStorageService imageStorage, IMapper mapper, IProviderRepository providerRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
            _imageStorage = imageStorage;
        }
        //  Obtener todos los productos con imágenes
        public async Task<IReadOnlyList<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllWithImagesAsync();
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }
        public async Task<IEnumerable<ProductDto>> GetAllProviderAsync()
        {
            var entities = await _repo.GetAllWithProviderAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(entities);
        }

        //  Obtener producto por Id con imágenes
        public async Task<DetailProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdWithImagesAsync(id);
            return product is null ? null : _mapper.Map<DetailProductDto>(product);
        }

        //  Crear producto
        public async Task<DetailProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            // Validar proveedor
            var provider = await _unitOfWork.Providers.GetByIdAsync(dto.ProviderId);
            if (provider == null)
                throw new Exception($"No existe un proveedor con Id {dto.ProviderId}");

            entity.Provider = provider;

            await _unitOfWork.Products.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DetailProductDto>(entity);
        }

        // 🔹 Actualizar producto
        public async Task<bool> UpdateAsync(int id, CreateProductDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdWithImagesAsync(id);
            if (product == null) throw new KeyNotFoundException("Producto no encontrado");

            _mapper.Map(dto, product);
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        // 🔹 Eliminar producto
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Products.GetByIdWithImagesAsync(id);
            if (entity is null) return false;

            // Eliminar imágenes físicas asociadas
            foreach (var img in entity.Images)
                await _imageStorage.DeleteAsync(img.Url);

            await _unitOfWork.Products.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        // 🔹 Agregar imagen a un producto
        public async Task<ProductImageDto> AddImageAsync(int productId, IFormFile file, bool isMain = false)
        {
            var product = await _unitOfWork.Products.GetByIdWithImagesAsync(productId);
            if (product == null) throw new Exception("Producto no encontrado");

            var url = await _imageStorage.UploadAsync(file.OpenReadStream(), file.FileName, "products");

            var image = new ProductImage
            {
                Url = url,
                ProductId = productId,
                IsMain = isMain
            };

            await _unitOfWork.ProductImages.AddAsync(image);

            if (isMain)
                await _unitOfWork.ProductImages.SetMainImageAsync(productId, image.Id);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductImageDto>(image);
        }

        // 🔹 Eliminar imagen de un producto
        public async Task<bool> RemoveImageAsync(int productId, int imageId)
        {
            var image = await _unitOfWork.ProductImages.GetByIdAsync(imageId);
            if (image == null || image.ProductId != productId) return false;

            await _imageStorage.DeleteAsync(image.Url);
            await _unitOfWork.ProductImages.DeleteAsync(image);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        // 🔹 Marcar imagen como principal
        public async Task<bool> SetMainImageAsync(int productId, int imageId)
        {
            var product = await _unitOfWork.Products.GetByIdWithImagesAsync(productId);
            if (product == null) return false;

            await _unitOfWork.ProductImages.SetMainImageAsync(productId, imageId);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
