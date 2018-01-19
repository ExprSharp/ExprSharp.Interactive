<div align="center">
    <h1>REPL for ExprSharp</h1>
</div>

![](https://img.shields.io/badge/framework-.netcore2.0-blue.svg)
![](https://img.shields.io/badge/iexpr.core-v0.5.3-brightgreen.svg)
![](http://progressed.io/bar/60?title=done)

+ Author: Stardust D.L.
+ Version: 0.1

The REPL environment for ExprSharp.

> ExprSharp: A tiny code language based on [iExpr.Core](https://github.com/iExpr/iExpr.Core).

> For more information about ExprSharp, please see [**Wiki**](https://github.com/ExprSharp/ExprSharp.Core/wiki)

# Install

1. Install .Net Core [Here](https://www.microsoft.com/net/learn/get-started/)
2. Download the repl tools [Here](https://github.com/ExprSharp/ExprSharp.Interactive/releases)
3. Unzip and get into the diretory, then use the command:

```
dotnet esi.dll
```

4. Enjoy it.
    + How to write codes in ExprSharp? see [Wiki For ExprSharp](https://github.com/ExprSharp/ExprSharp.Core/wiki)

# Examples

For example, you can write math expression:

```
1+2
2**20
math.pi
```

Or if you want to calculate 20! , you can write:

```
func{f=(n)=>func{if(n==0){return 1}{return n*f(n-1)}},return f(20)}
```

Or "paint" a triangle:

```
for(i=1;i<=21;i+=2){print(" "*((21-i)/2)+"*"*i+" "*((21-i)/2))}
```

Also you can make the Fibonacci numbers by iterator:

+ In command line, it's so long in one line:

```
func{it=iter{this.current=0,f0:=0,f1:=1,this.next=(_)=>func{this.current=f0+f1,f0=f1,f1=this.current,return this.current<100}},print(list(it))}
```

+ But in a file, it can be clear:
    + you just need to use command `now` to set the file name, and use `run` to run the file.

```
func{
    it=iter{
        this.current=0,
        f0:=0,
        f1:=1,
        this.next=(_)=>func{
            this.current=f0+f1,
            f0=f1,
            f1=this.current,
            return this.current<100
        }
    },
    print(list(it))
}
```

For more example, see [Wiki For ExprSharp](https://github.com/ExprSharp/ExprSharp.Core/wiki)

# Commands of REPL
|Items|Description|
|-----|-----------|
|`now <filename>`| change the current file name|
|`run`|run the code from current file|
|`view`|view all the variables|
|`clear`|renew the runtime|
|`exit`|exit the REPL|
|`cls`|clear the console|
|any codes|execute the code|

# Dependencies

+ ExprSharp
+ CommandLineParser

# License

## LGPLv3