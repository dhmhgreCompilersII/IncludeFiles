parser grammar CalcParser;

options { tokenVocab = CalcLexer; }

compileUnit
	:	( exp SEMI? )+
	;

exp : NUMBER
	| exp PLUS exp
	| exp MINUS exp
	| exp MULT exp
	| exp DIV exp
	 ;
