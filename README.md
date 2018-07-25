# NetSpell

Spell Checker for .net

[![NuGet Version](https://img.shields.io/nuget/v/NetSpell.svg?style=flat-square)](https://www.nuget.org/packages/NetSpell/)

## Download

The NetSpell library is available on nuget.org via package name `NetSpell`.

To install NetSpell, run the following command in the Package Manager Console

    PM> Install-Package NetSpell

More information about NuGet package available at
<https://nuget.org/packages/NetSpell>

## Development Builds

Development builds are available on the myget.org feed.  A development build is promoted to the main NuGet feed when it's determined to be stable.

In your Package Manager settings add the following package source for development builds:
<http://www.myget.org/F/loresoft/>

## Overview

The NetSpell project is a spell checking engine written entirely in managed C#
.net code. NetSpell's suggestions for a misspelled word are generated using phonetic
(sounds like) matching and ranked by a typographical score (looks like). NetSpell
supports multiple languages and the dictionaries are based on the OpenOffice Affix
compression format. The library can be used in Windows or Web Form projects. The
download includes an English dictionary with dictionaries for other languages available for download on the project web site. NetSpell also supports user added words and automatic creation of user dictionaries. The package includes a dictionary build tool to build custom dictionaries.

## Dictionaries

NetSpell dictionaries are based on the OpenOffice dictionary format. You can use
the Dictionary build tool to convert OpenOffice dictionaries to the NetSpell format.
OpenOffice dictionaries can be downloaded here: <http://lingucomponent.openoffice.org/dictionary.html>

## Features

- Detect misspelled works
- Generate suggestions for misspelled word
- Rank possible words for misspelled word
- Support multiple dictionaries

## Change Log

### NetSpell 2.1.7

- Fixed DeleteWord bug
- Improved white space handling for DeleteWord 
- Renamed SpellingForm to SuggestionForm
- Renamed `en-uk.dic` to `en-gb.dic`

### NetSpell 2.1.6

- Fixed EndOfText event bugs
- Fixed ReplaceWord bug
- Added AlertComplete Property to enable or disable Spell Check Complete message box.
- Added FreeTextBox Demo

### NetSpell 2.1.5

- Changed internal form to lazy load for better resource use in an asp.net application
- Made properties less dependant on the order in wish they are set.
- Fixed a dispose bug in internal form
- Bug fixes in Dictionary Build tool

### NetSpell 2.1

- Implemented spell checking for a range of words
- Added events for ReplacedWord, IgnoredWord, and DeletedWord
- IgnoreList and ReplaceList no longer reset when the Text property is set
- Spelling.Dictionary now lazy loads to make sharing the dictionary more efficient
- WordDictionary is now a component to better support design time and dictionary sharing
- Tracing as been added
- Improved HTML, XML and RTF support
- Added several new demos

### NetSpell 2.0.3

- Major bug fix in WordDictionary.Contains method
- update ignore html routine
- replace all bug fix
- minor web demo bug fixes

### NetSpell 2.0.2

- web demo bug fixes
- changes to user dictionary

### NetSpell 2.0.1

- Complete change to the dictionary
- Added international support
- Added Affix compression support
- Dictionary now based on OpenOffice dictionary format
- Improved web demo
- web demo now supports Ignore All, Replace All and Add Word
- Windows demo changed to a MDI text editor

### NetSpell 1.1

- Added Html Ignore support
- Changed license to BSD for more flexibility

### NetSpell 1.0

- More improvements to Visual Studio designer
- Replace word bug fix
- Improved suggestions by using Ispell's near miss strategy
- Misc bug fixes

### NetSpell 0.9

- Added MainDictionary property to spelling object
- Added UserDictionary property to spelling object to make it easier to identify when there was a user dictionary
- Changed constructor `Spelling(string dictionaryFile, string text)` to `Spelling(string mainDictionaryFile, string userDictionaryFile)`
- Added SpellingForm property to expose the internal suggestion form
- Added 'Add' button to internal suggestion form
- Improved Visual Studio designer support
- Added Web Demo project to demonstrate using NetSpell in an Asp.Net project
- Misc bug fixes
