<%@ Page Language="C#" %>
<script runat="server">

    // Insert page code here
    //

</script>
<html>
<head>
</head>
<body>
    <form runat="server">
        <p>
            <font size="4"><font size="3"><font face="Arial"><strong><font size="4">NetSpell 2.0
            - Spell Checker for .net</font></strong> </font></font></font>
        </p>
        <p>
            <font size="4"><font size="3"><font face="Arial"><strong>What is it?</strong> 
            <br />
            </font></font></font>
        </p>
        <font face="Arial"> 
        <hr />
        </font> 
        <p>
            <font face="Arial"></font>
        </p>
        <p>
            <font face="Arial">The NetSpell project is a spell checking engine written entirely
            in managed C# .net code. Package includes&nbsp; an English dictionaries with dictionaries
            for other languages available for download on the project web site. NetSpell's suggestions
            for misspelled words are generated using phonetic (sounds like) matching and ranked
            by a typographical score (looks like).&nbsp; Also supports "ignore all" and "replace
            all" misspelled-word handling. Project also includes a dictionary build tool to build
            custom dictionaries. </font>
        </p>
        <p>
            <font face="Arial"><font size="4"><font size="3">This project was built and compiled
            with Visual Studio .net 2003 and the .net 1.1 framework.</font></font> </font>
        </p>
        <font size="4"><font size="3"> 
        <p>
            <br />
            <font face="Arial"><strong>Dictionaries</strong> 
            <br />
            </font>
        </p>
        <font face="Arial"> 
        <hr />
        </font> 
        <p>
        </p>
        <p>
            <font face="Arial">NetSpell dictionaries are based on the OpenOffice dictionary format.&nbsp;
            You can use the Dictionary build tool to convert OpenOffice dictionaries to the NetSpell
            format.&nbsp; OpenOffice dictionaries can be downloaded at </font><a href="http://lingucomponent.openoffice.org/dictionary.html"><font face="Arial">http://lingucomponent.openoffice.org/dictionary.html</font></a><font face="Arial"> </font>
        </p>
        <p>
            <br />
            <font face="Arial"><strong>The Latest Version</strong> 
            <br />
            </font>
        </p>
        <font face="Arial"> 
        <hr />
        Details of the latest version can be found on the project web site. </font><a href="http://www.loresoft.com/netspell"><font face="Arial">http://www.loresoft.com/netspell</font></a><font face="Arial">&nbsp; 
        <br />
        The project web site also contains the dictionary build tool which can be used to
        build custom dictionaries. </font> 
        <p>
        </p>
        <p>
            <br />
            <font face="Arial"><strong>Documentation</strong> 
            <br />
            </font>
        </p>
        <font face="Arial"> 
        <hr />
        Documentation is available in HTML format, in the doc/ directory. </font> 
        <p>
        </p>
        <p>
            <br />
            <strong><font face="Arial">References and Credits<br />
            </font></strong>
        </p>
        <font face="Arial"> 
        <hr />
        OpenOffice Lingucomponent </font><a href="http://lingucomponent.openoffice.org/dictionary.html"><font face="Arial">http://lingucomponent.openoffice.org/dictionary.html</font></a><font face="Arial"> 
        <br />
        Dictionary Wordlists&nbsp;&nbsp;</font><a href="http://wordlist.sourceforge.net/"><font face="Arial">http://wordlist.sourceforge.net/</font></a><font face="Arial"> 
        <br />
        Metaphone Algorithm &nbsp;&nbsp;</font><a href="http://aspell.net/metaphone/"><font face="Arial">http://aspell.net/metaphone/</font></a><font face="Arial"> </font> 
        <p>
        </p>
        <p>
            <br />
            <strong><font face="Arial">BSD License<br />
            </font></strong>
        </p>
        <font face="Arial"> 
        <hr />
        Copyright (c) 2003, Paul Welter<br />
        All rights reserved. </font> 
        <p>
        </p>
        <p>
            <font face="Arial">Redistribution and use in source and binary forms, with or without
            modification, are permitted provided that the following conditions are met: </font>
        </p>
        <p>
            <font face="Arial">1) Redistributions of source code must retain the above copyright
            notice, this list of conditions and the following disclaimer. 
            <br />
            2) Redistributions in binary form must reproduce the above copyright notice, this
            list of conditions and the following disclaimer in the documentation and/or other
            materials provided with the distribution. 
            <br />
            3) Neither the name of the ORGANIZATION nor the names of its contributors may be used
            to endorse or promote products derived from this software without specific prior written
            permission. </font>
        </p>
        <p>
            <font face="Arial">THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
            "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
            IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
            IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
            INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
            TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
            BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
            STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
            OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.<br />
            </font>
        </p>
        </font></font> 
        <p>
            <font face="Arial"><strong>History</strong> </font>
        </p>
        <p>
            <font face="Arial">NetSpell 2.1<br />
            * Implemented spell checking for a range of words<br />
            * Added events for ReplacedWord, IgnoredWord, and DeletedWord<br />
            * IgnoreList and ReplaceList no longer reset when the Text property is set<br />
            * Spelling.Dictionary now lazy loads to make sharing the dictionary more efficient<br />
            * WordDictionary is now a component to better support design time and dictionary sharing<br />
            * Tracing as been added<br />
            * Improved HTML, XML and RTF support<br />
            * Added several new demos </font>
        </p>
        <p>
            <font face="Arial">NetSpell 2.0.3<br />
            * Major bug fix in WordDictionary.Contains method<br />
            * update ignore html routine<br />
            * replace all bug fix<br />
            * minor web demo bug fixes </font>
        </p>
        <p>
            <font face="Arial">NetSpell 2.0.2<br />
            * web demo bug fixes<br />
            * changes to user dictionary </font>
        </p>
        <p>
            <font face="Arial">NetSpell 2.0.1<br />
            * Complete change to the dictionary<br />
            &nbsp; - Added international support<br />
            &nbsp; - Added Affix compression support<br />
            &nbsp; - Dictionary now based on OpenOffice dictionary format<br />
            * Improved web demo<br />
            &nbsp; - web demo now supports Ignore All, Replace All and Add Word<br />
            * Windows demo changed to a MDI text editor </font>
        </p>
        <p>
            <br />
            <font face="Arial">NetSpell 1.1<br />
            * Added Html Ignore support<br />
            * Changed license to BSD for more flexibility </font>
        </p>
        <p>
            <font face="Arial">NetSpell 1.0 Stable Release 
            <br />
            * More improvements to Visual Studio designer<br />
            * Replace word bug fix<br />
            * Improved suggestions by using Ispell's near miss strategy<br />
            * Misc bug fixes </font>
        </p>
        <p>
            <font face="Arial">NetSpell beta 0.9 </font>
        </p>
        <p>
            <font face="Arial">* Added MainDictionary property to spelling object<br />
            * Added UserDictionary property to spelling object to make it easier to identify when
            there was a user dictionary<br />
            * Changed constructor Spelling(string dictionaryFile, string text) to Spelling(string
            mainDictionaryFile, string userDictionaryFile)<br />
            * Added SpellingForm property to expose the internal suggestion form<br />
            * Added "Add" button to internal suggestion form<br />
            * Improved Visual Studio designer support<br />
            * Added Web Demo project to demonstrate using NetSpell in an Asp.Net project<br />
            * Misc bug fixes </font>
        </p>
        <p>
            <font face="Arial">NetSpell beta 0.8 </font>
        </p>
        <p>
            <font face="Arial">* First beta build<br />
            </font>
        </p>
    </form>
</body>
</html>
