namespace Anycmd.UnifiedQueries.Validators
{
    using System.Collections.Generic;

    internal sealed class ExpressionNameTypeValidator : QuerySpecificationValidator
    {
        private readonly Dictionary<string, DataTypes> _nameTypeDictionary = new Dictionary<string, DataTypes>();

        public ExpressionNameTypeValidator(QuerySpecification querySpecification)
            : base(querySpecification)
        {
        }

        protected override void VisitExpression(Expression expression)
        {
            if (_nameTypeDictionary.ContainsKey(expression.Name))
            {
                var type = _nameTypeDictionary[expression.Name];
                if (type != expression.Type)
                {
                    this.AddError(
                        string.Format(
                            "The data type defined on the expression '{0}' ('{1}') is not consistent with the previous definition ('{2}').",
                            expression.Name,
                            expression.Type,
                            type));
                }
            }
            else
            {
                _nameTypeDictionary.Add(expression.Name, expression.Type);
            }
        }

        protected override void VisitLogicalOperation(LogicalOperation logicalOperation)
        {
        }

        protected override void VisitUnaryLogicalOperation(UnaryLogicalOperation unaryLogicalOperation)
        {
        }
    }
}
