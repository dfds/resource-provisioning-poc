﻿using System.Collections.Generic;
using ResourceProvisioning.Abstractions.Entities;

namespace ResourceProvisioning.Abstractions.Rules
{
	public interface IRuleEvaluator
	{
		IEnumerable<IRule> Rules { get; }

		void Evaluate<T>(T entity) where T : IEntity;
	}
}
