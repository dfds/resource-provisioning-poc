﻿using System;

namespace ResourceProvisioning.Abstractions.Events.Integration
{
	public interface IIntegrationEvent : IEvent
	{
		Guid Id { get; }

		DateTime CreationDate { get; }

		int Version { get; }

	}
}
