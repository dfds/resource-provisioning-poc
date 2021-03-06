﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ResourceProvisioning.Abstractions.Events;
using ResourceProvisioning.Broker.Domain.Events;

namespace ResourceProvisioning.Broker.Application.Events
{
	public class ResourceReadyEventHandler : IDomainEventHandler<ResourceReadyEvent>
	{
		public async Task Handle(ResourceReadyEvent @event, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
