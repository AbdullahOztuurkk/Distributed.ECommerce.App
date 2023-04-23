﻿using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using Clicco.Infrastructure.Context;
using Clicco.Infrastructure.Repositories;

namespace Clicco.Persistence.Repositories
{
    public class TransactionDetailProductRepository : GenericRepository<TransactionDetailProduct,CliccoContext>, ITransactionDetailProductRepository
    {
    }
}
