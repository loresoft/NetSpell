#################################################################
## This Makefile Exported by MinGW Developer Studio
## Copyright (c) 2002-2004 by Parinya Thipchart
#################################################################

ifneq (,$(findstring Release, $(CFG)))
  override CFG = Release
else
  override CFG = Debug
endif

PROJECT = MySpell
CC = C:\Program Files\MinGWStudio\MinGW\bin\g++.exe

ifeq ($(CFG),Debug)
  OBJ_DIR = Debug
  OUTPUT_DIR = Debug
  TARGET = MySpell.exe
  C_INCLUDE_DIRS = 
  C_PREPROC = 
  CFLAGS = -pipe  -Wall -g2 -O0 -frtti -fexceptions 
  RC_INCLUDE_DIRS = 
  RC_PREPROC = 
  RCFLAGS = 
  LIB_DIRS = 
  LIBS = 
  LDFLAGS = -pipe 
endif

ifeq ($(CFG),Release)
  OBJ_DIR = Release
  OUTPUT_DIR = Release
  TARGET = MySpell.exe
  C_INCLUDE_DIRS = 
  C_PREPROC = 
  CFLAGS = -pipe  -Wall -g0 -O2 -frtti -fexceptions 
  RC_INCLUDE_DIRS = 
  RC_PREPROC = 
  RCFLAGS = 
  LIB_DIRS = 
  LIBS = 
  LDFLAGS = -pipe -s 
endif

ifeq ($(OS),Windows_NT)
  NULL =
else
  NULL = nul
endif

SRC_OBJS = \
  $(OBJ_DIR)/affentry.o	\
  $(OBJ_DIR)/affixmgr.o	\
  $(OBJ_DIR)/csutil.o	\
  $(OBJ_DIR)/dictmgr.o	\
  $(OBJ_DIR)/example.o	\
  $(OBJ_DIR)/hashmgr.o	\
  $(OBJ_DIR)/myspell.o	\
  $(OBJ_DIR)/suggestmgr.o

define build_target
@echo Linking...
@$(CC) -o "$(OUTPUT_DIR)\$(TARGET)" $(SRC_OBJS) $(LIB_DIRS) $(LIBS) $(LDFLAGS)
endef

define compile_source
@echo Compiling $<
@$(CC) $(CFLAGS) $(C_PREPROC) $(C_INCLUDE_DIRS) -c "$<" -o "$@"
endef

.PHONY: print_header directories

$(TARGET): print_header directories $(SRC_OBJS)
	$(build_target)

.PHONY: clean cleanall

cleanall:
	@echo Deleting intermediate files for 'MySpell - $(CFG)'
	-@del $(OBJ_DIR)\*.o
	-@del "$(OUTPUT_DIR)\$(TARGET)"
	-@rmdir "$(OUTPUT_DIR)"

clean:
	@echo Deleting intermediate files for 'MySpell - $(CFG)'
	-@del $(OBJ_DIR)\*.o

print_header:
	@echo ----------Configuration: MySpell - $(CFG)----------

directories:
	-@if not exist "$(OUTPUT_DIR)\$(NULL)" mkdir "$(OUTPUT_DIR)"
	-@if not exist "$(OBJ_DIR)\$(NULL)" mkdir "$(OBJ_DIR)"

$(OBJ_DIR)/affentry.o: affentry.cxx	\
license.readme	\
affentry.hxx	\
atypes.hxx	\
baseaffix.hxx	\
affixmgr.hxx	\
hashmgr.hxx	\
htypes.hxx
	$(compile_source)

$(OBJ_DIR)/affixmgr.o: affixmgr.cxx	\
license.readme	\
affixmgr.hxx	\
atypes.hxx	\
baseaffix.hxx	\
hashmgr.hxx	\
htypes.hxx	\
affentry.hxx
	$(compile_source)

$(OBJ_DIR)/csutil.o: csutil.cxx	\
csutil.hxx
	$(compile_source)

$(OBJ_DIR)/dictmgr.o: dictmgr.cxx	\
dictmgr.hxx
	$(compile_source)

$(OBJ_DIR)/example.o: example.cxx	\
myspell.hxx	\
hashmgr.hxx	\
htypes.hxx	\
affixmgr.hxx	\
atypes.hxx	\
baseaffix.hxx	\
suggestmgr.hxx	\
csutil.hxx
	$(compile_source)

$(OBJ_DIR)/hashmgr.o: hashmgr.cxx	\
license.readme	\
hashmgr.hxx	\
htypes.hxx
	$(compile_source)

$(OBJ_DIR)/myspell.o: myspell.cxx	\
license.readme	\
myspell.hxx	\
hashmgr.hxx	\
htypes.hxx	\
affixmgr.hxx	\
atypes.hxx	\
baseaffix.hxx	\
suggestmgr.hxx	\
csutil.hxx
	$(compile_source)

$(OBJ_DIR)/suggestmgr.o: suggestmgr.cxx	\
license.readme	\
suggestmgr.hxx	\
atypes.hxx	\
affixmgr.hxx	\
baseaffix.hxx	\
hashmgr.hxx	\
htypes.hxx
	$(compile_source)

