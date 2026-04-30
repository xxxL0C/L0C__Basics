# \[Label\(Text\)\]

フィールドのインスペクタ上の表示名を任意の文字列に上書きします。

```csharp
[Label("移動速度 (m\u002fs)")]
public float moveSpeed = 5f;
```

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| Text | `string` | ○ | \- | 表示するラベル文字列<br />`string.Empty` / `""` の場合はラベル非表示 |
