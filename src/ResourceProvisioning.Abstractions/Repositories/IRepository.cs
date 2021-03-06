﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Data;

namespace ResourceProvisioning.Abstractions.Repositories
{
	public interface IRepository<TAggregate> : IRepository where TAggregate : IAggregateRoot
	{
		Task<IEnumerable<TAggregate>> GetAsync(Expression<Func<TAggregate, bool>> filter);

		TAggregate Add(TAggregate aggregate);

		TAggregate Update(TAggregate aggregate);

		void Delete(TAggregate aggregate);
	}

	public interface IRepository
	{
		IUnitOfWork UnitOfWork { get; }
	}
}
