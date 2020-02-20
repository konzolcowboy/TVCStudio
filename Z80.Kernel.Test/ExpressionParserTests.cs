using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z80.Kernel.Z80Assembler;
using Z80.Kernel.Z80Assembler.ExpressionHandling;

namespace Z80.Kernel.Test
{
    [TestClass]
    public class ExpressionParserTests
    {
        [DataTestMethod]
        [DataRow("2+1")]
        [DataRow("$02+$1")]
        public void Plus(string expression)
        {
            var resolver = new ExpressionParser(expression);
            ParseResult result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 3);
        }
        [DataTestMethod]
        [DataRow("2-1")]
        [DataRow("$02-$01")]
        public void Minus(string expression)
        {
            var resolver = new ExpressionParser(expression);
            ParseResult result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 1);
        }
        [DataTestMethod]
        [DataRow("9/3")]
        [DataRow("$09/$03")]
        public void Divide(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 3);
        }
        [DataTestMethod]
        [DataRow("5*3")]
        [DataRow("$05*$03")]
        public void Multiply(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 15);
        }
        [DataTestMethod]
        [DataRow("171&222")]
        [DataRow("$ab&$de")]
        public void And(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 138);
        }
        [DataTestMethod]
        [DataRow("(2+3)*(3+4)")]
        [DataRow("($2+$3)*($3+$4)")]
        public void PlusAndMultiplyWithParenthesis(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 35);
        }
        [DataTestMethod]
        [DataRow("+3")]
        [DataRow("+$3")]
        public void UnaryPlus(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 3);
        }
        [DataTestMethod]
        [DataRow("-10")]
        [DataRow("-$a")]
        public void UnaryMinus(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual((short)result.ResultValue, -10);
        }
        [DataTestMethod]
        [DataRow("(-10)*(-3)")]
        [DataRow("-$a*(-$3)")]
        public void MultiplyWithUnaryMinus(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual((short)result.ResultValue, 30);
        }
        [TestMethod]
        public void WrongParenthesisCount()
        {
            var resolver = new ExpressionParser("(-2)*(-3");
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Error);
        }
        [TestMethod]
        public void TwoOperandsAfterEachOther()
        {
            var resolver = new ExpressionParser("2+-5");
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Error);
        }
        [DataTestMethod]
        [DataRow("sqrt(25)")]
        [DataRow("sqrt($19)")]
        public void Sqrt(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 5);
        }
        [DataTestMethod]
        [DataRow("abs(-9)")]
        [DataRow("abs(-$FFF7)")]
        public void Abs(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 9);
        }
        [TestMethod]
        public void High()
        {
            var resolver = new ExpressionParser("high($ABCD)");
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 0xab);
        }
        [TestMethod]
        public void Low()
        {
            var resolver = new ExpressionParser("low($ABCD)");
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 0xcd);
        }
        [DataTestMethod]
        [DataRow("171|205")]
        [DataRow("$ab|$cd")]
        public void Or(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 239);
        }
        [DataTestMethod]
        [DataRow("171^205")]
        [DataRow("$ab^$cd")]
        public void Xor(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 102);
        }
        [DataTestMethod]
        [DataRow("~205")]
        [DataRow("~$00cd")]
        public void OnesComplement(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 0xff32);
        }
        [DataTestMethod]
        [DataRow("205 < 2")]
        [DataRow("$cd < 2")]
        public void ShiftLeft(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 0x334);
        }
        [DataTestMethod]
        [DataRow("205 > 2")]
        [DataRow("$cd > 2")]
        public void ShiftRight(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 0x33);
        }
        [DataTestMethod]
        [DataRow("low(205 < 2)")]
        [DataRow("low($cd < 2)")]
        public void ShiftLeftWithLow(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 0x34);
        }
        [DataTestMethod]
        [DataRow("unsupported(205 < 2)")]
        [DataRow("unsupported($cd < 2)")]
        public void NotSupportedOperatorThrows(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Error);
        }
        [DataTestMethod]
        [DataRow("high(abs(-4200))")]
        [DataRow("high(abs($ef97))")]
        public void HighWithAbs(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 0x10);
        }
        [DataTestMethod]
        [DataRow("sqrt(abs(-6*9+3456+134/1200))")]
        public void SqrtWithAbs(string expression)
        {
            var resolver = new ExpressionParser(expression);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 0x3A);
        }

        [TestMethod]
        public void ExpressionWithHexaSymbol()
        {
            var symbolTable = new Dictionary<string, Symbol> { { "X", new Symbol { Value = 0x19 } } };
            var expression = "sqrt(X)";
            var resolver = new ExpressionParser(expression, symbolTable);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 5);
        }

        [TestMethod]
        public void DivideByZeroDoesNotWork()
        {
            var resolver = new ExpressionParser("2/0");
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Error);
        }
        [TestMethod]
        public void ExpressionWithDecimalSymbol()
        {
            var symbolTable = new Dictionary<string, Symbol> { { "X", new Symbol { Value = 25 } } };
            var expression = "sqrt(X)";
            var resolver = new ExpressionParser(expression, symbolTable);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 5);
        }

        [TestMethod]
        public void ExpressionWithFutureSymbol()
        {
            var symbolTable = new Dictionary<string, Symbol> { { "X", new Symbol() } };
            var expression = "sqrt(X)";
            var resolver = new ExpressionParser(expression, symbolTable);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.ContainsFutureSymbol);
        }

        [TestMethod]
        public void ExpressionWithThreeSymbols()
        {
            var symbolTable = new Dictionary<string, Symbol>
            {
                { "X", new Symbol{Value = 0x01} },
                { "Y", new Symbol { Value = 2 } },
                { "Z", new Symbol { Value = 0x03 } }
            };

            var expression = "(x*y*z)*(x+y+z)";
            var resolver = new ExpressionParser(expression, symbolTable);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 36);
        }

        [TestMethod]
        public void ExpressionWithNumberInSymbolName()
        {
            var symbolTable = new Dictionary<string, Symbol> { { "NUMBER345", new Symbol { Value = 0x19 } } };
            var expression = "sqrt(number345)";
            var resolver = new ExpressionParser(expression, symbolTable);
            var result = resolver.Parse();
            Assert.AreEqual(result.ResultCode, ParseResultCode.Ok);
            Assert.AreEqual(result.ResultValue, 5);
            Assert.AreEqual(resolver.SymbolsInExpression.Count, 1);
            Assert.AreEqual(resolver.SymbolsInExpression[0], "NUMBER345");
        }
    }
}
