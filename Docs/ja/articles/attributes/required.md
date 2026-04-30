# \[Required\(Message\)\]

参照型フィールドが `null` のとき、インスペクタにエラーを表示します。  
アサインし忘れ系のバグをエディタ段階で検出できます。

> [!NOTE]
> エディタ上の視覚的な警告のみで、ビルドには影響しません。

```csharp
[Required("Player Animator は必須です")]
public Animator playerAnimator;
```

| パラメータ | 型 | 必須 | 初期値 | 説明 |
| --- | --- | --- | --- | --- |
| Message | `string` | \- | `"This field cannot be Null."` | 表示するエラーメッセージ |
