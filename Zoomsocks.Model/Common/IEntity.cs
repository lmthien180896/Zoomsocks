using System;
using System.Runtime.Serialization;

namespace SingLife.ULTracker.Model.Common
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    /// <typeparam name="TId">The type of the entity ID.</typeparam>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Gets the entity ID.
        /// </summary>
        /// <value>The entity ID.</value>
        TId Id { get; }

        /// <summary>
        /// Sets the entity ID.
        /// </summary>
        /// <param name="id">The entity ID value.</param>
        void SetId(TId id);
    }

    public abstract class Entity : IEntity<Guid>
    {
        protected Entity()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }

        [IgnoreDataMember]
        public Guid Id { get; private set; }

        /// <summary>
        /// Sets a value as the identity of this entity. This method should be used only in special cases
        /// where the entity class extending this base class needs to alter its identity.
        /// </summary>
        /// <param name="id">Value of the entity identity.</param>
        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}