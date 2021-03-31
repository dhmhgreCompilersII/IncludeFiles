lexer grammar CalcLexer;

@lexer::header {
	using System;
	using System.IO;
}

@lexer::members { 
	string file;
	ICharStream current;
	
}

/*
 * Lexer Rules
 */
INCLUDEPREFIX : '#include'[ \t]+'"' {
									  Skip();
									  Mode(CalcLexer.FILEMODE);
									  
									};

// The following ruls has always less length matched string that the the rule above
// ANY : ~[#]+ { Console.WriteLine("#"+Text); };
NUMBER : [1-9][0-9]*;
PLUS : '+';
MINUS : '-';
MULT : '*';
DIV : '/';
SEMI : ';';
WS : [ \r\n\t]+ ->skip;
ENDOFFILE : EOF ;


////////////////////////////////////////////////////////////////////////////////////////////////////////
mode FILEMODE;
FILE : [a-zA-Z][a-zA-Z0-9_]*'.'[a-zA-Z0-9_]+ {  
												file= Text;
												StreamReader s = new StreamReader(file);
												current =new AntlrInputStream(s);			
											    Skip();
											 };
DQUOTE: '"'  {  
				this._input = current;
				this._tokenFactorySourcePair = Tuple.Create<ITokenSource, ICharStream>((ITokenSource) this, current);
				PushLexerState();
				Skip();	
				Mode(CalcLexer.DefaultMode); 	
				
};
