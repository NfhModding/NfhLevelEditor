using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.Framework
{
    /// <summary>
    /// A simple <see cref="IModelChangeCommand"/> implementation for a single property update.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    public class ModelPropertyChangeCommand<T, P> : ModelChangeCommand
    {
        private T model;
        private P value;
        private Func<T, P> getter;
        private Action<T, P> setter;

        /// <summary>
        /// Initializes a new <see cref="ModelPropertyChangeCommand"/>.
        /// </summary>
        /// <param name="modelChangeNotifier">The <see cref="ModelChangeNotifier"/> to dispatch
        /// change notifications.</param>
        /// <param name="model">The model to update.</param>
        /// <param name="getter">The getter of the model's property.</param>
        /// <param name="setter">The setter of the model's property.</param>
        /// <param name="value">The value to set the model's property to.</param>
        public ModelPropertyChangeCommand(
            IModelChangeNotifier modelChangeNotifier, 
            T model,
            Func<T, P> getter, 
            Action<T, P> setter,
            P value)
            : base(modelChangeNotifier, model)
        {
            this.model = model;
            this.value = value;
            this.getter = getter;
            this.setter = setter;
        }

        /// <summary>
        /// Initializes a new <see cref="ModelPropertyChangeCommand"/>.
        /// </summary>
        /// <param name="modelChangeNotifier">The <see cref="ModelChangeNotifier"/> to dispatch
        /// change notifications.</param>
        /// <param name="model">The model to update.</param>
        /// <param name="property">An expression to the model's property to be updated..</param>
        /// <param name="value">The value to set the model's property to.</param>
        public ModelPropertyChangeCommand(
            IModelChangeNotifier modelChangeNotifier,
            T model,
            Expression<Func<T, P>> property,
            P value)
            : base(modelChangeNotifier, model)
        {
            this.model = model;
            this.value = value;
            var propertyInfo = GetPropertyInfo(property);
            this.getter = m => (P)propertyInfo.GetValue(m);
            this.setter = (m, v) => propertyInfo.SetValue(m, v);
        }

        protected override void ExecuteWithoutNotify() => setter(model, value);

        public override IModelChangeCommand GetUndoCommand() =>
            new ModelPropertyChangeCommand<T, P>(
                ModelChangeNotifier, 
                model,
                getter, 
                setter,
                getter(model));

        private static PropertyInfo GetPropertyInfo(Expression<Func<T, P>> property)
        {
            if (property.Body is UnaryExpression unaryExp)
            {
                if (unaryExp.Operand is MemberExpression memberExp)
                {
                    return (PropertyInfo)memberExp.Member;
                }
            }
            else if (property.Body is MemberExpression memberExp)
            {
                return (PropertyInfo)memberExp.Member;
            }

            throw new ArgumentException($"The expression doesn't indicate a valid property. [ {property} ]");
        }
    }
}
