#region License
//
// Expression Engine Library: ExpressionVisitor.cs
//
// Author:
//   Giacomo Stelluti Scala (gsscoder@gmail.com)
//
// Copyright (C) 2012 Giacomo Stelluti Scala
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
#endregion
#region Using Directives
using System;
using System.Collections.Generic;
#endregion

namespace ExpressionEngine.Core
{
	abstract class ExpressionVisitor
	{
		public abstract void Visit(Model.LiteralExpression expression);

		public abstract void Visit(Model.UnaryExpression expression);

		public abstract void Visit(Model.FunctionExpression expression);

		public abstract void Visit(Model.BinaryExpression expression);

		public abstract object Result { get; }

		public class EvaluatingExpressionVisitor : ExpressionVisitor
		{
			public override void Visit(Model.LiteralExpression expression)
			{
				_result = expression.Value;
			}

			public override void Visit(Model.UnaryExpression expression)
			{
				expression.Value.Accept(this);
				switch (expression.Operator)
				{
					case Model.OperatorType.UnaryPlus:
						//_result = _result;
						break;
					case Model.OperatorType.UnaryMinus:
						_result = _result * -1;
						break;
					default:
						throw new ExpressionException("Invalid unary operator type.");
				}
			}

			public override void Visit(Model.FunctionExpression expression)
			{
				List<double> argsList = new List<double>(expression.Arguments.Count);
				expression.Arguments.ForEach(arg => {
					arg.Accept(this);
					argsList.Add(_result);
				});
				var args = argsList.ToArray();
				var builtIn = Kernel.BuiltIn.FromString(expression.Name);
            	if (builtIn == null)
            	{
                	throw new ExpressionException("Undefined function.");
            	}
				_result = builtIn.Execute(args);
			}

			public override void Visit(Model.BinaryExpression expression)
			{
				Func<double> left = () => {
					expression.Left.Accept(this);
					var leftValue = _result;
					return leftValue;
				};
				Func<double> right = () => {
					expression.Right.Accept(this);
					var rightValue = _result;
					return rightValue;
				};
	            switch (expression.Operator)
	            {
	                case Model.OperatorType.Add:
	                    _result = left() + right();
						break;
	                case Model.OperatorType.Subtract:
	                    _result =  left() - right();
						break;
	                case Model.OperatorType.Multiply:
	                    _result =  left() * right();
						break;
	                case Model.OperatorType.Divide:
						_result =  left() / right();
						break;
	                case Model.OperatorType.Exponent:
	                    _result =  Math.Pow(left(), right());
						break;
					default:
						throw new ExpressionException("Invalid binary operator type.");
				}
			}

			public override object Result
			{
				get
				{
					return _result;
				}
			}

			private double _result;
		}

		public static ExpressionVisitor Create()
		{
			return new EvaluatingExpressionVisitor();
		}
	}
}