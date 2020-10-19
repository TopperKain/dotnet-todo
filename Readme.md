# Dotnet Todo.txt CLI

`dotnet-todo` is a .NET command line port of [Todo.txt](http://todotxt.org/) that tries to
remain faithful to the command line and functionality of the original shell script wherever
possible. As such, the [usage](#usage) below is a modified copy of the 
[original on GitHub](https://github.com/todotxt/todo.txt-cli/blob/master/USAGE.md).

## Installation

This program is a [dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools). 
Build it, then package it using the **Pack** command in Visual Studio or `dotnet pack` 
on the command line. Until this package is published, install it using the following
command line from the solution root;

```sh
dotnet tool install -g --add-source .\todo\nupkg\ dotnet-todo
```

## Enabling Tab Completion

This program supports tab completion using `dotnet-suggest`. To enable, for each shell
you must install the `dotnet-suggest` global tool and adding a shim to your profile. This 
only needs to be done once and work for all applications built using `System.CommandLine`.

Follow the [setup instructions](https://github.com/dotnet/command-line-api/blob/main/docs/dotnet-suggest.md)
for your shell.

## Usage

```shell
todo [-fhpantvV] [-d todo_config] action [task_number] [task_description]
```

## Actions

### `add`
Adds "THING I NEED TO DO" to your todo.txt file on its own line.  

Project and context notation optional.  

Quotes required.

```shell
todo add "THING I NEED TO DO +project @context"
todo a "THING I NEED TO DO +project @context"
```

### `addm`
Adds "FIRST THING I NEED TO DO" to your todo.txt on its own line and
adds "SECOND THING I NEED TO DO" to you todo.txt on its own line.

Project and context notation optional.

```shell
todo addm "FIRST THING I NEED TO DO +project1 @context" "SECOND THING I NEED TO DO +project2 @context"
```

### `addto`      
Adds a line of text to any file located in the todo.txt directory.

For example, `addto inbox.txt "decide about vacation"`

```shell
todo addto DEST "TEXT TO ADD"
```

### `append`
Adds TEXT TO APPEND to the end of the task on line ITEM#.

Quotes optional.

```shell
todo append ITEM# "TEXT TO APPEND"
todo app ITEM# "TEXT TO APPEND"
```

### `archive`
Moves all done tasks from todo.txt to done.txt and removes blank lines.

```shell
todo archive
```

### `deduplicate`
Removes duplicate lines from todo.txt.

```shell
todo deduplicate
```

### `del`
Deletes the task on line ITEM# in todo.txt. If TERM specified, deletes only 
TERM from the task.

```shell
todo del ITEM# [TERM]
todo rm ITEM# [TERM]
```

### `depri`
Deprioritizes (removes the priority) from the task(s) on line ITEM# in todo.txt.

```shell
todo depri ITEM#[, ITEM#, ITEM#, ...]
todo dp ITEM#[, ITEM#, ITEM#, ...]
```

### `do`
Marks task(s) on line ITEM# as done in todo.txt.

```shell
todo do ITEM#[, ITEM#, ITEM#, ...]
```

### `help`
Display help about usage, options, built-in and add-on actions, or just the usage 
help for the passed ACTION(s).

```shell
todo help [ACTION...]
```

### `list`
Displays all tasks that contain TERM(s) sorted by priority with line numbers.  Each 
task must match all TERM(s) (logical AND). Hides all tasks that contain TERM(s) 
preceded by a minus sign (i.e. `-TERM`). 

If no TERM specified, lists entire todo.txt.
​    
```shell
todo list [TERM...]
todo ls [TERM...]
```

### `listall`
Displays all the lines in todo.txt AND done.txt that contain TERM(s) sorted by 
priority with line  numbers. Hides all tasks that contain TERM(s) preceded by a 
minus sign (i.e. `-TERM`).

If no TERM specified, lists entire todo.txt AND done.txt concatenated and sorted.

```shell
todo listall [TERM...]
todo lsa [TERM...]
```

### `listcon`
Lists all the task contexts that start with the @ sign in todo.txt.

If TERM specified, considers only tasks that contain TERM(s).

```shell
todo listcon [TERM...]
todo lsc [TERM...]
```

### `listfile`
Displays all the lines in SRC file located in the todo.txt directory, sorted by 
priority with line numbers. If TERM specified, lists all lines that contain TERM(s) 
in SRC file. Hides all tasks that contain TERM(s) preceded by a minus sign (i.e. `-TERM`).

Without any arguments, the names of all text files in the todo.txt directory are listed.

```shell
todo listfile [SRC [TERM...]]
todo lf [SRC [TERM...]]
```

### `listpri`
Displays all tasks prioritized PRIORITIES. PRIORITIES can be a single one (A) or a range 
(A-C). If no PRIORITIES specified, lists all prioritized tasks. If TERM specified, lists 
only prioritized tasks that contain TERM(s). Hides all tasks that contain TERM(s) preceded 
by a minus sign (i.e. `-TERM`).

```shell
todo listpri [PRIORITIES] [TERM...]
todo lsp [PRIORITIES] [TERM...]
```

### `listproj`
Lists all the projects (terms that start with a `+` sign) in todo.txt. If TERM specified, 
considers only tasks that contain TERM(s).

```shell
todo listproj [TERM...]
todo lsprj [TERM...]
```

### `move`
Moves a line from source text file (SRC) to destination text file (DEST). Both source 
and destination file must be located in the directory defined in the configuration 
directory. When SRC is not defined it's by default todo.txt.

```shell
todo move ITEM# DEST [SRC]
todo mv ITEM# DEST [SRC]
```

### `prepend`
Adds TEXT TO PREPEND to the beginning of the task on line ITEM#. Quotes optional.

```shell
todo prepend ITEM# "TEXT TO PREPEND"
todo prep ITEM# "TEXT TO PREPEND"
```

### `pri`
Adds PRIORITY to task on line ITEM#.  If the task is already prioritized, replaces 
current priority with new PRIORITY. PRIORITY must be a letter between A and Z.

```shell
todo pri ITEM# PRIORITY
todo p ITEM# PRIORITY
```

### `replace`
Replaces task on line ITEM# with UPDATED TODO.

```shell
todo replace ITEM# "UPDATED TODO"
```

## Options

### `-@`
Hide context names in list output.

### `-+`
Hide project names in list output.

### `-c`
Color mode

### `-d CONFIG_FILE`
Use a configuration file other than the default `~/.todo/config`

### `-f`
Forces actions without confirmation or interactive input.

### `-h`
Display a short help message; same as action "shorthelp"

### `-p`
Plain mode turns off colors

### `-P`
Hide priority labels in list output.

### `-a`
Don't auto-archive tasks automatically on completion

### `-t`
Prepend the current date to a task automatically when it's added.

### `--version`
Displays version, license and credits