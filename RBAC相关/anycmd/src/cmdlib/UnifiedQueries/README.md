##UQML简介
Unified Queries Markup Language可以简称UQML。
UQML是一个基于xml的标记语言，它由一系列predicate和几个可以将一个predicate集合并为单条predicate的predicate Combining algorithms组成。
**predicate**是一系列标准function，这些function的返回值是布尔类型。
**predicate Combining algorithms**只有两个，一个and算法一个or算法。
UQML引擎通常直接解析xml，直接将UQML解析为sql字符串。它是简单的，它是高效的。

##最佳实践
查询返回的数据应该是keyValue数组或者HashTable那样的数据，不应该是强类型的字段都写死过的实体对象。

##官方
这是apworks的作者daxnet的框架，进一步了解请转到daxnet的博文 http://www.cnblogs.com/daxnet/p/3925426.html