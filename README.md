# Sanderling-V8
The memory inspection features of the **Sanderling** bot for **Eve Online** combined with a barebones **V8** Javascript engine.

This project makes use of the **memory scanning** library from the [Arcitectus/Sanderling](https://github.com/Arcitectus/Sanderling) project.

The V8 engine is currently provided by the [prepare/Espresso](https://github.com/prepare/Espresso) project. It wraps the **Node.JS** build to provide the native engine. Unfortunately, the **Espresso-ND** feature (embedded Node.JS) doesn't appear to be building in **Windows 10/Visual Studio 2017** yet, so we only have access to the barebones embedded V8 engine, not the Node.JS.

It may be a better long-term solution to use barebones V8 combined with a **.NET** version of Rhino's [env.js(https://github.com/envjs/env-js)

This project is still a work-in-progress and master branch is currently considered unstable.

## License (MIT)
Copyright (c) 2018 cornmonger

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
