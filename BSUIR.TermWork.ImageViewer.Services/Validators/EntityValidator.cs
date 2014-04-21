// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The entity validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Linq.Expressions;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The entity validator.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    internal abstract class EntityValidator<TEntity> : EntityBaseValidator, 
                                                       IEntityValidator<TEntity>
        where TEntity : EntityBase
    {
        #region Fields

        /// <summary>
        /// The _full type name.
        /// </summary>
        private readonly string _fullTypeName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityValidator{TEntity}"/> class.
        /// </summary>
        protected EntityValidator()
        {
            this._fullTypeName = typeof(TEntity).FullName;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public abstract void Validate(TEntity entity);

        /// <summary>
        /// The validate property.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="propertyValue">
        /// The property value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="EntityValidationException">
        /// </exception>
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

        #endregion

        #region Methods

        /// <summary>
        /// The form full property name.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected virtual string FormFullPropertyName<T>(Expression<Func<TEntity, T>> property)
        {
            string result;
            result = string.Format("{0}.{1}", this._fullTypeName, property);
            return result;
        }

        /// <summary>
        /// The get property validtaion token.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="EntityValidator"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
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

        /// <summary>
        /// The register property.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// </exception>
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

        /// <summary>
        /// The validate key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <exception cref="EntityValidationException">
        /// </exception>
        protected virtual void ValidateKey(int key)
        {
            if (key < 0)
            {
                throw new EntityValidationException("The key value cannot be below zero!");
            }
        }

        #endregion

        /// <summary>
        /// The property validation token.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        protected sealed class PropertyValidationToken<T>
        {
            #region Fields

            /// <summary>
            /// The _property.
            /// </summary>
            private readonly Func<TEntity, T> _property;

            /// <summary>
            /// The _verification method.
            /// </summary>
            private readonly Action<T> _verificationMethod;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="PropertyValidationToken{T}"/> class.
            /// </summary>
            /// <param name="property">
            /// The property.
            /// </param>
            /// <param name="verificationMethod">
            /// The verification method.
            /// </param>
            public PropertyValidationToken(Func<TEntity, T> property, Action<T> verificationMethod)
            {
                this._property = property;
                this._verificationMethod = verificationMethod;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the property.
            /// </summary>
            public Func<TEntity, T> Property
            {
                get { return this._property; }
            }

            /// <summary>
            /// Gets the verification method.
            /// </summary>
            public Action<T> VerificationMethod
            {
                get { return this._verificationMethod; }
            }

            #endregion
        }
    }
}