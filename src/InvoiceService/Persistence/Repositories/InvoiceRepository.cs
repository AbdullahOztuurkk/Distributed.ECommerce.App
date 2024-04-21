﻿using InvoiceWorkerService.Domain.Concrete;
using InvoiceWorkerService.Persistence.Context;
using InvoiceWorkerService.Persistence.Repositories.Contracts;
using System.Linq.Expressions;

namespace InvoiceWorkerService.Persistence.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly IDbContext dbContext;
    public InvoiceRepository(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<string> CreateAsync(Invoice entity)
    {
        return await dbContext.Invoices.CreateAsync(entity);
    }

    public async Task<IEnumerable<Invoice>> FindAsync(Expression<Func<Invoice, bool>> predicate)
    {
        return await dbContext.Invoices.FindAsync(predicate);
    }

    public async Task<Invoice> FindOneAsync(Expression<Func<Invoice, bool>> predicate)
    {
        return await dbContext.Invoices.FindOneAsync(predicate);
    }

    public async Task<Invoice> GetByIdAsync(string id)
    {
        return await dbContext.Invoices.GetByIdAsync(id);
    }

    public async Task<bool> RemoveAsync(string id)
    {
        return await dbContext.Invoices.RemoveAsync(id);
    }

    public async Task<bool> UpdateAsync(string id, Invoice entity)
    {
        return await dbContext.Invoices.UpdateAsync(id, entity);
    }
}