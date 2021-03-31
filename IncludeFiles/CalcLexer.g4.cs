using System;
using System.Collections.Generic;
using Antlr4.Runtime;

namespace IncludeFiles {

    public class LexerState {
        string m_file;
        ICharStream m_current;
        private Tuple<ITokenSource, ICharStream> m_tokenFactorySourcePair;

        public string M_File {
            get => m_file;
            set => m_file = value;
        }

        public ICharStream M_Current {
            get => m_current;
            set => m_current = value;
        }

        public Tuple<ITokenSource, ICharStream> M_TokenFactorySourcePair {
            get => m_tokenFactorySourcePair;
            set => m_tokenFactorySourcePair = value;
        }
    }

    public partial class CalcLexer {
        public Stack<LexerState> m_lexerStates = new Stack<LexerState>();
        public LexerState m_currentState=null;

        public void PushLexerState() {
            m_currentState = new LexerState() {
                M_Current = _input,
                M_TokenFactorySourcePair = _tokenFactorySourcePair,
                M_File = file
            };
            m_lexerStates.Push(m_currentState);
        }

        public void PopParserState() {
            m_lexerStates.Pop();
            m_currentState = m_lexerStates.Peek();
            _input = m_currentState.M_Current;
            _tokenFactorySourcePair = m_currentState.M_TokenFactorySourcePair;
        }

        public override IToken NextToken() {
            int marker = this._input != null ? this._input.Mark() : throw new InvalidOperationException("nextToken requires a non-null input stream.");
        label_3:
            try {
                while (!this._hitEOF) {
                    this._token = (IToken)null;
                    this._channel = 0;
                    this._tokenStartCharIndex = this._input.Index;
                    this._tokenStartCharPositionInLine = this.Interpreter.Column;
                    this._tokenStartLine = this.Interpreter.Line;
                    this._text = (string)null;
                    do {
                        this._type = 0;
                        int num;
                        try {
                            num = this.Interpreter.Match(this._input, this._mode);
                        } catch (LexerNoViableAltException ex) {
                            this.NotifyListeners(ex);
                            this.Recover(ex);
                            num = -3;
                        }

                        if (this._input.La(1) == -1) {
                           this._hitEOF = true;
                        }
                        if (this._type == 0)
                            this._type = num;
                        if (this._type == -3)
                            goto label_3;
                    }
                    while (this._type == -2);
                    if (this._token == null)
                        this.Emit();
                    return this._token;
                }

                if (this._hitEOF == true) {
                    if (m_lexerStates.Count == 1) {
                        this.EmitEOF();
                    } else {
                        PopParserState();
                        this._hitEOF = false;
                        return this.NextToken();
                    }
                }
                return this._token;
            } finally {
                this._input.Release(marker);
            }
        }
    }
}
