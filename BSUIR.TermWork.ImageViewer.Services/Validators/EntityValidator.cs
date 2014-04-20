using System;
using System.Linq.Expressions;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal abstract class EntityValidator<TEntity> : EntityBaseValidator,
                                                       IEntityValidator<TEntity>
        where TEntity : EntityBase
    {
        private readonly string _fullTypeName;

        protected EntityValidator()
        {
            this._fullTypeName = typeof(TEntity).FullName;
        }

        #region Implementation of IEntityValidator<TEntity>

        public abstract void Validate(TEntity entity);

        public void ValidateProperty<T>(Expression<Func<TEntity, T>> property, T propertyValue)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            PropertyValidationToken<T> validationToken = this.GetPropertyValidtaionToken(property);
            if (validationToken != null)
            {
                validationToken.VerificationMethod(propertyValue);
            }
            else
            {
                throw new EntityValidationException(
                    "The property " + property + " has not been registered.");
            }
        }

        protected virtual void ValidateKey(int key)
        {
            if (key < 0)
            {
                throw new EntityValidationException("The key value cannot be below zero!");
            }
        }

        protected void RegisterProperty<T>(Expression<Func<TEntity, T>> property, Action<T> action)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            string fullPropertyName = this.FormFullPropertyName(property);
            lock (this.OperationBuffer)
            {
                if (!this.OperationBuffer.ContainsKey(fullPropertyName))
                {
                    Func<TEntity, T> compiledProperty = property.Compile();
                    var propertyValidationToken = new PropertyValidationToken<T>(
                        compiledProperty,
                        action);
                    this.OperationBuffer.Add(fullPropertyName, propertyValidationToken);
                }
            }
        }

        protected PropertyValidationToken<T> GetPropertyValidtaionToken<T>(
            Expression<Func<TEntity, T>> property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            PropertyValidationToken<T> result = null;
            string fullPropertyName = this.FormFullPropertyName(property);
            lock (this.OperationBuffer)
            {
                object tempProperty;
                if (this.OperationBuffer.TryGetValue(fullPropertyName, out tempProperty))
                {
                    result = tempProperty as PropertyValidationToken<T>;
                }
            }

            return result;
        }

        protected virtual string FormFullPropertyName<T>(Expression<Func<TEntity, T>> property)
        {
            string result;
            result = string.Format("{0}.{1}", this._fullTypeName, property);
            return result;
        }

        protected sealed class PropertyValidationToken<T>
        {
            private readonly Func<TEntity, T> _property;
            private readonly Action<T> _verificationMethod;

            public PropertyValidationToken(Func<TEntity, T> property, Action<T> verificationMethod)
            {
                this._property = property;
                this._verificationMethod = verificationMethod;
            }

            public Func<TEntity, T> Property
            {
                get { return this._property; }
            }

            public Action<T> VerificationMethod
            {
                get { return this._verificationMethod; }
            }
        }

        #endregion
    }
}
