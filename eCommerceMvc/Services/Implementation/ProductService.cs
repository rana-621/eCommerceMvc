﻿using eCommerceMvc.Data;
using eCommerceMvc.Models;
using eCommerceMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerceMvc.Services.Implementation;

public class ProductService : IProductInterface
{

    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task CreateProductAsync(Product product)
    {
        _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateProductAsync(Product product)
    {
        //_context.Products.Update(product);
        await _context.SaveChangesAsync();

    }


}
