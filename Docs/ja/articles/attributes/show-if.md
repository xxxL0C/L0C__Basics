# \[ShowIf\(Condition\)\]

別フィールドまたはパラメータなしメソッドから返る `bool` 値を参照して、フィールドを動的に表示します。

```csharp
public bool useCustomSpeed;

[ShowIf("useCustomSpeed")]
public float customSpeed;
```

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| Condition | `string` | ○ | \- | 参照する `bool` フィールドまたはメソッドの名前 |
