﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ResourceProvisioning.Abstractions.Aggregates;
using ResourceProvisioning.Abstractions.Entities;
using ResourceProvisioning.Abstractions.Grid;
using ResourceProvisioning.Broker.Domain.Events;
using ResourceProvisioning.Broker.Domain.ValueObjects;

namespace ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate
{
	public sealed class EnvironmentRoot : Entity<Guid>, IAggregateRoot
	{
		private List<EnvironmentResourceReference> _resources;

		public DesiredState DesiredState { get; private set; }

		public EnvironmentStatus Status { get; private set; }
		private int _statusId = EnvironmentStatus.Created.Id;

		public DateTime CreateDate { get; private set; } = DateTime.Now;

		public IEnumerable<EnvironmentResourceReference> Resources => _resources.AsReadOnly();

		private EnvironmentRoot() : base()
		{
			_resources = new List<EnvironmentResourceReference>();
		}

		public EnvironmentRoot(DesiredState desiredState) : this()
		{
			DesiredState = desiredState;
			
			AddDomainEvent(new EnvironmentCreatedEvent(this));
		}

		public void AddResource(Guid resourceId, DateTime provisioned, string comment)
		{
			var existingResource = _resources.Where(o => o.ResourceId == resourceId).SingleOrDefault();

			if (existingResource == null)
			{        
				var resource = new EnvironmentResourceReference(resourceId, provisioned, comment);

				var validationResult = resource.Validate(new ValidationContext(resource));

				if (validationResult.Any())
				{
					var innerException = new AggregateException(validationResult.Select(o => new Exception(o.ErrorMessage)));

					throw new ProvisioningBrokerDomainException(nameof(resource), innerException);
				}

				_resources.Add(resource);

				Initialize();
			}
		}

		public void SetDesiredState(DesiredState desiredState) {
			DesiredState = desiredState;

			Initialize();
		}

		public void Initialize()
		{
			_statusId = GridActorStatus.Initializing.Id;

			AddDomainEvent(new EnvironmentInitializingEvent(Id));
		}

		public void Start()
		{
			if (_statusId == GridActorStatus.Initializing.Id)
			{
				_statusId = GridActorStatus.Started.Id;

				AddDomainEvent(new EnvironmentStartedEvent(Id));
			}
		}

		public void Stop()
		{
			_statusId = GridActorStatus.Stopped.Id;

			AddDomainEvent(new EnvironmentStoppedEvent(Id));
		}

		public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (DesiredState == null)
			{
				yield return new ValidationResult(nameof(DesiredState));
			}
		}
	}
}
