#region License
//
// Expression Engine Library: ExpressionEvaluatorFixture.cs
//
// Author:
//   Giacomo Stelluti Scala (gsscoder@gmail.com)
//
// Copyright (C) 2012 - 2013 Giacomo Stelluti Scala
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
using NUnit.Framework;
using Should.Fluent;
#endregion

namespace ExpressionEngine.Tests
{
    [TestFixture]
    public sealed class ExpressionFixture
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExpressionWithNullTextThrowsException()
        {
            Expression.Create(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExpressionWithNullTextThrowsException_2()
        {
            Expression.Create(null, variables: null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExpressionWithNullTextThrowsException_3()
        {
            Expression.Create(null, functions: null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExpressionWithNullTextThrowsException_4()
        {
            Expression.Create(null, null, null);
        }

        [Test]
        public void ExpressionWithoutUserDefinedNamesIsImmutable()
        {
            Expression.Create("1 + (3 / 4) * (2 - 1)").Should().Be.OfType<Expression>();
        }

        [Test]
        public void PlainAddition()
        {
            Expression.Create("1 + 2").Value.Should().Equal(3D);
        }

        [Test]
        public void PlainAddition_2()
        {
            Expression.Create("1 + 2 + 3").Value.Should().Equal(6D);
        }

        [Test]
        public void PlainAddition_3()
        {
            Expression.Create("1 + 2 + 3 + 4").Value.Should().Equal(10D);
        }

        [Test]
        public void PlainSubtraction()
        {
            Expression.Create("3 - 4").Value.Should().Equal(-1D);
        }

        [Test]
        public void PlainMultiplication()
        {
            Expression.Create("3 * 9").Value.Should().Equal(27D);
        }

        [Test]
        public void PlainDivision()
        {
            Expression.Create("16 / 4").Value.Should().Equal(4D);
        }

        [Test]
        public void PlainModulo()
        {
            Expression.Create("10 % 4").Value.Should().Equal(2D);
        }

        [Test]
        public void SmallExpr()
        {
            Expression.Create("1 + 2 * 3 / 1").Value.Should().Equal(7D);
        }

        [Test]
        public void SmallExprWithDecimal()
        {
            Expression.Create(".90 - 3 * 4 + 11.123").Value.Should().Equal(0.022999999999999687D);
        }

        [Test]
        public void SmallExprWithModulo()
        {
            Expression.Create("+0.90 - 3 * 4 + 11 - (123 % 23)").Value.Should().Equal(-8.1D);
        }

        [Test]
        public void SmallExprWithSignes()
        {
            Expression.Create("-3 + -1").Value.Should().Equal(-4D);
        }

        [Test]
        public void SmallExprWithFunction()
        {
            Expression.Create("sqrt(10) - 1").Value.Should().Equal(2.1622776601683795D);
        }

        [Test]
        public void SmallExprWithFunction_2()
        {
            Expression.Create("sqrt(10000) / 2").Value.Should().Equal(50D);
        }

        [Test]
        public void SmallExprWithFunction_3()
        {
            Expression.Create("sqrt(1000) % 3").Value.Should().Equal(1.6227766016837926D);
        }

        [Test]
        public void SmallExprWithFunction_4()
        {
            Expression.Create("cos(999) % .3").Value.Should().Equal(0.099649852980826459D);
        }

        [Test]
        public void SmallExprWithFunction_Unary()
        {
            Expression.Create("-sqrt(100)").Value.Should().Equal(-10D);
        }

        [Test]
        public void Expr()
        {
            Expression.Create("3 * 0.31 / 19 + 10 - .7").Value.Should().Equal(9.3489473684210527D);
        }

        [Test]
        public void ExprWithBrackets()
        {
            Expression.Create("1 + (2 - 1)").Value.Should().Equal(2D);
        }

        [Test]
        public void ExprWithBrackets_2()
        {
            Expression.Create("3 * 0.31 / ((19 + 10) - .7)").Value.Should().Equal(0.032862190812720848D);
        }

        [Test]
        public void ExprWithBrackets_3()
        {
            Expression.Create("3 * 0.31 / 19 + 10 - (1.7 % 2)").Value.Should().Equal(8.3489473684210527D);
        }

        [Test]
        public void ExprWithBrackets_Functions()
        {
            Expression.Create("3 * 0.31 / ((19 + sqrt(1000.5 / 10)) -.7 ^ 2) + 3").Value.Should().Equal(3.0326172734832215D);
        }

        [Test]
        public void UnaryBrackets()
        {
            Expression.Create("-(1 + 2)").Value.Should().Equal(-3D);
        }

        [Test]
        public void Function()
        {
            Expression.Create("2 ^ cos(90)").Value.Should().Equal(0.73302097391658683D);
        }

        [Test]
        public void Function_2()
        {
            Expression.Create("2 ^ -atan(-33)").Value.Should().Equal(2.9089582019337388);
        }

        [Test]
        public void NestedFunctions()
        {
            Expression.Create("log(log(10, 3))").Value.Should().Equal(0.73998461763125689D);
        }

        [Test]
        public void NestedFunctions_2()
        {
            Expression.Create("cos(-log(100))").Value.Should().Equal(-0.10701348355876977D);
        }

        [Test]
        public void NestedFunctions_3()
        {
            Expression.Create("sin(10.3) * cos(sqrt(sin(301 - sqrt(10))))").Value.Should().Equal(-0.55707344143373561D);
        }

        [Test]
        public void FunctionsWithNestedExpr()
        {
            Expression.Create("sqrt(sqrt(10 * 3) - sqrt(1 + 3))").Value.Should().Equal(1.8647320384043551D);
        }

        [Test]
        public void FunctionsWithNestedExpr_2()
        {
            Expression.Create("(10 * 3) ^ -(1 + log(10 * 10))").Value.Should().Equal(0.0000000052539263674376282D);
        }

        [Test]
        public void NegativePositiveDecimalBracket()
        {
            Expression.Create(".2 / +9.99 * (-.123 + -2)").Value.Should().Equal(-0.042502502502502509D);
        }

        [Test]
        public void NegativePositiveDecimalBracket_2()
        {
            Expression.Create("-.23 + +.99 * ((-.123 + -2.1)--333)").Value.Should().Equal(327.23922999999996D);
        }

        [Test]
        public void Exponent()
        {
            Expression.Create("10^2").Value.Should().Equal(100D);
        }

        [Test]
        public void ExponentWithNestedExpr()
        {
            Expression.Create("(10 * 3) ^ -(1 + 3)").Value.Should().Equal(0.0000012345679012345679D);
        }

        [Test]
        public void Variable()
        {
            Expression.Create("pi - 3").Value.Should().Equal(0.14159265358979312D);
        }

        [Test]
        public void Variable_2()
        {
            Expression.Create("(e + pi) - 5.8598744820488378").Value.Should().Equal(0D);
        }

        [Test]
        public void Constant_3()
        {
            Expression.Create("-e").Value.Should().Equal(-2.7182818284590451D);
        }

        [Test]
        public void Variable_Function()
        {
            Expression.Create("10 - (pi * atan(10)) - -e").Value.Should().Equal(8.0965979343737935D);
        }

        [Test]
        public void CachingExpression()
        {
            var expr = Expression.Create(".123 + .345");
            expr.IsValueCacheRetrieved.Should().Be.False();
            Expression.Create("1 + 2 + 3"); // anather expression...
            expr.IsValueCacheRetrieved.Should().Be.False();
            expr = null;
            var expr2 = Expression.Create(".123+.345"); // same as first, but with no spaces
            expr2.IsValueCacheRetrieved.Should().Be.True();
        }

        #region Expected Exceptions
        [Test]
        [ExpectedException(typeof(ExpressionException), ExpectedMessage = "Unexpected end of input, instead of token(s): 'LITERAL', 'IDENT', '+', '-', '(', '^'.")]
        public void IncompleteExpressioThrowsException()
        {
            Expression.Create("3 + 1 - (");
        }

        [Test]
        [ExpectedException(typeof(ExpressionException), ExpectedMessage = "Expected expression.")]
        public void IncompleteExpressioThrowsException_2()
        {
            Expression.Create("1 2");
        }

        [Test]
        [ExpectedException(typeof(ExpressionException), ExpectedMessage = "Syntax error, odd number of brackets.")]
        public void MissingBrackethrowsException()
        {
            Expression.Create("3 + (1 -");
        }

        [Test]
        [ExpectedException(typeof(ExpressionException), ExpectedMessage = "Syntax error, odd number of brackets.")]
        public void MissingBrackethrowsException_2()
        {
            Expression.Create("3 + 3 / (1");
        }
        #endregion
    }
}