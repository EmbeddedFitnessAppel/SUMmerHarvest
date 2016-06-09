using System;
using System.Collections.Generic;

public class FuncComparer<T> : IComparer<T> {
    private readonly Func<T, T, int> func;
    public FuncComparer(Func<T, T, int> comparerFunc) {
        this.func = comparerFunc;
    }

    public int Compare(T x, T y) {
        return this.func(x, y);
    }
}