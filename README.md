# Sanderling-V8
The memory inspection features of the **Sanderling** bot for **Eve Online** combined with a barebones **V8** Javascript engine.

This project makes use of the **memory scanning** library from the [Arcitectus/Sanderling](https://github.com/Arcitectus/Sanderling) project.

The V8 **engine wrapper** is currently provided by the [prepare/Espresso](https://github.com/prepare/Espresso) project.

This project is still a **work-in-progress** and the *master* branch is currently considered **unstable**.

## Design
The embedded V8 engine provides the bot with a **core low-level** JS library to handle basic UI input and output. The local libraries handle **basic logic** nothing more advanced than *"fly here"*, *"read this"*, *"attack that"*, *"scan for bad guys"*.

For the more **complex logic** that actually *runs* the bot, Sanderling-V8 will **communicate** with a **Node.JS** service running on the same system via **ZeroMQ** (todo).

## Stack
Windows 10  
Visual Studio 2017  
C# 7  
.NET 4  
V8 (from Node.JS)  
Javascript (ECMA 6)  
Sanderling (MemoryReading DLL)  
Eve Online

## Build
Build the solution once and then manually copy **lib/libespr.dll** to the **bin/<Release|Debug>** directory. That file is Espresso's patched version of Node.JS, built as a DLL.

## License (MIT)
Copyright (c) 2018 International Organization for Corn Mongering

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
