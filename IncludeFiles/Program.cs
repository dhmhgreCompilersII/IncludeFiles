using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace IncludeFiles {
    class Program {
        static void Main(string[] args) {
            var a = new StreamReader("./root.txt");
            var antlrInput = new AntlrInputStream(a);
            var lexer = new CalcLexer(antlrInput);
            var tokens = new BufferedTokenStream(lexer);
            var parser = new CalcParser(tokens);
            lexer.PushLexerState();
            IParseTree tree =parser.compileUnit();
            Console.WriteLine(tree.ToStringTree());

        }
    }
}
